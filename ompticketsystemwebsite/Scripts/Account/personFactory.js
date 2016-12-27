//import { Component, OnInit } from '@angular/core';
//import { Http, Response,Headers } from '@angular/http';
var serviceId = 'personFactory';
angular.module('PersonsApp').factory(serviceId, ['$http', personFactory]);
function personFactory($http) {
    function getPeople(name) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var body = JSON.stringify(name);
        return $http.post('/Account/GetPeople', body, headers);
    }
    var service = {
        getPeople: getPeople
    };
    return service;
}
//# sourceMappingURL=personFactory.js.map