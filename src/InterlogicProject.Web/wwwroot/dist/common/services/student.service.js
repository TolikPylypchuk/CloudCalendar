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
var StudentService = (function () {
    function StudentService(http, accountService) {
        this.students = "/api/students";
        this.currentStudentSource = new ReplaySubject_1.ReplaySubject();
        this.currentUserId = 0;
        this.http = http;
        this.accountService = accountService;
    }
    StudentService.prototype.getCurrentStudent = function () {
        var _this = this;
        return this.accountService.getCurrentUser()
            .map(function (user) {
            return user.id === _this.currentUserId
                ? _this.currentStudentSource.asObservable()
                : _this.http.get(_this.students + "/userId/" + user.id, { headers: functions_1.getHeaders() })
                    .map(function (response) {
                    var student = response.json();
                    _this.currentUserId = student.userId;
                    _this.currentStudentSource.next(student);
                    return student;
                });
        })
            .switch();
    };
    StudentService.prototype.getStudents = function () {
        return this.http.get(this.students, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    StudentService.prototype.getStudent = function (id) {
        return this.http.get(this.students + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    StudentService.prototype.getStudentByUserId = function (userId) {
        return this.http.get(this.students + "/userId/" + userId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    StudentService.prototype.getStudentByEmail = function (email) {
        return this.http.get(this.students + "/email/" + email, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    StudentService.prototype.getStudentByTranscript = function (transcript) {
        return this.http.get(this.students + "/transcript/" + transcript, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    StudentService.prototype.getStudentsByGroup = function (groupId) {
        return this.http.get(this.students + "/groupId/" + groupId, { headers: functions_1.getHeaders() })
            .map(function (response) {
            return response.status === 200
                ? response.json()
                : null;
        })
            .first();
    };
    StudentService.prototype.addStudent = function (student) {
        var action = this.http.post(this.students, JSON.stringify(student), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            student.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    StudentService.prototype.updateStudent = function (student) {
        return this.http.put(this.students + "/" + student.id, JSON.stringify(student), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    StudentService.prototype.deleteStudent = function (id) {
        return this.http.delete(this.students + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    return StudentService;
}());
StudentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http, account_1.AccountService])
], StudentService);
exports.default = StudentService;
//# sourceMappingURL=student.service.js.map