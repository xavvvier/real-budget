(function () {
    'use strict';

    var RBApp = angular.module('RBApp', []);


    RBApp.controller('RBAppCtrl', function RBAppCtrl($scope, $timeout, $http) {
        var vm = this;
        vm.cards = [];
        vm.workspaces = {};

        $http.post(GetWorkspacesSummaryURL)
            .then(function (response) {
                vm.workspaces = response.data;
            });

        $http.post(GetSummaryURL)
            .then(function (response) {
                vm.cards = response.data;
            });

        vm.hub = $.connection.mainHub;
        
        vm.hub.client.broadCastMessage = function (message) {
            console.log(message);
            $scope.$apply(function () {
                vm.workspaces = message;
            });
        }

        $.connection.hub.start();
    });
})();
