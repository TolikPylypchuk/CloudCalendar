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
var CommentService = (function () {
    function CommentService(http) {
        this.comments = "/api/comments";
        this.http = http;
    }
    CommentService.prototype.getComments = function () {
        return this.http.get(this.comments, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.getComment = function (id) {
        return this.http.get(this.comments + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.getCommentsByClass = function (classId) {
        return this.http.get(this.comments + "/classId/" + classId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.getSomeCommentsByClass = function (classId, take) {
        return this.http.get(this.comments + "/classId/" + classId + "/take/" + take, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.getCommentsByUser = function (userId) {
        return this.http.get(this.comments + "/userId/" + userId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.getCommentsByClassAndUser = function (classId, userId) {
        return this.http.get(this.comments + "/classId/" + classId + "/userId/" + userId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.getSomeCommentsByClassAndUser = function (classId, userId, take) {
        return this.http.get(this.comments + "/classId/" + classId + "/userId/" + userId + "/take/" + take, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    CommentService.prototype.addComment = function (comment) {
        var action = this.http.post(this.comments, JSON.stringify(comment), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            comment.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    CommentService.prototype.updateComment = function (comment) {
        return this.http.put(this.comments + "/" + comment.id, JSON.stringify(comment), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    CommentService.prototype.deleteComment = function (id) {
        return this.http.delete(this.comments + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    return CommentService;
}());
CommentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], CommentService);
exports.default = CommentService;
//# sourceMappingURL=comment.service.js.map