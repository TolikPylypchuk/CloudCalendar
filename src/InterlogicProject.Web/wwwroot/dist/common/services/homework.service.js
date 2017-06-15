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
var HomeworkService = (function () {
    function HomeworkService(http) {
        this.homeworks = "/api/homeworks";
        this.http = http;
    }
    HomeworkService.prototype.getHomeworks = function () {
        return this.http.get(this.homeworks, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.getHomework = function (id) {
        return this.http.get(this.homeworks + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.getHomeworksByClass = function (classId) {
        return this.http.get(this.homeworks + "/classId/" + classId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.getHomeworksByStudent = function (studentId) {
        return this.http.get(this.homeworks + "/studentId/" + studentId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.getHomeworksByClassAndStudent = function (classId, studentId) {
        return this.http.get(this.homeworks + "/classId/" + classId + "/studentId/" + studentId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.updateHomework = function (homework) {
        return this.http.put(this.homeworks + "/" + homework.id, JSON.stringify(homework), {
            headers: new http_1.Headers({ "Content-Type": "application/json" })
        })
            .first()
            .publish();
    };
    HomeworkService.prototype.deleteHomework = function (id) {
        return this.http.delete(this.homeworks + "/" + id)
            .first()
            .publish();
    };
    return HomeworkService;
}());
HomeworkService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], HomeworkService);
exports.default = HomeworkService;
//# sourceMappingURL=homework.service.js.map