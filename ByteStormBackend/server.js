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
        EquipoCodigo TEXT,
        misionCodigo TEXT
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
    const { nombre, descripcion, tipo, EquipoCodigo, misionCodigo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'INSERT INTO items (nombre, descripcion, tipo, EquipoCodigo, misionCodigo) VALUES (?, ?, ?, ?, ?)';
    db.run(sql, [nombre, descripcion, tipo, EquipoCodigo || null, misionCodigo || null], function (err) {
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
app.get('/api/equipo/:EquipoCodigo', (req, res) => {
    const EquipoCodigo = req.params.EquipoCodigo;
    const sql = 'SELECT * FROM items WHERE EquipoCodigo = ? AND tipo = "Equipo"';

    db.get(sql, [EquipoCodigo], (err, row) => {
        if (err) {
            return res.status(500).json({ message: 'Error al obtener el equipo', error: err.message });
        }
        if (!row) {
            return res.status(404).json({ message: 'Equipo no encontrado' });
        }
        res.json(row);
    });
});

app.get('/api/mision/:misionCodigo', (req, res) => {
    const misionCodigo = req.params.misionCodigo;
    const sql = 'SELECT * FROM items WHERE misionCodigo = ? AND tipo = "Misión"';

    db.get(sql, [misionCodigo], (err, row) => {
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
app.put('/api/equipo/:EquipoCodigo', (req, res) => {
    const EquipoCodigo = req.params.EquipoCodigo;
    const { nombre, descripcion, tipo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'UPDATE items SET nombre = ?, descripcion = ?, tipo = ? WHERE EquipoCodigo = ? AND tipo = "Equipo"';
    db.run(sql, [nombre, descripcion, tipo, EquipoCodigo], function (err) {
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
app.put('/api/mision/:misionCodigo', (req, res) => {
    const misionCodigo = req.params.misionCodigo;
    const { nombre, descripcion, tipo } = req.body;

    if (!nombre || !descripcion || !tipo) {
        return res.status(400).json({ message: 'Todos los campos (nombre, descripción y tipo) son obligatorios.' });
    }

    const sql = 'UPDATE items SET nombre = ?, descripcion = ?, tipo = ? WHERE misionCodigo = ? AND tipo = "Misión"';
    db.run(sql, [nombre, descripcion, tipo, misionCodigo], function (err) {
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
app.delete('/api/equipo/:EquipoCodigo', (req, res) => {
    const EquipoCodigo = req.params.EquipoCodigo;
    const sql = 'DELETE FROM items WHERE EquipoCodigo = ? AND tipo = "Equipo"';

    db.run(sql, EquipoCodigo, function (err) {
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
app.delete('/api/mision/:misionCodigo', (req, res) => {
    const misionCodigo = req.params.misionCodigo;
    const sql = 'DELETE FROM items WHERE misionCodigo = ? AND tipo = "Misión"';

    db.run(sql, misionCodigo, function (err) {
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