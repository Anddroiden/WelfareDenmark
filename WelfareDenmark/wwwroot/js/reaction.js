(function () {
    var stateTypes = {
        stopped: "stopped",
        waiting: "waiting",
        ready: "ready"
    };
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
    var iteration = 0;
    var results = [];
    changeState(stateTypes.stopped);
    startButton.addEventListener("click", onStartClick);
    
    function saveResult() {
        /** @type HTMLInputElement*/
        var $score = document.getElementById("Score");
        /** @type HTMLFormElement*/
        var $form = document.getElementById("Form");
        $score.value = results.reduce(add) / results.length;
        $form.submit();
    }
    function add(a, b) {
        return a + b
    }
    function renderResults() {
        var avgTimeElapsed = results.reduce(add) / results.length;
        timeDisplay.innerHTML = '';
        var $li_ = document.createElement("li");
        $li_.innerHTML = 'Your average reaction time was ' + avgTimeElapsed + ' milliseconds';
        timeDisplay.appendChild($li_);
        for (var index = 0; index < results.length; index++) {
            var timeElapsed = results[index];
            var $li = document.createElement("li");
            $li.innerHTML = 'Your reaction time was ' + timeElapsed + ' milliseconds';
            timeDisplay.appendChild($li);
        }
    }

    function changeState(newState) {
        state = newState;
        startButton.className = newState;
    }

    function start() {
        changeState(stateTypes.waiting)
        timeout = setTimeout(waitToReady, RNG());
    }

    function end() {
        changeState(stateTypes.stopped);
        clearTimeout(timeout);
    }

    function react() {
        iteration++;
        timeElapsed = new Date() - startTime;
        timeDisplay.innerHTML = 'Your reaction time was ' + timeElapsed + ' milliseconds';
        results.push(timeElapsed);
        renderResults();
        if (iteration <= 3) {
            start();
        } else {
            end();
            saveResult();
        }
    }

    function waitToReady() {
        changeState(stateTypes.ready);
        startTime = new Date();
    }

    function onStartClick() {
        if (state === stateTypes.stopped) {
            start();
        }
        else if (state === stateTypes.waiting) {
            end();
            alert("you clicked too early");
        }
        else if (state === stateTypes.ready) {
            react();
        }
    }

    /**
     * @return {number}
     */
    function RNG() {
        return 2500 + Math.random() * 1000;
    }

})();

