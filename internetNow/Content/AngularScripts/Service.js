app.service("EmployeeService", function ($http) {
    this.getEmployee = function () {
         return $http.get("/Home/getallemployee");
    };
}); 