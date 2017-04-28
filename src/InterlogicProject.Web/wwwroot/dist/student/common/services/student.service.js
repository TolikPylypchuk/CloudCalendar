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
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var BehaviorSubject_1 = require("rxjs/BehaviorSubject");
var Observable_1 = require("rxjs/Observable");
var StudentService = (function () {
    function StudentService(http) {
        var _this = this;
        this.currentUserSource = new BehaviorSubject_1.BehaviorSubject(null);
        this.currentStudentSource = new BehaviorSubject_1.BehaviorSubject(null);
        this.currentGroupSource = new BehaviorSubject_1.BehaviorSubject(null);
        this.http = http;
        this.http.get("/api/users/current")
            .map(function (response) { return response.json(); })
            .catch(this.handleError)
            .subscribe(function (data) { return _this.initUser(data); });
    }
    StudentService.prototype.getCurrentUser = function () {
        return this.currentUserSource.asObservable();
    };
    StudentService.prototype.getCurrentStudent = function () {
        return this.currentStudentSource.asObservable();
    };
    StudentService.prototype.getCurrentGroup = function () {
        return this.currentGroupSource.asObservable();
    };
    StudentService.prototype.getStudent = function (id) {
        return this.http.get("api/students/" + id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    StudentService.prototype.getLecturer = function (id) {
        return this.http.get("api/lecturers/" + id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    StudentService.prototype.initUser = function (user) {
        var _this = this;
        this.currentUserSource.next(user);
        this.http.get("/api/students/userId/" + user.id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError)
            .subscribe(function (data) { return _this.initStudent(data); });
    };
    StudentService.prototype.initStudent = function (student) {
        var _this = this;
        this.currentStudentSource.next(student);
        this.http.get("/api/groups/" + student.groupId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError)
            .subscribe(function (data) { return _this.initGroup(data); });
    };
    StudentService.prototype.initGroup = function (group) {
        this.currentGroupSource.next(group);
    };
    StudentService.prototype.handleError = function (error) {
        var message;
        if (error instanceof http_1.Response) {
            var body = error.json() || "";
            var err = body.error || JSON.stringify(body);
            message = error.status + " - " + (error.statusText || "") + " " + err;
        }
        else {
            message = error.message ? error.message : error.toString();
        }
        console.error(message);
        return Observable_1.Observable.throw(message);
    };
    return StudentService;
}());
StudentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], StudentService);
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = StudentService;
//# sourceMappingURL=student.service.js.map