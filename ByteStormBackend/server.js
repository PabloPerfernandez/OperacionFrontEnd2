// Importar módulos necesarios
const express = require('express');
const bodyParser = require('body-parser');
const sqlite3 = require('sqlite3').verbose();
const cors = require('cors');

// Crear una instancia de Express
const app = express();
const PORT = 5056;

// Middleware
app.use(cors()); // Habilitar CORS
app.use(bodyParser.json()); // Analizar cuerpos de solicitudes JSON

// Conexión a la base de datos SQLite
const db = new sqlite3.Database('BytestormSQL', (err) => {
    if (err) {
        console.error('Error al conectar a la base de datos:', err.message);
    } else {
        console.log('Conectado a la base de datos SQLite.');
    }
});

// Crear la tabla "items" si no existe
db.run(`
    CREATE TABLE IF NOT EXISTS items (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        nombre TEXT NOT NULL,
        descripcion TEXT NOT NULL,
        tipo TEXT NOT NULL
    );
`, (err) => {
    if (err) {
        console.error('Error al crear la tabla:', err.message);
    } else {
        console.log('Tabla "items" verificada/existente.');
    }
});

// Ruta para crear un nuevo elemento
app.post('/api/crear', async (req, res) => {
    const { nombre, descripcion, tipo } = req.body;
    
    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    try {
        const sql = 'INSERT INTO items (nombre, descripcion, tipo) VALUES (?, ?, ?)';
        db.run(sql, [nombre, descripcion, tipo], function(err) {
            if (err) {
                return res.status(500).json({ message: 'Error al insertar los datos', error: err.message });
            }
            res.status(201).json({ message: 'Datos insertados correctamente', id: this.lastID });
        });
    } catch (error) {
        res.status(500).json({ message: 'Error al procesar la solicitud', error: error.message });
    }
});

// Ruta para obtener todos los elementos
app.get('/api/items', (req, res) => {
    const sql = 'SELECT * FROM items';
    db.all(sql, [], (err, rows) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener los datos', error: err.message });
        }
        res.json(rows);
    });
});

// Ruta para obtener un elemento por ID
app.get('/api/items/:id', (req, res) => {
    const id = req.params.id;
    const sql = 'SELECT * FROM items WHERE id = ?';

    db.get(sql, [id], (err, row) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener el dato', error: err.message });
        }
        if (!row) {
            return res.status(404).json({ message: 'Elemento no encontrado' });
        }
        res.json(row);
    });
});

// Ruta para actualizar un elemento
app.put('/api/items/:id', (req, res) => {
    const id = req.params.id;
    const { nombre, descripcion, tipo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'UPDATE items SET nombre = ?, descripcion = ?, tipo = ? WHERE id = ?';

    db.run(sql, [nombre, descripcion, tipo, id], function(err) {
        if (err) {
            return res.status(500).json({ message: 'Error al actualizar los datos', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Elemento no encontrado' });
        }
        res.json({ message: 'Datos actualizados correctamente' });
    });
});

// Ruta para eliminar un elemento
app.delete('/api/items/:id', (req, res) => {
    const id = req.params.id;
    const sql = 'DELETE FROM items WHERE id = ?';

    db.run(sql, id, function(err) {
        if (err) {
            return res.status(500).json({ message: 'Error al eliminar los datos', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Elemento no encontrado' });
        }
        res.json({ message: 'Datos eliminados correctamente' });
    });
});

// Manejo del cierre de la base de datos al terminar
process.on('SIGINT', () => {
    db.close((err) => {
        if (err) {
            console.error('Error al cerrar la base de datos:', err.message);
        }
        console.log('Conexión a la base de datos SQLite cerrada.');
        process.exit(0);
    });
});

// Iniciar el servidor
app.listen(PORT, () => {
    console.log(`Servidor corriendo en http://localhost:${PORT}`);
});