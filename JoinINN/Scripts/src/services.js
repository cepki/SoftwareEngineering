angular.module('joinin')
.service('validationServiceForAdd', function ($location, $http, $timeout) {
    console.log("HEUUEUEUEUEUEUEUE");
    this.firstnameValidation = function (firstname) {
        var doesItContainJustLetters = /^[a-zA-ZČčĆćŽžĐđŠšŽž]+/.test(firstname);
        if (firstname == undefined || firstname.trim() == "") {
            return "Obavezno polje"
        }
        else if (firstname != undefined && firstname.length <= 25 && firstname.length >= 3 && doesItContainJustLetters) {
            return true;
        }
        else if (firstname != undefined && firstname.length > 25) {
            return "Ime mora imat manje od 26 slova";
        }
        else if (firstname != undefined && firstname.length < 3) {
            return "Ime mora imat najmanje 3 slova";
        }
        else {
            return "Ne smije bit brojeva u imenu";
        }
    }

    this.lastanmeValidation = function (lastname) {
        var doesItContainJustLetters = /^[a-zA-ZČčĆćŽžĐđŠšŽž]+/.test(lastname); //regex for digits
        if (lastname == undefined || lastname.trim() == "") {
            return "Obavezno polje"
        }
        else if (lastname != undefined && lastname.length <= 25 && lastname.length >= 3 && doesItContainJustLetters) {
            return true;
        }
        else if (lastname != undefined && lastname.length > 25) {
            return "Ime mora imat manje od 25 slova";
        }
        else if (lastname != undefined && lastname.length < 3) {
            return "Ime mora imat najmanje 3 slova";
        }
        else {
            return "Nema brojeva";
        }
    }

    this.emailValidation = function (email, isEmailUnique) {
        var regexForEmail = /^([0-9]|[a-z]|[.-_])+([a-z]|[0-9])+@[a-z]+\.[a-z]{2,4}$/;
        var regexForDubleDotOrOtherCharacterThatIsNotLetter = /[.-_][.,_-]/;
        var isItNormalEmail = regexForEmail.test(email);

        var doesItContainIllegatRepatings = regexForDubleDotOrOtherCharacterThatIsNotLetter.test(email);
        var doesItContaintIllegatCharacters = /[!#$%&'*/=?^`{|}~]/.test(email);
        var doesItContainsUppercaseLetters = /[A-Z]/.test(email);
        if (email == undefined || email.trim() == "") {
            return "Obavezno polje"
        }
        else if (email != undefined && isItNormalEmail && !doesItContainIllegatRepatings && isEmailUnique) {
            return true;
        }
        else if (doesItContaintIllegatCharacters) {
            return "Dozvoljeno je koristit samo slova [a-z], [. _ -] i brojeve"
        }
        else if (doesItContainsUppercaseLetters) {
            return "nema velikih slova"
        }
        else if (doesItContainIllegatRepatings) {
            return "Ne smiju se ponavljat 2 znaka koji nisu slovo ili broj zaredom"
        }
        else if (isItNormalEmail == false) {
            return "Email mora izgledat nesto@hotmail.com"
        }
        else if (!isEmailUnique) {
            console.log(isEmailUnique);
            return "Neko vec koristi ovja mial"
        }
        else {
            return "AJMOOOOOOOO";
        }

    }
    
    this.usernameValidation = function (username, isUsernameUnique) {
        var regexForUsername = /^[a-zA-Z][a-zA-Z0-9]{2,10}$/;
        var isUsernameOk = regexForUsername.test(username);
        var isFirstCharLetter = /^[a-zA-Z]/.test(username);

        if (username == undefined || username.trim() == "") {
            return "Obavezno polje"
        }
        else if (username != undefined && isUsernameOk && isUsernameUnique) {
            return true;
        }
        else if (username != undefined && username.length < 3) {
            return "Treba bit najmanje 3 znaka";
        }
        else if (username != undefined && username.length >= 11) {
            return "Kraći username";
        }
        else if (!isFirstCharLetter) {
            return "Prvi znako mora bit [a-z] ili [A-Z]";
        }
        else if (!isUsernameOk) {
            return "Samo slova i brojevi"
        }
        else if (!isUsernameUnique) {
            return "Zauzet username"
        }
        else {
            return "AJMOOO";
        }

    }

    this.passwordValidation = function (password) {
        var regexForPassword = /^([a-zA-Z0-9]|[+-.])+$/
        var isPasswordOke = regexForPassword.test(password);

        if (password == undefined || password.trim() == "") {
            return "Obavezno polje"
        }
        else if (password != undefined && password.length >= 6 && password.length <= 15 && isPasswordOke) {
            return true;
        }
        else if (password != undefined && password.length <= 5) {
            return "Treba bit duza sifra";
        }
        else if (password != undefined && password.length >= 16) {
            return "Treba bit kraca sifra";
        }
        else {
            return "Dozvoljeni znakovi su [a-z], [A-Z], [0-9] i (+) (-) (.)"
        }
    }    

    this.phoneNumberValidation = function (phoneNumber) {
        var regexForPhonenumber = /^[0-9]{9,10}$/;
        var isThereAnytingThatIsNotDigit = /[^0123456789]/.test(phoneNumber);
        var isPhoneNumbernumberOk = regexForPhonenumber.test(phoneNumber);

        if (phoneNumber == undefined || phoneNumber.trim() == "") {
            return "Obavezno polje"
        }
        else if (phoneNumber != undefined && isPhoneNumbernumberOk) {
            return true;
        }
        else if (isThereAnytingThatIsNotDigit) {
            return "Samo brojevi!!!";
        }
        else if (phoneNumber != undefined && phoneNumber.length >= 11) {
            return "skrati malo";
        }
        else if (phoneNumber != undefined && phoneNumber.length <= 8) {
            return "malo duze";
        }
        else {
            return "Samo brojevi";
        }
    }

    this.addressValidation = function (address) {
        if (address == undefined || address.trim() == "") {
            return "Obavezno polje"
        }
        return true;
    }

    this.descriptionValidation = function (description) {
        if (description == undefined || description.trim() == "") {
            return "Obavezno polje"
        }
        else if (description != undefined && description.length >= 30) {
            return true;
        }
        else {
            return "Nemoj stedit na opisu";
        }
    }

    this.repeatenPassswordValidation = function (firstPassword, secondPassword) {
        var arePasswordsSame = (firstPassword == secondPassword);
        var regexForPassword = /^([a-zA-Z0-9]|[+-.])+$/
        var isPasswordOke = regexForPassword.test(firstPassword);

        if (firstPassword == undefined || firstPassword.trim() == "") {
            return "Obavezno polje"
        }
        else if (firstPassword != undefined && arePasswordsSame && regexForPassword) {
            return true;
        }
        else if (!arePasswordsSame) {
            return "Isto password";
        }
        else {
            return "Dozvoljeni znakovi su [a-z], [A-Z], [0-9] i (+) (-) (.)"
        }
    }

    this.passswordValidationTrueOrFalse = function (password) {
        var regexForPassword = /^([a-zA-Z0-9]|[+-.])+$/
        var isPasswordOke = regexForPassword.test(password);

        if (password == undefined || password.trim() == "") {
            return false;
        }
        else if (password != undefined && password.length >= 6 && password.length <= 15 && regexForPassword) {
            return true;
        }
        else if (password != undefined && password.length <= 5) {
            return false;
        }
        else if (password != undefined && password.length >= 16) {
            return false;
        }
        else {
            return false;
        }
    }

})