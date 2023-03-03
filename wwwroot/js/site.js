
/*
    let searchTextBox = document.querySelector('#searchTextbox');
    searchTextBox.focus();
*/

function switchVideo(key) {
    let player = document.querySelector('#youtubePlayer');
    player.src = "https://www.youtube.com/embed/" + key + "?rel=0&controls=1";

    let videoSelector = document.querySelector('#videoSelector');
    videoSelector.blur();
}