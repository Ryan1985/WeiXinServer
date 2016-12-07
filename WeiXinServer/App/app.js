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
        var openId = $location.search().openId;
        $http.get('/api/User?paramsString=' + JSON.stringify({ 'OpenId': openId }))
            .success(function(response) {
                $scope.userList = response;
            });
    }]);
})();
