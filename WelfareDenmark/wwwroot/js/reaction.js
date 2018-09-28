(function () {
    
    /** @type string*/ 
    var state = 'stopped';
    
    /** @type Date*/ 
    var startTime;
    
    /** @type Date*/ 
    var timeElapsed;
    
    /** @type HTMLElement*/
    var timeDisplay = document.getElementById("timeDisplay");
    
    /** @type HTMLElement*/
    var startButton = document.getElementById("startButton");
    
    startButton.addEventListener("click", onStartClick);
    
    function start() {
        state = 'waiting';
        setTimeout(waitToReady, 3000);
    }

    function end() {
        state = 'stopped';
    }

    function react() {
        state = 'stopped';
        timeElapsed = new Date() - startTime;
        timeDisplay.innerHTML = 'Your reaction time was ' + timeElapsed;
    }

    function waitToReady() {
        state = 'ready';
        startTime = new Date();
    }

    function onStartClick() {
        if (state === 'stopped') {
            start();
        }

        if (state === 'waiting') {
            end();
            console.log("you clicked too early");

        }

        if (state === 'ready') {
            react();
        }
    }
})();

