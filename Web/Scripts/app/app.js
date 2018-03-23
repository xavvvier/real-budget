(function () {
    'use strict';

    var RBApp = angular.module('RBApp', []);


    RBApp.controller('RBAppCtrl', ['$scope', '$timeout', '$http', function RBAppCtrl($scope, $timeout, $http) {
        var vm = this;
        vm.cards = [];
        vm.workspaces = {};
        vm.filterUsers = filterUsers;
        vm.users = [];

        $http.post(baseUrl + 'Home/GetWorkspacesData')
            .then(function (response) {
                vm.workspaces = response.data;

                getSummary(vm.workspaces);
              
            });

        function getSummary(workspaces) {
            var list = workspaces;
            var CostDay = 0;
            var ViewsHour = 0;
            var ViewsHourBadge = 0;
            var AverageTime = 0;
            var EditsHour = 0;
            var EditsHourBadge = 0;

            for (var i = 0; i < list.length; i++) {
                CostDay += list[i].CostDay;
                ViewsHour += list[i].ViewsHour;
                ViewsHourBadge += list[i].ViewsHourBadge;
                AverageTime += list[i].AverageTime;
                EditsHour += list[i].EditsHour;
                EditsHourBadge += list[i].EditsHourBadge;
            }

            vm.cards.CostDay = CostDay;
            vm.cards.ViewsHour = ViewsHour;
            vm.cards.ViewsHourBadge = ViewsHourBadge;
            vm.cards.AverageTime = AverageTime;
            vm.cards.EditsHour = EditsHour;
            vm.cards.EditsHourBadge = EditsHourBadge;
        }

    //function userTests() {
    //    $http.get(baseUrl + 'api/User/GetAll')
    //        .then(function (response) {
    //            console.log(response);
    //        });
    //    var usersToUpdate = [
    //        {
    //            ArtifactID: 9,
    //            PricePerHour: new Date().getSeconds()
    //        }
    //    ];
    //    $http.post(baseUrl + 'api/User/UpdatePrices', usersToUpdate)
    //        .then(function (response) {
    //            console.log(response);
    //        });
    //}
    //userTests();

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
            getSummary(message);

        });
    }

        $.connection.hub.start();
}]);
}) ();
