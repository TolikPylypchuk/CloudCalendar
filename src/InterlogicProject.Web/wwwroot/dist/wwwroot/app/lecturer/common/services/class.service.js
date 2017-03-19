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
var Observable_1 = require("rxjs/Observable");
var ClassService = (function () {
    function ClassService(http) {
        this.http = http;
    }
    ClassService.prototype.getClass = function (id) {
        return this.http.get("api/classes/" + id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClassService.prototype.getPlaces = function (classId) {
        return this.http.get("api/classrooms/classId/" + classId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClassService.prototype.getGroups = function (classId) {
        return this.http.get("api/groups/classId/" + classId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClassService.prototype.getLecturers = function (classId) {
        return this.http.get("api/lecturers/classId/" + classId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClassService.prototype.getComments = function (classId) {
        return this.http.get("api/comments/classId/" + classId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClassService.prototype.getMaterials = function (classId) {
        return this.http.get("api/materials/classId/" + classId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClassService.prototype.addComment = function (comment) {
        return this.http.post("api/comments", JSON.stringify(comment), {
            headers: new http_1.Headers({ "Content-Type": "application/json" })
        })
            .catch(this.handleError);
    };
    ClassService.prototype.updateComment = function (comment) {
        return this.http.put("api/comments/" + comment.id, JSON.stringify(comment), {
            headers: new http_1.Headers({ "Content-Type": "application/json" })
        })
            .catch(this.handleError);
    };
    ClassService.prototype.deleteComment = function (id) {
        return this.http.delete("api/comments/" + id)
            .catch(this.handleError);
    };
    ClassService.prototype.deleteMaterial = function (id) {
        return this.http.delete("api/materials/" + id)
            .catch(this.handleError);
    };
    ClassService.prototype.handleError = function (error) {
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
    return ClassService;
}());
ClassService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ClassService);
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = ClassService;
//# sourceMappingURL=class.service.js.map