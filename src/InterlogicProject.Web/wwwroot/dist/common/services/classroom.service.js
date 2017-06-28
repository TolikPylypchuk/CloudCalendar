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
var ClassroomService = (function () {
    function ClassroomService(http) {
        this.classrooms = "/api/classrooms";
        this.http = http;
    }
    ClassroomService.prototype.getClassrooms = function () {
        return this.http.get(this.classrooms, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassroomService.prototype.getClassroom = function (id) {
        return this.http.get(this.classrooms + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassroomService.prototype.getClassroomsByBuilding = function (buildingId) {
        return this.http.get(this.classrooms + "/buildingId/" + buildingId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassroomService.prototype.getClassroomsByClass = function (classId) {
        return this.http.get(this.classrooms + "/classId/" + classId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassroomService.prototype.addClassroom = function (classroom) {
        var action = this.http.post(this.classrooms, JSON.stringify(classroom), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            classroom.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    ClassroomService.prototype.updateClassroom = function (classroom) {
        return this.http.put(this.classrooms + "/" + classroom.id, JSON.stringify(classroom), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    ClassroomService.prototype.deleteClassroom = function (id) {
        return this.http.delete(this.classrooms + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    ClassroomService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], ClassroomService);
    return ClassroomService;
}());
exports.default = ClassroomService;
//# sourceMappingURL=classroom.service.js.map