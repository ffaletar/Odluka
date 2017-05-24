

var AngularApp = angular.module('AngularApp', [])

AngularApp.controller('UsporedbeController', function ($scope, AngularService) {
    
    //AngularService.upisiUsporedbu();
    //$scope.hehe = AngularService.dohvatiKriterije();
    
    $scope.customStyle = {};
    $scope.upisiUsporedbu = function (kriterij1, kriterij2, vrijednost) {
        $scope.konzistentno = AngularService.upisiUsporedbu(kriterij1, kriterij2, vrijednost)
        .success(function(data){
            $scope.students = data;

            $scope.$watch('students', function () {
                if ($scope.students == 'True') {
                    $scope.backColor = "color : 'green'";
                } else {
                    $scope.backColor = "color : 'red'";
                }
            });
        })
        .error(function(data){
            $scope.backColor = "'background-color' : 'green'";
        });
    }

    $scope.noviProjekt = function (naziv, opis, korisnik) {
        AngularService.noviProjekt(naziv, opis, korisnik)
        .success(function (data) {
            $scope.projekt = data;
        })
        .error(function (data) {
            $scope.projekt = "'background-color' : 'green'";
        });
    }
});



AngularApp.service('AngularService', function ($http) {

    this.dohvatiKriterije = function () {
        return $http.get('/Kriteriji/DohvatiSveKriterije');
    };

    this.upisiUsporedbu = function (kriterij1, kriterij2, vrijednost) {
        return $http.get("/Kriteriji/UpisiUsporedbu?kriterij1=" + kriterij1 + "&kriterij2=" + kriterij2 + "&vrijednost=" + vrijednost);
    };

    this.noviProjekt = function (naziv, opis, korisnik) {
        return $http.get("/Projekt/NoviProjekt?naziv=" + naziv + "&opis=" + opis + "&korisnik=" + korisnik);
    };

    this.dohvatiKonzistentnost = function (roditelj) {
        return $http.get("/Kriteriji/ProvjeriKonzistentnost?roditelj="+roditelj);
    };

});

