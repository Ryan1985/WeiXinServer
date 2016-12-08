(function () {
    'use strict';

    angular.module('app', [
        // Angular modules
        'ngAnimate',
        'ngRoute'

        // Custom modules

        // 3rd Party Modules
        
    ]).config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }]).controller('appController', ['$scope', '$location','$http', function($scope, $location,$http) {


        $scope.viewModel = {
            DepartureTime: new Date(),
            Departure:'上海南站'
        };


        $scope.refresh = function() {
            $http.get('/api/Order?OpenId=' + $location.search().openid)
                .success(function(response) {
                    if (response.HasError == true) {
                        alert(response.ErrorMessage);
                    } else {
                        if (response.QueryResult.Id != 0) {
                            $scope.viewModel = response.QueryResult;
                            $scope.viewModel.DepartureTime = new Date($scope.viewModel.DepartureTime);
                        }
                    }
                });
        };


        $scope.Save = function () {
            $scope.viewModel.OpenId = $location.search().openid;
            $http.post("/api/Order", $scope.viewModel)
            .success(function (response) {
                if (response.HasError == true) {
                    alert(response.ErrorMessage);
                } else {
                    alert('保存成功');
                    $scope.refresh();
                }
            })
            .error(function (data) {
                alert(data);
            });
        };

        $scope.refresh();
    }]);
})();
