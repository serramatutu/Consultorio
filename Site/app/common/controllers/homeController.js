'use strict';
app.controller('homeController', ['$scope', function ($scope) {
  document.getElementById('homev').style.height = window.innerWidth <= 991 ? "0%" : "100%";
}]);

window.addEventListener("resize",
  function()
  {
    document.getElementById('homev').style.height = window.innerWidth <= 991 ? "0%" : "100%";
  });
