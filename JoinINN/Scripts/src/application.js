angular.module('joinin', ['ngRoute', 'angularValidator', 'ngDialog', 'ngCookies'])
.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', {
        controller: 'mainController',
        templateUrl: '/HtmlTemplates/main.html'
    })

    $routeProvider.when('/makeProfile', {
        controller: 'secondController',
        templateUrl: '/HtmlTemplates/second.html'
    })

    $routeProvider.when('/udruga/:Id', {
        controller: 'thirdController',
        templateUrl: '/HtmlTemplates/third.html'
    })

    $routeProvider.when('/login', {
        controller: 'loginController',
        templateUrl: '/HtmlTemplates/login.html'
    })

    $routeProvider.when('/bravoDiana', {
        templateUrl: '/HtmlTemplates/diana.html',
        controller: 'bravoDianaController'
    })

    $routeProvider.when('/adminPage', {
        templateUrl: '/HtmlTemplates/admin.html',
        controller: 'adminController'
    })
})