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
var moment = require("moment");
var common_1 = require("../../common/common");
var ModalCommentsComponent = (function () {
    function ModalCommentsComponent(http, studentService, classService) {
        this.comments = [];
        this.currentComment = {
            text: ""
        };
        this.editedCommentId = 0;
        this.editedCommentOriginalText = "";
        this.http = http;
        this.studentService = studentService;
        this.classService = classService;
    }
    ModalCommentsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.studentService.getCurrentUser()
            .subscribe(function (user) {
            if (user) {
                _this.currentComment.userId = user.id;
                _this.currentComment.userFirstName = user.firstName;
                _this.currentComment.userMiddleName = user.middleName;
                _this.currentComment.userLastName = user.lastName;
                _this.currentComment.userFullName = user.fullName;
                _this.currentComment.classId = _this.classId;
            }
        });
        this.classService.getComments(this.classId)
            .subscribe(function (data) { return _this.comments = data; });
    };
    ModalCommentsComponent.prototype.formatDateTime = function (dateTime, format) {
        return moment.utc(dateTime).format(format);
    };
    ModalCommentsComponent.prototype.getCommentId = function (index, comment) {
        return comment.id;
    };
    ModalCommentsComponent.prototype.addComment = function () {
        var _this = this;
        this.currentComment.dateTime = moment().utc()
            .add(2, "hours").toISOString();
        this.http.post("api/comments", JSON.stringify(this.currentComment), {
            headers: new http_1.Headers({ "Content-Type": "application/json" })
        })
            .subscribe(function (response) {
            if (response.status === 201) {
                _this.comments.push(response.json());
                _this.currentComment = {
                    userId: _this.currentComment.userId,
                    userFirstName: _this.currentComment.userFirstName,
                    userMiddleName: _this.currentComment.userMiddleName,
                    userLastName: _this.currentComment.userLastName,
                    userFullName: _this.currentComment.userFullName,
                    classId: _this.currentComment.classId,
                    text: ""
                };
            }
        });
    };
    ModalCommentsComponent.prototype.editComment = function (comment) {
        this.editedCommentId = comment.id;
        this.editedCommentOriginalText = comment.text;
    };
    ModalCommentsComponent.prototype.updateComment = function (comment) {
        var _this = this;
        this.http.put("api/comments/" + comment.id, JSON.stringify(comment), {
            headers: new http_1.Headers({ "Content-Type": "application/json" })
        })
            .subscribe(function (response) {
            if (response.status === 204) {
                _this.editedCommentId = 0;
            }
        });
    };
    ModalCommentsComponent.prototype.cancelEditing = function (comment) {
        comment.text = this.editedCommentOriginalText;
        this.editedCommentId = 0;
        this.editedCommentOriginalText = "";
    };
    ModalCommentsComponent.prototype.deleteComment = function (comment) {
        var _this = this;
        this.http.delete("api/comments/" + comment.id)
            .subscribe(function (response) {
            if (response.status === 204) {
                _this.comments = _this.comments.filter(function (c) { return c.id !== comment.id; });
            }
        });
    };
    return ModalCommentsComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], ModalCommentsComponent.prototype, "classId", void 0);
ModalCommentsComponent = __decorate([
    core_1.Component({
        selector: "student-modal-comments",
        templateUrl: "app/student/calendar/modal/modal-comments.component.html",
        styleUrls: ["app/student/calendar/modal/modal-comments.component.css"]
    }),
    __metadata("design:paramtypes", [http_1.Http,
        common_1.StudentService,
        common_1.ClassService])
], ModalCommentsComponent);
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = ModalCommentsComponent;
//# sourceMappingURL=modal-comments.component.js.map