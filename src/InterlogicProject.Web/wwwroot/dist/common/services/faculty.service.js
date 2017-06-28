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
var FacultyService = (function () {
    function FacultyService(http) {
        this.faculties = "/api/faculties";
        this.http = http;
    }
    FacultyService.prototype.getFaculties = function () {
        return this.http.get(this.faculties, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    FacultyService.prototype.getFaculty = function (id) {
        return this.http.get(this.faculties + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    FacultyService.prototype.getFacultiesByBuilding = function (buildingId) {
        return this.http.get(this.faculties + "/buildingId/" + buildingId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    FacultyService.prototype.addFaculty = function (faculty) {
        var action = this.http.post(this.faculties, JSON.stringify(faculty), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            faculty.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    FacultyService.prototype.updateFaculty = function (faculty) {
        return this.http.put(this.faculties + "/" + faculty.id, JSON.stringify(faculty), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    FacultyService.prototype.deleteFaculty = function (id) {
        return this.http.delete(this.faculties + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    FacultyService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], FacultyService);
    return FacultyService;
}());
exports.default = FacultyService;
//# sourceMappingURL=faculty.service.js.map