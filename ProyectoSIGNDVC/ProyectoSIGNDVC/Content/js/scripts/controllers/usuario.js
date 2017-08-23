angular.module('DVCApp').controller('usuarioCtrl', function ($scope, $q, $location, usuarioService) {
    $scope.usuarios = [];

    $scope.usuario = '';

    $scope.loadAllUsuario = function () {
        usuarioService.all().then(function (response) {
            $scope.usuarios = response;
        }, function (error) {
            //console.log(error);
        });
    };

    $scope.deleteUsuario = function (usuario) {
        usuarioService.delete(usuario).then(function (response) {
            $scope.loadAllUsuario();
        }, function (error) {
            console.log(error);
        });
    };

    $scope.editUsuario = function (usuario) {
        //usuarioService.setSelectedUsuario(usuario);
        //console.log(usuarioService.getSelectedUsuario());
        window.location.replace("/Configuration/EditarUsuario?usuario="+usuario.usuario);
    };

    $scope.loadUsuario = function () {
        console.log($location.search());
        /*usuarioService.get($location.search().usuario).then(function (response) {
            $scope.usuario = response;
            console.log($scope.usuario);
        }, function (error) {
            //console.log(error);
        });*/
    };

    $scope.loadAllUsuario();
    //$scope.loadUsuario();
});