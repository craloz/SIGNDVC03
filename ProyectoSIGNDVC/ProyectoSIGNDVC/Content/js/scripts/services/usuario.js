angular.module('DVCApp').service('usuarioService', function (BASEURL, $http, $q) {
    var self = this;
    self.selected = {};

    self.update = function (input) {
        var deferred = $q.defer();
        $http.put(BASEURL +'/Configuration/EditarUsuario', input)
        .then(
                function (response) {
                   deferred.resolve(response);
                },
                function (response) {
                    deferred.reject(response);
                }
            );
            return deferred.promise;
    };

    self.all = function () {
        var deferred = $q.defer();
        $http.get(BASEURL + '/Configuration/GetAllUsuarios')
            .then(
            function (response) {
                deferred.resolve(response.data);
            },
            function (response) {
                deferred.reject(response.data);
            }
            );
        return deferred.promise;
    };

    self.delete = function (usuario) {
        var deferred = $q.defer();
        $http.get(BASEURL + '/Configuration/DeleteUsuario?usuario='+usuario.usuario)
            .then(
            function (response) {
                deferred.resolve(response.data);
            },
            function (response) {
                deferred.reject(response.data);
            }
            );
        return deferred.promise;
    };

    self.get = function (usuarioId) {
        var deferred = $q.defer();
        $http.get(BASEURL + '/Configuration/GetUsuario?usuario=' + usuarioId)
            .then(
            function (response) {
                deferred.resolve(response.data);
            },
            function (response) {
                deferred.reject(response.data);
            }
            );
        return deferred.promise;
    };

        
 });


