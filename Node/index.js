const express = require('express');
const path = require('path');
const app = express();
const port = 8000;


const staticFolderPath = path.join(__dirname, 'Build'); 
app.use(express.static(staticFolderPath));

app.get('/', (req, res) => {
    res.sendFile(path.join(staticFolderPath, 'index.html'));
});

app.listen(port, '0.0.0.0', () => {
    console.log(`Server is running on http://localhost:${port}`);
});