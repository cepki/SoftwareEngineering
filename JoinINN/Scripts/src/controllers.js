angular.module('joinin')
.controller('mainController', function ($scope, $http, ngDialog, $rootScope, $cookies) {
    $scope.limit = 4;
    var allGroups = [];

    $http.get('api/CitiesApi/GetAllCities')
    .then(function (result) {
        $scope.allCities = result.data;
        var city = {
            Id: 1000,
            Name:"Svi"
        }
        $scope.allCities.push(city);
        $scope.chosenCity = city;
    })


    $http.get('api/AffinitiesApi/GetAllAffinites')
    .then(function (result) {
        $scope.allAffinities = result.data;
        var affinity = {
            Id: 1000,
            Name: "Bilokoji"
        }
        $scope.allAffinities.push(affinity);
        $scope.chosenAffinity = affinity;
    })

    $http.get('api/SocialGroupsApi/GetAllGroups')
    .then(function (result) {
        $scope.groupsToShow = result.data;
        angular.forEach(result.data, function (value) {
            allGroups.push(value);
        })

        $scope.areAllLoaded = ($scope.limit >= result.data.length);
    })

    $scope.readAll = function () {
        $scope.limit = $scope.groupsToShow.length;
        $scope.areAllLoaded = !$scope.areAllLoaded;
    }

    $scope.nesto = function(user)
    {
        ngDialog
            .open({
                templateUrl: '/HtmlTemplates/third.html',
                controller: 'thirdController',
                data: user,
            })

    }

    $scope.idemo = function()
    {
        console.log($cookies.getObject("logedUser"));
    }

    $scope.FindWithThisCondition = function()
    {
        $scope.groupsToShow.splice(0, $scope.groupsToShow.length);
        $scope.limit = 4;
        if($scope.searchedText == null || $scope.searchedText == undefined)
        {
            angular.forEach(allGroups, function (value) {
                if(($scope.chosenCity.Id == 1000 || ($scope.chosenCity.Id == value.CityId)) && (($scope.chosenAffinity.Id == 1000 || value.AffinityTypes.some(hasThisElement))))
                {
                    $scope.groupsToShow.push(value);
                }
                else
                {

                }
            })
        }
        else
        {
            angular.forEach(allGroups, function (value) {
                if (($scope.chosenCity.Id == 1000 || ($scope.chosenCity.Id == value.CityId)) && (($scope.chosenAffinity.Id == 1000 || value.AffinityTypes.some(hasThisElement))) && ((value.GroupName).toLowerCase()).indexOf(($scope.searchedText).toLowerCase()) >= 0) {
                    $scope.groupsToShow.push(value);
                }
                else {
                }
            })
        }

        $scope.areAllLoaded = ($scope.groupsToShow.length > 4) ? false : true;
    }

    function hasThisElement(element) {
        return element.Id == $scope.chosenAffinity.Id;
    }

})

.controller('homeController', function () {
})

.controller('secondController', function ($scope, $http) {

    var affinities = [];
    $scope.isschool = true.toString();

    $http.get('api/CitiesApi/GetAllCities')
    .then(function(result)
    {
        $scope.cities = result.data;
        $scope.chosenCity = result.data[0];
    })


    $http.get('api/AffinitiesApi/GetAllAffinites')
    .then(function (result) {
        $scope.affinities = result.data;
    })

    
    $scope.submitUserForm = function () {
        console.log("Ide submit");

        var user = {
            Username: $scope.username,
            Name: $scope.groupname,
            ContactNumber: $scope.contactnumber,
            EmailAddress: $scope.emailaddress,
            OfficialWebUrl: $scope.officialweburl,
            FacebookPageUrl: $scope.facebookpageurl,
            Description: $scope.description,
            GroupEmailAddress: $scope.groupemailaddress,
            IsSchool: ($scope.isschool == "true"),
            IsAssociation: !($scope.isschool == "true"),
            CityId: $scope.chosenCity.Id,
            AffinityTypes: affinities,
            photoUrl: $scope.photourl
        }


        $http.post('api/SocialGroupsApi/MakeNewUser', user)
        .success(function () {
            console.log("Uspilo");
        })
        .error(function () {
            console.log("Nije uspilo");
        })
    }

    $scope.addAffinity = function(affinity)
    {
        if(affinities.indexOf(affinity) > -1)
        {
            affinities.splice(affinities.indexOf(affinity), 1);
        }
        else
        {
            affinities.push(affinity);
        }

        console.log(affinities);
    }

})

.controller('thirdController', function ($scope, $http, $routeParams) {

    console.log($scope.ngDialogData);
    
})

.controller('loginController', function ($scope, $http, $rootScope, $location, $cookies) {
    $scope.login = function()
    {
        var user = {
            username: $scope.username,
            password: $scope.password
        }

        $http.post('/api/LoginApi/SignIn', user)
        .success(function () {
            $http.get('/api/LoginApi/WhoAmI')
            .then(function(result){
                $cookies.putObject("logedUser", result.data);
                $location.path("/");
            })
        })
        .error(function () {
            alert("Krivi login");
            $location.path("/");
        })
    }

})

.controller('adminController', function ($scope, $http, $cookies, $location) {
    var allGroups = [];

    $http.get('api/UsersApi/GetAllGroups')
    .then(function (result) {
        $scope.groupsToShow = result.data;
        angular.forEach(result.data, function (value) {
            allGroups.push(value);
        })
    })

    $scope.Delete = function(user)
    {
        //console.log("Go");
        $http.get('api/AdminApi/AdminDeleteGroup/' + user.Id)
        .then(function (result) {
            $scope.groupsToShow.splice($scope.groupsToShow.indexOf(user), 1);
            allGroups.splice($scope.groupsToShow.indexOf(user), 1);
        })
    }
})