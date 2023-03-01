// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myfunc(id) {
    var x = document.getElementsByClassName('sel-label');
    x.classList.remove('sel-label');
  
    document.getElementById("radbtn-".concat(id)).style.border = "2px solid black";
    //document.getElementById("radbtn-".concat(id)).style.backgroundColor = "grey";

}