const { callbackify } = require('util');
const https = require('http');
const server = require('http').Server();
const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http,{
  cors: {
    origin: "*",
    methods: ["GET", "POST"]
  }
});
const Redis = require('ioredis');
const redis = new Redis({host:'cache_app', port:6379});

// const redis = new Redis({host: process.env.REDIS_HOST, password: process.env.REDIS_PASSWORD});
const axios = require('axios').default;

const httpClient = axios.create({
    httpsAgent: new https.Agent({
        rejectUnauthorized: false
    })
});
// io.set('origins','*:*');
redis.subscribe('pokemon');

function processApiMessages(channel, message) {
    const data = JSON.parse(message);

    if ('room' in data) {
        logEventOnConsole(data);
        io.emit('pokemon', data);
        
        io.to(data.room).emit(data.event, data.data);
    }
}

function onConnect(socket) {
    socket.join(socket.userRoom);
    handleEvents(socket);
}

function handleEvents(socket) {
    socket.on('message', (message) => processMessage(socket, message));
}

function processMessage(socket, message) {
    logEventOnConsole(message);
    
    socket.on('pokemon', (message) => {
      io.emit('pokemon_post',  message.data)
    });

    io.on('connection', (socket) => {
      socket.on('pokemon', msg => {
        io.emit('pokemon_post', msg);
        console.log('pokemon_post', msg)
      });
    });
    socket.broadcast.to(message.room).emit(message.event, message.data);
}

function logEventOnConsole(message) {
    console.log('\n---------------------------');
    console.log(`Sala: ${message.room}`);
    console.log(`\tEvento: ${message.event}`);
    console.log(`\tObjeto: ${JSON.stringify(message.data)}`);
}

io.on('connection', (socket) => {
  socket.on('pokemon', msg => {
    io.emit('pokemon_post', msg);
    console.log('pokemon_post', msg)
  });
});

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});


redis.on('message', processApiMessages);
// io.use(callbackify(authMiddleware));
io.sockets.on('connection', onConnect);

// server.listen(3001);
http.listen(3000, () => {
  console.log(`Socket.IO server running at http://localhost:3000/`);
});
