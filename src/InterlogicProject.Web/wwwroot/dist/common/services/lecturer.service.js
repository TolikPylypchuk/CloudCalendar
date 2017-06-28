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
var ReplaySubject_1 = require("rxjs/ReplaySubject");
var functions_1 = require("../functions");
var account_1 = require("../../account/account");
var LecturerService = (function () {
    function LecturerService(http, accountService) {
        this.lecturers = "/api/lecturers";
        this.currentLecturerSource = new ReplaySubject_1.ReplaySubject(null);
        this.currentUserId = null;
        this.http = http;
        this.accountService = accountService;
    }
    LecturerService.prototype.getCurrentLecturer = function () {
        var _this = this;
        return this.accountService.getCurrentUser()
            .map(function (user) {
            return user.id === _this.currentUserId
                ? _this.currentLecturerSource.asObservable()
                : _this.http.get(_this.lecturers + "/userId/" + user.id, { headers: functions_1.getHeaders() })
                    .map(function (response) {
                    var lecturer = response.json();
                    _this.currentUserId = lecturer.userId;
                    return lecturer;
                });
        })
            .switch();
    };
    LecturerService.prototype.getLecturers = function () {
        return this.http.get(this.lecturers, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    LecturerService.prototype.getLecturer = function (id) {
        return this.http.get(this.lecturers + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    LecturerService.prototype.getLecturerByUserId = function (userId) {
        return this.http.get(this.lecturers + "/userId/" + userId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    LecturerService.prototype.getLecturerByEmail = function (email) {
        return this.http.get(this.lecturers + "/email/" + email, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    LecturerService.prototype.getLecturersByDepartment = function (departmentId) {
        return this.http.get(this.lecturers + "/departmentId/" + departmentId, { headers: functions_1.getHeaders() })
            .map(function (response) {
            return response.status === 200
                ? response.json()
                : null;
        })
            .first();
    };
    LecturerService.prototype.getLecturersByFaculty = function (facultyId) {
        return this.http.get(this.lecturers + "/facultyId/" + facultyId, { headers: functions_1.getHeaders() })
            .map(function (response) {
            return response.status === 200
                ? response.json()
                : null;
        })
            .first();
    };
    LecturerService.prototype.getLecturersByClass = function (classId) {
        return this.http.get(this.lecturers + "/classId/" + classId, { headers: functions_1.getHeaders() })
            .map(function (response) {
            return response.status === 200
                ? response.json()
                : null;
        })
            .first();
    };
    LecturerService.prototype.addLecturer = function (lecturer) {
        var action = this.http.post(this.lecturers, JSON.stringify(lecturer), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            lecturer.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    LecturerService.prototype.updateLecturer = function (lecturer) {
        return this.http.put(this.lecturers + "/" + lecturer.id, JSON.stringify(lecturer), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    LecturerService.prototype.deleteLecturer = function (id) {
        return this.http.delete(this.lecturers + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    LecturerService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http, account_1.AccountService])
    ], LecturerService);
    return LecturerService;
}());
exports.default = LecturerService;
//# sourceMappingURL=lecturer.service.js.map