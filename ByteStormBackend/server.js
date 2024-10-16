const express = require('express');
const bodyParser = require('body-parser');
const sqlite3 = require('sqlite3').verbose();
const cors = require('cors');

const app = express();
const PORT = 5056;

// Middleware
app.use(cors());
app.use(bodyParser.json());

// Conexión a la base de datos SQLite
const db = new sqlite3.Database('BytestormSQL', (err) => {
    if (err) {
        console.error('Error al conectar a la base de datos:', err.message);
    } else {
        console.log('Conectado a la base de datos SQLite.');
    }
});

db.run(`
    CREATE TABLE IF NOT EXISTS items (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        nombre TEXT NOT NULL,
        descripcion TEXT NOT NULL,
        tipo TEXT NOT NULL,
        equipoCodigo TEXT,
        Mision TEXT
    );
`, (err) => {
    if (err) {
        console.error('Error al crear la tabla:', err.message);
    } else {
        console.log('Tabla "items" verificada/existente.');
    }
});

// Crear un nuevo item (equipo, misión o operativo)
app.post('/api/crear', (req, res) => {
    const { nombre, descripcion, tipo, codigo, Mision } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'INSERT INTO items (nombre, descripcion, tipo, equipoCodigo, Mision) VALUES (?, ?, ?, ?, ?)';
    db.run(sql, [nombre, descripcion, tipo, equipoCodigo || null, Mision || null], function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al insertar los datos', error: err.message });
        }
        res.status(201).json({ message: 'Datos insertados correctamente', id: this.lastID });
    });
});

// Obtener todos los elementos
app.get('/api/items', (req, res) => {
    const sql = 'SELECT * FROM items';
    db.all(sql, [], (err, rows) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener los datos', error: err.message });
        }
        res.json(rows);
    });
});

// Obtener un elemento por ID (operativos)
app.get('/api/operativo/:id', (req, res) => {
    const id = req.params.id;
    const sql = 'SELECT * FROM items WHERE id = ? AND tipo = "Operativo"';

    db.get(sql, [id], (err, row) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener el operativo', error: err.message });
        }
        if (!row) {
            return res.status(404).json({ message: 'Operativo no encontrado' });
        }
        res.json(row);
    });
});

// Obtener un elemento por código (misiones y equipos)
app.get('/api/equipo/:equipoCodigo', (req, res) => {
    const equipoCodigo = req.params.equipoCodigo;
    const sql = 'SELECT * FROM items WHERE equipoCodigo = ? AND tipo = "Equipo"';

    db.get(sql, [equipoCodigo], (err, row) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener el equipo', error: err.message });
        }
        if (!row) {
            return res.status(404).json({ message: 'Equipo no encontrado' });
        }
        res.json(row);
    });
});

app.get('/api/mision/:codigo', (req, res) => {
    const codigo = req.params.codigo;
    const sql = 'SELECT * FROM items WHERE codigo = ? AND tipo = "Misión"';

    db.get(sql, [Codigo], (err, row) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener la misión', error: err.message });
        }
        if (!row) {
            return res.status(404).json({ message: 'Misión no encontrada' });
        }
        res.json(row);
    });
});

// Actualizar un operativo por ID
app.put('/api/operativo/:id', (req, res) => {
    const id = req.params.id;
    const { nombre, descripcion, tipo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'UPDATE items SET nombre = ?, descripcion = ?, tipo = ? WHERE id = ? AND tipo = "Operativo"';
    db.run(sql, [nombre, descripcion, tipo, id], function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al actualizar los datos', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Operativo no encontrado' });
        }
        res.json({ message: 'Operativo actualizado correctamente' });
    });
});

// Actualizar un equipo por código
app.put('/api/equipo/:equipoCodigo', (req, res) => {
    const equipoCodigo = req.params.equipoCodigo;
    const { nombre, descripcion, tipo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'UPDATE items SET nombre = ?, descripcion = ?, tipo = ? WHERE equipoCodigo = ? AND tipo = "Equipo"';
    db.run(sql, [nombre, descripcion, tipo, equipoCodigo], function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al actualizar el equipo', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Equipo no encontrado' });
        }
        res.json({ message: 'Equipo actualizado correctamente' });
    });
});

// Actualizar una misión por código
app.put('/api/mision/:codigo', (req, res) => {
    const codigo = req.params.codigo;
    const { nombre, descripcion, tipo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'UPDATE items SET nombre = ?, descripcion = ?, tipo = ? WHERE codigo = ? AND tipo = "Misión"';
    db.run(sql, [nombre, descripcion, tipo, codigo], function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al actualizar la misión', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Misión no encontrada' });
        }
        res.json({ message: 'Misión actualizada correctamente' });
    });
});

// Eliminar un operativo por ID
app.delete('/api/operativo/:id', (req, res) => {
    const id = req.params.id;
    const sql = 'DELETE FROM items WHERE id = ? AND tipo = "Operativo"';

    db.run(sql, id, function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al eliminar el operativo', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Operativo no encontrado' });
        }
        res.json({ message: 'Operativo eliminado correctamente' });
    });
});

// Eliminar un equipo por código
app.delete('/api/equipo/:equipoCodigo', (req, res) => {
    const equipoCodigo = req.params.equipoCodigo;
    const sql = 'DELETE FROM items WHERE equipoCodigo = ? AND tipo = "Equipo"';

    db.run(sql, equipoCodigo, function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al eliminar el equipo', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Equipo no encontrado' });
        }
        res.json({ message: 'Equipo eliminado correctamente' });
    });
});

// Eliminar una misión por código
app.delete('/api/mision/:codigo', (req, res) => {
    const codigo = req.params.codigo;
    const sql = 'DELETE FROM items WHERE codigo = ? AND tipo = "Misión"';

    db.run(sql, codigo, function (err) {
        if (err) {
            return res.status(500).json({ message: 'Error al eliminar la misión', error: err.message });
        }
        if (this.changes === 0) {
            return res.status(404).json({ message: 'Misión no encontrada' });
        }
        res.json({ message: 'Misión eliminada correctamente' });
    });
});

// Cierre de la base de datos
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