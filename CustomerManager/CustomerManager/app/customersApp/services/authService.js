'use strict';

define(['app'], function (app) {

    var injectParams = ['$http', '$rootScope'];

    var authFactory = function ($http, $rootScope) {
        var serviceBase = '/api/dataservice/',
            factory = {
                loginPath: '/login',
                user: {
                    isAuthenticated: false,
                    roles: null
                }
            };

        factory.login = function (username, password) {
            var userLogin = { userName: username, password: password };
            return $http.post(serviceBase + 'login', JSON.stringify(userLogin)).then(
                function (results) {
                    var loggedInData = results.data;
                    changeAuth(!!loggedInData);
                    return loggedInData;
                });
        };

        factory.logout = function () {
            changeAuth(false);
            return false;
        };

        factory.redirectToLogin = function () {
            $rootScope.$broadcast('redirectToLogin', null);
        };

        function changeAuth(loggedIn) {
            factory.user.isAuthenticated = loggedIn;
            $rootScope.$broadcast('loginStatusChanged', loggedIn);
        }

        return factory;
    };

    authFactory.$inject = injectParams;

    app.factory('authService', authFactory);

});
