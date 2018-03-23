(function () {
    'use strict';

    var RBApp = angular.module('RBApp', []);


    RBApp.controller('RBAppCtrl', ['$scope', '$timeout', '$http',function RBAppCtrl($scope, $timeout, $http) {
        var vm = this;
        vm.cards = [];
        vm.workspaces = {};
        vm.filterUsers = filterUsers;
        vm.users = [];

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

        function filterUsers(ArtifactId) {
            var index = categoryIndex(ArtifactId);
            vm.users = vm.workspaces[index].User;
        }

        function categoryIndex(ArtifactId) {
            var index;
            for (var i = 0; i < vm.workspaces.length; ++i) {
                if (vm.workspaces[i].WorkspaceArtifactId == ArtifactId) {
                    index = i;
                    break;
                }
            }
            return index;
        }

        vm.hub = $.connection.mainHub;
        
        vm.hub.client.broadCastMessage = function (message) {
            $scope.$apply(function () {
                vm.workspaces = message;
            });
        }
        vm.hub.client.instanceSummary = function (message) {
            $scope.$apply(function () {
                vm.card = message;
            });
        }

        $.connection.hub.start();
    }]);
})();
