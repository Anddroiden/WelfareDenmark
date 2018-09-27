var state = 'stopped';
var startButton = document.getElementById("startButton");
startButton.addEventListener("click", onStartClick);
var startTime;
var timeElapsed;

function start() {
    state = 'stopped';
    setTimeout(waitToReady, 3000);
}
function end() {
    state = 'stopped'
}

function react() {
    state = 'stopped'
    timeElapsed = new Date() - startTime;
    document.getElementById("timeDisplay").innerHTML = timeElapsed;
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
        alert("you clicked too early")
    }

    if (state === 'ready') {
        react();
    }
}

