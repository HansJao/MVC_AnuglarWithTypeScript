var controllerId = 'personController';
angular.module('PersonsApp').controller(controllerId, ['$scope', 'personFactory', personController]);
function personController($scope, personFactory) {
    $scope.people = [];
    personFactory.getPeople({ "test": "123" }).success(function (data) {
        $scope.people = data;
    }).error(function (error) {
        // log errors
    });
}
//# sourceMappingURL=personController.js.map