angular.module('joinin')
.controller('mainController', function ($scope, $http, ngDialog, $rootScope, $cookies) {
    
    console.log($rootScope.logedRole);

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

        console.log(result.data);

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
    var choise = 1;
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
            //Username: $scope.username,
            Name: $scope.groupname,
            Password: $scope.password,
            ContactNumber: $scope.contactnumber,
            EmailAddress: $scope.emailaddress,
            OfficialWebUrl: $scope.officialweburl,
            FacebookPageUrl: $scope.facebookpageurl,
            Description: $scope.description,
            GroupEmailAddress: $scope.groupemailaddress,
            IsSchool: ($scope.isschool == "true"),
            IsAssociation: !($scope.isschool == "true"),
            CityId: $scope.chosenCity.Id,
            AffinityType_Id: choise,
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
   
    $scope.type = function(id)
    {
        choise = id;
    }
})

.controller('thirdController', function ($scope, $http, $routeParams) {

    console.log($scope.ngDialogData);
    
})

.controller('loginController', function ($scope, $http, $rootScope, $location, $cookies, $timeout) {
    $scope.login = function()
    {
        var user = {
            username: $scope.username,
            password: $scope.password
        }

        $http.post('/api/LoginApi/SignIn', user)
        .success(function () {

            $http.get('/api/LoginApi/WhoAmI')
            .then(function (result) {
                $rootScope.logedRole = result.data.Role;
                $cookies.putObject("logedUser", result.data);
                $timeout(function () {
                    $location.path("/");
                },700)
            })
        })
        .error(function () {
            alert("Krivi login");
            $location.path("/");
        })
    }

})

.controller('logoutController', function ($rootScope, $http, $location, $cookies) {
    $http.get('/api/LoginApi/SignOut')
    .success(function () {
        $rootScope.logedRole = null;
        $cookies.putObject("logedUser", null);
        $location.path('/');
    })
})

.controller('adminController', function ($scope, $http, $cookies, $location) {
    var allGroups = [];

    $http.get('api/SocialGroupsApi/GetAllGroups')
    .then(function (result) {
        $scope.groupsToShow = result.data;
        angular.forEach(result.data, function (value) {
            allGroups.push(value);
        })
    })

    $scope.Delete = function(user)
    {
        console.log("Go");
        $http.get('api/AdminApi/AdminDeleteGroup/' + user.Id)
        .then(function (result) {
            $scope.groupsToShow.splice($scope.groupsToShow.indexOf(user), 1);
            allGroups.splice($scope.groupsToShow.indexOf(user), 1);
        })
    }
})

.controller('editProfileController', function ($scope, $http) {
    $scope.choise = 0;
    var oldPassword = "STARIPASSWORDJEOVO";
    $scope.test = true;
    //$scope.isschool = true.toString();
    $scope.isOldPasswordCorrect = false;

    $http.get('api/CitiesApi/GetAllCities')
    .then(function (result) {
        $scope.cities = result.data;
        $scope.chosenCity = result.data[0];
    })


    $http.get('api/AffinitiesApi/GetAllAffinites')
    .then(function (result) {
        $scope.affinities = result.data;
    })

    $http.get('api/loginApi/GetLogedUser')
    .then(function (result) {
        
        $scope.password = "";
        //console.log(result.data);

        $scope.groupname = result.data.Name;
        $scope.contactnumber = result.data.ContactNumber;
        $scope.emailaddress = result.data.EmailAddress;
        $scope.officialweburl = result.data.OfficialWebUrl;
        $scope.facebookpageurl = result.data.FacebookPageUrl;
        $scope.description = result.data.Description;
        $scope.isschool = result.data.IsSchool;
        $scope.choise = result.data.AffinityType.Id;
        $scope.id = result.data.Id;
        $scope.photourl = result.data.photoUrl;
        oldPassword = result.data.Password;
    })

    $scope.$watch('password', function (value) { //watch for old password, if it is right, then ng-show="true" for new password
        //console.log("KSKKSAKK")
        //console.log(value);
        //console.log(oldPassword);
        if (value === oldPassword) {
            //console.log("iste su sifre");
            $scope.isOldPasswordCorrect = true; //check if is it right
        }
        else
        {
            $scope.isOldPasswordCorrect = false;  //if password is not defined then for sure it is not oke
        }
    })



    $scope.submitUserForm = function (submitedForm) {


        var newPassword = $scope.isOldPasswordCorrect ? $scope.newPassword : oldPassword;
        //console.log(newPassword);
        var user = {
            Id: $scope.id,
            Name: $scope.groupname,
            Password: newPassword,
            ContactNumber: $scope.contactnumber,
            EmailAddress: $scope.emailaddress,
            OfficialWebUrl: $scope.officialweburl,
            FacebookPageUrl: $scope.facebookpageurl,
            Description: $scope.description,
            GroupEmailAddress: $scope.groupemailaddress,
            AffinityType_Id: $scope.choise,
            photoUrl: $scope.photourl
        }

        console.log(user);

        $http.post('api/SocialGroupsApi/EditGroup', user)
        .success(function () {
            console.log("Uspilo");
        })
        .error(function () {
            console.log("Nije uspilo");
        })
    }

    $scope.type = function (id) {
        $scope.choise = id;
    }


})