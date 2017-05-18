

var AngularApp = angular.module('AngularApp', [])

AngularApp.controller('UsporedbeController', function ($scope, AngularService) {
    
    //AngularService.upisiUsporedbu();
    //$scope.hehe = AngularService.dohvatiKriterije();
    
    $scope.customStyle = {};
    $scope.upisiUsporedbu = function (kriterij1, kriterij2, vrijednost) {
        $scope.konzistentno = AngularService.upisiUsporedbu(kriterij1, kriterij2, vrijednost);
        if ($scope.konzistentno == true) {
            $scope.aaa = "Uspjeh";
        } else {
            $scope.aaa = "Neuspjeh";
        }
    }
});



AngularApp.service('AngularService', function ($http) {

    this.dohvatiKriterije = function () {
        return $http.get('/Kriteriji/DohvatiSveKriterije');
    };

    this.upisiUsporedbu = function (kriterij1, kriterij2, vrijednost) {
        return $http.get("/Kriteriji/UpisiUsporedbu?kriterij1=" + kriterij1 + "&kriterij2=" + kriterij2 + "&vrijednost=" + vrijednost);
    };

    this.dohvatiKonzistentnost = function (roditelj) {
        return $http.get("/Kriteriji/ProvjeriKonzistentnost?roditelj="+roditelj);
    };

});

