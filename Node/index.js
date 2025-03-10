const express = require('express');
const path = require('path');
const app = express();
const port = 8000;

// Указываем путь к папке с собранным проектом Unity
// app.use((req, res, next) => {
//     if (req.url.match(/\.(data|wasm|jsd)\.gz$/)) {
//       res.set({
//         "Content-Encoding": "gzip",
//         "Content-Type": req.url.endsWith(".wasm.gz") ? "application/wasm" : "application/octet-stream"
//       });
//     }
//     next();
//   });
app.use(express.static('F:\\Unity\\FarmingGameClient\\Build'));

app.get('/', (req, res) => {
    res.sendFile('F:\\Unity\\FarmingGameClient\\Build', 'index.html');
});

app.listen(port, '0.0.0.0' , () => {
    console.log(`Server is running on http://localhost:${port}`);
});