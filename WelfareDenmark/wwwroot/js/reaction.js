(function () {
    
    /** @type string*/ 
    var state;
    
    /** @type Date*/ 
    var startTime;
    
    /** @type Date*/ 
    var timeElapsed;
    
    /** @type HTMLElement*/
    var timeDisplay = document.getElementById("timeDisplay");
    
    /** @type HTMLElement*/
    var startButton = document.getElementById("startButton");
    var timeout;
    
    changeState('stopped');
    startButton.addEventListener("click", onStartClick);
    
    function changeState(newState){
        state = newState;
        startButton.className = newState;
    }

    function start() {
        
        changeState('waiting')
        timeout = setTimeout(waitToReady, 3000);
    }

    function end() {
        changeState('stopped');
        clearTimeout(timeout);
    }

    function react() {
        changeState('stopped')
        timeElapsed = new Date() - startTime;
        timeDisplay.innerHTML = 'Your reaction time was ' + timeElapsed + ' milliseconds';
    }

    function waitToReady() {
        changeState('ready');
        startTime = new Date();
    }

    function onStartClick() {
        if (state === 'stopped') {
            start();
        }

        else if (state === 'waiting') {
            end();
            alert("you clicked too early");
        }

        else if (state === 'ready') {
            react();
            
        }
    }
})();

