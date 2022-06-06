
var socket = io("http://localhost:3000");

socket.on('pokemon', function (data) {
    console.log("msg ", data);
});

//(
//    function () {
//        var socket = io("http://localhost:3000");

//        window.Socket = {
//            GetStars: () => {
//                socket.on('pokemon', function (data) {

//                    console.log("msg ", data);
//                    //var item = document.createElement('li');
//                    //item.textContent = data.data.message != undefined ? data.data.message : data;
//                    //messages.appendChild(item);
//                    //window.scrollTo(0, document.body.scrollHeight);
//                });
//            }
//        }
//    }
//)