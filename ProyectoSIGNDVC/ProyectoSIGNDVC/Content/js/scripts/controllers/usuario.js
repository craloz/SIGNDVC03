angular.module('DVCApp').controller('usuarioCtrl', function ($scope, $q, usuarioService) {
    $scope.usuarios = [];
    $scope.loadAllUsuario = function () {
        usuarioService.all().then(function (response) {
            $scope.usuarios = response;
        }, function (error) {
            console.log(error);
        });
    };

    $scope.deleteUsuario = function (usuario) {
        usuarioService.delete(usuario).then(function (response) {
            
        }, function (error) {
            console.log(error);
        });
    };

    $scope.editUsuario = function (usuario) {
        console.log(usuario);
    };

    $scope.loadAllUsuario();
});