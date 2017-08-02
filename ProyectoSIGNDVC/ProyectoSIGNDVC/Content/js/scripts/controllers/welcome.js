angular.module('DVCApp').controller('WelcomeCtrl', function ($scope, usuarioService) {
    
    usuarioService.all().then(function (response) {
        console.log(response);
    }, function (error) {
        console.log(error);
    });
});