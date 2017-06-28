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
var ClassService = (function () {
    function ClassService(http) {
        this.classes = "/api/classes";
        this.http = http;
    }
    ClassService.prototype.getClasses = function () {
        return this.http.get(this.classes, { headers: functions_1.getHeaders() })
            .map(function (response) {
            return response.status === 200
                ? response.json()
                : null;
        })
            .first();
    };
    ClassService.prototype.getClass = function (id) {
        return this.http.get(this.classes + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) {
            return response.status === 200
                ? response.json()
                : null;
        })
            .first();
    };
    ClassService.prototype.getClassesInRange = function (start, end) {
        return this.http.get(this.classes + "/range/" + start.format("YYYY-MM-DD") + "/" +
            ("" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForGroup = function (groupId) {
        return this.http.get(this.classes + "/groupId/" + groupId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForGroupInRange = function (groupId, start, end) {
        return this.http.get(this.classes + "/groupId/" + groupId + "/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForLecturer = function (lecturerId) {
        return this.http.get(this.classes + "/lecturerId/" + lecturerId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForLecturerInRange = function (lecturerId, start, end) {
        return this.http.get(this.classes + "/lecturerId/" + lecturerId + "/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForGroupAndLecturer = function (groupId, lecturerId) {
        return this.http.get(this.classes + "/groupId/" + groupId + "/lecturerId/" + lecturerId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForGroupAndLecturerInRange = function (groupId, lecturerId, start, end) {
        return this.http.get(this.classes + "/groupId/" + groupId + "/lecturerId/" + lecturerId + "/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForClassroom = function (classroomId) {
        return this.http.get(this.classes + "/classroomId/" + classroomId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.getClassesForClassroomInRange = function (classroomId, start, end) {
        return this.http.get(this.classes + "/classroomId/" + classroomId + "/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    ClassService.prototype.addClass = function (c) {
        var action = this.http.post(this.classes, JSON.stringify(c), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            c.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    ClassService.prototype.updateClass = function (c) {
        return this.http.put(this.classes + "/" + c.id, JSON.stringify(c), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    ClassService.prototype.deleteClass = function (id) {
        return this.http.delete(this.classes + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    ClassService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], ClassService);
    return ClassService;
}());
exports.default = ClassService;
//# sourceMappingURL=class.service.js.map