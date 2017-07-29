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
var moment = require("moment");
var common_1 = require("../../../common/common");
var ModalCommentsComponent = (function () {
    function ModalCommentsComponent(classService, commentService, notificationService, studentService) {
        this.comments = [];
        this.currentComment = {
            text: ""
        };
        this.editedCommentId = 0;
        this.editedCommentOriginalText = "";
        this.classService = classService;
        this.commentService = commentService;
        this.notificationService = notificationService;
        this.studentService = studentService;
    }
    ModalCommentsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.studentService.getCurrentStudent()
            .subscribe(function (student) {
            _this.currentStudent = student;
            _this.currentComment.userId = student.userId;
            _this.currentComment.userFirstName = student.firstName;
            _this.currentComment.userMiddleName = student.middleName;
            _this.currentComment.userLastName = student.lastName;
            _this.currentComment.userFullName = student.fullName;
            _this.currentComment.classId = _this.classId;
            _this.classService.getClass(_this.classId)
                .subscribe(function (c) {
                _this.currentClass = c;
            });
        });
        this.commentService.getCommentsByClass(this.classId)
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
        var action = this.commentService.addComment(this.currentComment);
        action.subscribe(function (response) {
            if (response.status === 201) {
                _this.comments.push(response.json());
                _this.currentComment = {
                    userId: _this.currentComment.userId,
                    userFirstName: _this.currentComment.userFirstName,
                    userMiddleName: _this.currentComment.userMiddleName,
                    userLastName: _this.currentComment.userLastName,
                    userFullName: _this.currentComment.userFullName,
                    userEmail: _this.currentComment.userEmail,
                    classId: _this.currentComment.classId,
                    text: ""
                };
                var notification_1 = {
                    dateTime: moment().toISOString(),
                    text: _this.getNotificationText(),
                    userId: _this.currentStudent.userId,
                    classId: _this.classId
                };
                var action_1 = _this.notificationService.addNotificationForGroupsInClass(notification_1, _this.classId);
                action_1.subscribe(function () {
                    return _this.notificationService.addNotificationForLecturersInClass(notification_1, _this.classId)
                        .connect();
                });
                action_1.connect();
            }
            ;
        });
        action.connect();
    };
    ModalCommentsComponent.prototype.editComment = function (comment) {
        this.editedCommentId = comment.id;
        this.editedCommentOriginalText = comment.text;
    };
    ModalCommentsComponent.prototype.updateComment = function (comment) {
        var _this = this;
        var action = this.commentService.updateComment(comment);
        action.subscribe(function (response) {
            if (response.status === 204) {
                _this.editedCommentId = 0;
            }
        });
        action.connect();
    };
    ModalCommentsComponent.prototype.cancelEditing = function (comment) {
        comment.text = this.editedCommentOriginalText;
        this.editedCommentId = 0;
        this.editedCommentOriginalText = "";
    };
    ModalCommentsComponent.prototype.deleteComment = function (comment) {
        var _this = this;
        var action = this.commentService.deleteComment(comment.id);
        action.subscribe(function (response) {
            if (response.status === 204) {
                _this.comments = _this.comments.filter(function (c) { return c.id !== comment.id; });
            }
        });
        action.connect();
    };
    ModalCommentsComponent.prototype.getNotificationText = function () {
        return this.currentStudent.firstName + " " + this.currentStudent.lastName + " " +
            ("\u0434\u043E\u0434\u0430\u0432 \u043A\u043E\u043C\u0435\u043D\u0442\u0430\u0440 \u0434\u043E \u043F\u0430\u0440\u0438 '" + this.currentClass.subjectName + "' ") +
            (moment(this.currentClass.dateTime).format("DD.MM.YYYY") + ".");
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], ModalCommentsComponent.prototype, "classId", void 0);
    ModalCommentsComponent = __decorate([
        core_1.Component({
            selector: "ip-student-modal-comments",
            templateUrl: "/templates/student/calendar/modal-comments",
            styleUrls: ["/dist/css/style.min.css"]
        }),
        __metadata("design:paramtypes", [common_1.ClassService,
            common_1.CommentService,
            common_1.NotificationService,
            common_1.StudentService])
    ], ModalCommentsComponent);
    return ModalCommentsComponent;
}());
exports.default = ModalCommentsComponent;
//# sourceMappingURL=modal-comments.component.js.map