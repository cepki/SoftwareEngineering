angular.module('joinin')
.controller('mainController', function ($scope, $http, ngDialog, $rootScope, $cookies, $location) {
    
    console.log($rootScope.logedRole);

    $scope.userToShowForFirst = {
        photoUrl: "http://www.seven46.com/wp-content/uploads/2014/08/JoinIN.jpg",
        Name: "JoinIn",
        Purpose: "Povezati mlade",
        City: { Name: "Povezimo mlade" },
        AffinityType: { Name: "Podijelimo znanje" },

    };

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

    $scope.aloo = function()
    {
        console.log($scope.groupsToShow);
    }

    $scope.FindWithThisCondition = function()
    {
        //console.log("U pretrazi sam");
        //console.log($scope.groupsToShow);
        $scope.groupsToShow.splice(0, $scope.groupsToShow.length);
        //console.log($scope.groupsToShow);
        console.log();
        $scope.limit = 4;
        //console.log(!!$scope.searchedText);
        if(!$scope.searchedText)
        {
            angular.forEach(allGroups, function (value) {
                //console.log(value);
                //console.log($scope.chosenCity);
                if(($scope.chosenCity.Id == 1000 || ($scope.chosenCity.Id == value.CityId)) && (($scope.chosenAffinity.Id == 1000 || value.AffinityType.Id == $scope.chosenAffinity.Id)))
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
                if (($scope.chosenCity.Id == 1000 || ($scope.chosenCity.Id == value.CityId)) && (($scope.chosenAffinity.Id == 1000 || value.AffinityType.Id == $scope.chosenAffinity.Id)) && ((value.Name).toLowerCase()).indexOf(($scope.searchedText).toLowerCase()) >= 0) {
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

    $scope.PutThisGroupInMiddle = function(group)
    {
        $scope.userToShowForFirst = group;
    }
})

.controller('homeController', function () {
})

.controller('secondController', function ($scope, $http, $location) {
    var choise = 1;
    var affinities = [];
    $scope.isschool = true.toString();
    $scope.startPicture = "http://nkdinamo-okic.hr/page/wp-content/uploads/2014/09/no-avatar_female5-300x284.jpg";
    $scope.newPicture = "";
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

    $scope.$watch('newPicture', function (newValue) {
        if(newValue)
        {
            console.log("Minjam sliku");
            $scope.startPicture = newValue;
        }
    });


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
            photoUrl: !$scope.newPicture ? $scope.startPicture : $scope.newPicture,
        }


        $http.post('api/SocialGroupsApi/MakeNewUser', user)
        .success(function () {
            console.log("Uspilo");
            $location.path("/");
        })
        .error(function () {
            alert("Doslo je do greske");
            $location.path("/");
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
        console.log(user);

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
            Console.log("KRIVI LOGIN");
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

.controller('adminController', function ($scope, $http, $cookies, $location, _) {
    $scope.searchedCity = "Sve grupe";
    $http.get('/api/AdminApi/GetAllInformationsForAdmin')
    .success(function (result) {
        $scope.informations = result;
        console.log(result);
    })

    $scope.changeCity = function(grad)
    {
        
        console.log("u change city sam");
        if(grad === "svi")
        {
            $scope.groupsToShow = allGroups;
        }
        else if(grad === "zagreb")
        {
            $scope.groupsToShow = _.filter(allGroups, function(group){return group.CityId === 2});
        }
        else
        {
            $scope.groupsToShow = _.filter(allGroups, function(group){return group.CityId === 1})
        }
    }

    var allGroups = [];

    $http.get('api/SocialGroupsApi/GetAllGroups')
    .then(function (result) {
        console.log(result.data);
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

        console.log("ide provjera pasworda");
        $http.get('api/loginApi/GetHashOfThisPassword/' + value)

        .then(function (result) {
            if (result.data === oldPassword) {
                //console.log("iste su sifre");
                $scope.isOldPasswordCorrect = true; //check if is it right
            }
            else {
                $scope.isOldPasswordCorrect = false;  //if password is not defined then for sure it is not oke
            }
        })
        
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