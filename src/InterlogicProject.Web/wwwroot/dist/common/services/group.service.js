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
var GroupService = (function () {
    function GroupService(http) {
        this.groups = "/api/groups";
        this.http = http;
    }
    GroupService.prototype.getGroups = function () {
        return this.http.get(this.groups, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.getGroup = function (id) {
        return this.http.get(this.groups + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.getGroupsByDepartment = function (departmentId) {
        return this.http.get(this.groups + "/departmentId/" + departmentId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.getGroupsByFaculty = function (facultyId) {
        return this.http.get(this.groups + "/facultyId/" + facultyId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.getGroupsByYear = function (year) {
        return this.http.get(this.groups + "/year/" + year, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.getGroupsByFacultyAndYear = function (facultyId, year) {
        return this.http.get(this.groups + "/facultyId/" + facultyId + "/year/" + year, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.getGroupsByClass = function (classId) {
        return this.http.get(this.groups + "/classId/" + classId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    GroupService.prototype.addGroup = function (group) {
        var action = this.http.post(this.groups, JSON.stringify(group), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            group.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    GroupService.prototype.updateGroup = function (group) {
        return this.http.put(this.groups + "/" + group.id, JSON.stringify(group), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    GroupService.prototype.deleteGroup = function (id) {
        return this.http.delete(this.groups + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    GroupService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], GroupService);
    return GroupService;
}());
exports.default = GroupService;
//# sourceMappingURL=group.service.js.map