(function () {
    'use strict';

    var RBApp = angular.module('RBApp', []);


    RBApp.controller('RBAppCtrl', ['$scope', '$timeout', '$http',function RBAppCtrl($scope, $timeout, $http) {
        var vm = this;
        vm.cards = [];
        vm.workspaces = {};

        $http.post(baseUrl + 'Home/GetWorkspacesData')
            .then(function (response) {
                vm.workspaces = response.data;
            });

        $http.post(baseUrl + 'Home/GetSummary')
            .then(function (response) {
                vm.cards = response.data;
            });

        function userTests() {
            $http.get(baseUrl + 'api/User/GetAll')
                .then(function (response) {
                    console.log(response);
                });
            var usersToUpdate = [
                {
                    ArtifactID: 9,
                    PricePerHour: new Date().getSeconds()
                }
            ];
            $http.post(baseUrl + 'api/User/UpdatePrices', usersToUpdate)
                .then(function (response) {
                    console.log(response);
                });
        }
        userTests();
        vm.hub = $.connection.mainHub;
        
        vm.hub.client.broadCastMessage = function (message) {
            console.log(message);
            $scope.$apply(function () {
                vm.workspaces = message;
            });
        }
        vm.hub.client.instanceSummary = function (message) {
            console.log(message);
            $scope.$apply(function () {
                //vm.workspaces = message;
            });
        }

        $.connection.hub.start();
    }]);
})();
