// const app = require('express')();
// const http = require('http').Server(app);
// const io = require('socket.io')(http);
// const port = process.env.PORT || 3000;

// app.get('/', (req, res) => {
//   res.sendFile(__dirname + '/index.html');
// });

// io.on('connection', (socket) => {
//   socket.on('chat message', msg => {
//     io.emit('chat message', msg);
//     console.log('chat message', msg)
//   });
// });

// http.listen(port, () => {
//   console.log(`Socket.IO server running at http://localhost:${port}/`);
// });

// const app = require('express')();
// const http = require('http').Server(app);
// const io = require('socket.io')(http);
// const port = process.env.PORT || 3000;
// require('dotenv').config();
const { callbackify } = require('util');
const https = require('http');
const server = require('http').Server();
const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const Redis = require('ioredis');
const redis = new Redis({host:'cache_app', port:6379});

// const redis = new Redis({host: process.env.REDIS_HOST, password: process.env.REDIS_PASSWORD});
const axios = require('axios').default;

const httpClient = axios.create({
    httpsAgent: new https.Agent({
        rejectUnauthorized: false
    })
});

redis.subscribe('deal_talk');

function processApiMessages(channel, message) {
    const data = JSON.parse(message);

    if ('room' in data) {
        logEventOnConsole(data);
        // io.emit('deal_talk', data);
        
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
    
    socket.on('deal_talk', (message) => {
      io.emit('deal_talk',  message.data)
    });

    io.on('connection', (socket) => {
      socket.on('deal_talk', msg => {
        io.emit('deal_talk', msg);
        console.log('deal_talk', msg)
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
  socket.on('deal_talk', msg => {
    io.emit('deal_talk', msg);
    console.log('deal_talk', msg)
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
