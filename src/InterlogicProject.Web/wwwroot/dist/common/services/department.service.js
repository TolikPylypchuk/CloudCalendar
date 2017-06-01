"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var functions_1 = require("../functions");
var DepartmentService = (function () {
    function DepartmentService(http) {
        this.departments = "api/departments";
        this.http = http;
    }
    DepartmentService.prototype.getDepartments = function () {
        return this.http.get(this.departments)
            .map(function (response) { return response.json(); })
            .first();
    };
    DepartmentService.prototype.getDepartment = function (id) {
        return this.http.get(this.departments + "/" + id)
            .map(function (response) { return response.json(); })
            .first();
    };
    DepartmentService.prototype.getByFaculty = function (facultyId) {
        return this.http.get(this.departments + "/facultyId/" + facultyId)
            .map(function (response) { return response.json(); })
            .first();
    };
    DepartmentService.prototype.addDepartment = function (department) {
        var action = this.http.post(this.departments, JSON.stringify(department), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            department.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    DepartmentService.prototype.updateDepartment = function (department) {
        return this.http.put(this.departments + "/" + department.id, JSON.stringify(department), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    DepartmentService.prototype.deleteDepartment = function (id) {
        return this.http.delete(this.departments + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    return DepartmentService;
}());
DepartmentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], DepartmentService);
exports.default = DepartmentService;
//# sourceMappingURL=department.service.js.map