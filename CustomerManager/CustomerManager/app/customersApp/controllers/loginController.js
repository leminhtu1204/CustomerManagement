'use strict';

define(['app'], function (app) {

    var injectParams = ['$location', '$routeParams', 'authService'];

    var LoginController = function ($location, $routeParams, authService) {
        var vm = this,
            path = '/';

        vm.email = null;
        vm.password = null;
        vm.errorMessage = null;

        vm.login = function () {
            authService.login(vm.email, vm.password).then(function (loggedInData) {
                //$routeParams.redirect will have the route
                //they were trying to go to initially
                if (!loggedInData) {
                    vm.errorMessage = 'Unable to login';
                    return;
                }

                if (loggedInData && loggedInData.isAdmin) {
                    if (!!loggedInData && $routeParams && $routeParams.redirect) {
                        path = path + $routeParams.redirect;
                        return;
                    } 
                    path = path + "customers";
                } else {
                    if (!!loggedInData && $routeParams && $routeParams.redirect && $routeParams.redirect !== "customers") {
                        path = path + $routeParams.redirect;
                        return;
                    }
                    path = path + "cameras";
                }

                $location.path(path);
            });
        };
    };

    LoginController.$inject = injectParams;

    app.register.controller('LoginController', LoginController);

});