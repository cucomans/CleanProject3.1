﻿$.fn.toggleCheck = function () {
    if (this.tagName === 'INPUT') {
        $(this).prop('checked', !($(this).is(':checked')));
    }

}

document.addEventListener('DOMContentLoaded', function () {

    //var messageInput = document.getElementById('message');

    // Get the user name and store it to prepend to messages.
    var name = prompt('Ingresá tu nick:', '');
    if (name == undefined || name == '') {
        alert('sos pelotudo o te dicen pelado?? INGRESA TU NICK');
        name = prompt('Ingresá tu nick:', '');
        if (name == undefined || name == '') {
            alert('TE FUISTE DESCONECTADO POR PETERO');
            return false;}
    }
    // Set initial focus to message input box.
    // messageInput.focus();
   
    // Start the connection.
   
    var connection = new signalR.HubConnectionBuilder()
        .withUrl('/chat')
        .build();
    

    let onlineCount = document.querySelector('span.online-count');
    let updateCountCallback = function (message) {
        if (!message) return;
        console.log('updateCount = ' + message);
        if (onlineCount) onlineCount.innerText = message ;
    };
    let goldenIniCallBack = function (dic) {
        if (!dic) return;
        console.log(dic);
        $.each(dic, function (key, value) {
            $('#' +key).prop('checked', value);
        });
        //if (onlineCount) onlineCount.innerText = message;
    };
    connection.on('updateCount', updateCountCallback);

    connection.on('goldenIni', goldenIniCallBack);

    // Create a function that the hub can call to broadcast messages.
    connection.on('broadcastMessage', function (name, num, tipo, state) {
        // Html encode display name and message.
        // //console.log('toggle');
        // $(tipo + num).toggle();
        if (state) {
            $('#' + tipo + num).prop('checked', true);
            $('#' + tipo + num).attr('title', "tildado por: " + name);
           
            var encodedName = name + ' tildo ';
            var encodedMsg = tipo + num;
            // Add the message to the page.
            var liElement = document.createElement('li');
            liElement.style.color = 'green';
            liElement.innerHTML = '<strong>' + encodedName + '</strong>:&nbsp;&nbsp;' + encodedMsg;
            document.getElementById('discussion').appendChild(liElement);
        }
        else {
            $('#' + tipo + num).prop('checked', false);
            
            $('#' + tipo + num).attr('title', "destildado por: " + name);
            var encodedName = name + ' destildo ';
            var encodedMsg = tipo + num;
            // Add the message to the page.
            var liElement = document.createElement('li');
            liElement.style.color = 'darkred';
            liElement.innerHTML = '<strong>' + encodedName + '</strong>:&nbsp;&nbsp;' + encodedMsg;
            document.getElementById('discussion').appendChild(liElement);
        }

    });

    // Transport fallback functionality is now built into start.

    connection.start()
        .then(function () {
            connection.invoke('adduser', name);
            document.getElementById('derkon1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('budge1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('budge2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('budge3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('budge4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('budge5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('goblin1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('goblin2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('goblin3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('goblin4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('goblin5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('rabbit1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('rabbit2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('rabbit3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('rabbit4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('rabbit5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('darkknight1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('darkknight2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('darkknight3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('darkknight4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('darkknight5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('soldier1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('soldier2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('soldier3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('soldier4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('soldier5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('titan1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('titan2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('titan3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('titan4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('titan5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('lizard1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('lizard2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('lizard3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('lizard4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('lizard5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            //
            document.getElementById('vepar1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('vepar2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('vepar3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('vepar4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('vepar5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            //
            document.getElementById('tantalo1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('tantalo2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('tantalo3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('tantalo4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('tantalo5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            //
            document.getElementById('wheel1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('wheel2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('wheel3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('wheel4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('wheel5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            //
            document.getElementById('devil1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('devil2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('devil3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('devil4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('devil5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            //
            document.getElementById('crust1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('crust2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('crust3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('crust4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('crust5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            //
            document.getElementById('golem1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('golem2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('golem3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('golem4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('golem5').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            //new
            document.getElementById('satyro1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('satyro2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('satyro3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('satyro4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            document.getElementById('twintale1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('twintale2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('twintale3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('twintale4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            document.getElementById('napin1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('napin2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('napin3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('napin4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            document.getElementById('ironknight1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('ironknight2').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('ironknight3').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
            document.getElementById('ironknight4').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });

            document.getElementById('ggd1').addEventListener('click', function (event) {
                var state = $(this).prop('checked');
                var num = $(this).data('number');
                var tipo = $(this).data('tipo');
                connection.invoke('send', name, num, tipo, state);
            });
           
           

        })
        .catch(error => {
            console.error(error.message);
        });
});