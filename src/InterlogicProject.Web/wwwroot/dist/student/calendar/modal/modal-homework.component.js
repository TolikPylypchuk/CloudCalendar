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
var ng2_file_upload_1 = require("ng2-file-upload");
var moment = require("moment");
var common_1 = require("../../../common/common");
var functions_1 = require("../../../common/functions");
var ModalHomeworkComponent = (function () {
    function ModalHomeworkComponent(classService, homeworkService, notificationService, studentService) {
        this.classService = classService;
        this.homeworkService = homeworkService;
        this.notificationService = notificationService;
        this.studentService = studentService;
    }
    ModalHomeworkComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.studentService.getCurrentStudent()
            .subscribe(function (student) {
            _this.currentStudentId = student.id;
            _this.uploader = new ng2_file_upload_1.FileUploader({
                url: "/api/homeworks/classId/" + _this.classId +
                    ("/studentId/" + student.id),
                authToken: "Bearer " + functions_1.getAuthToken(),
                removeAfterUpload: true
            });
            _this.uploader.onCompleteItem = function (item) {
                _this.homeworkService.getHomeworkByClassAndStudent(_this.classId, _this.currentStudentId)
                    .subscribe(function (homework) { return _this.homework = homework; });
            };
            _this.uploader.onCompleteAll = function () {
                _this.notificationService.addNotificationForLecturersInClass({
                    dateTime: moment().toISOString(),
                    text: _this.getNotificationText(student),
                    userId: student.userId,
                    classId: _this.classId
                }, _this.currentClass.id)
                    .connect();
            };
            _this.homeworkService.getHomeworkByClassAndStudent(_this.classId, _this.currentStudentId)
                .subscribe(function (homework) { return _this.homework = homework; });
        });
        this.classService.getClass(this.classId)
            .subscribe(function (c) { return _this.currentClass = c; });
    };
    ModalHomeworkComponent.prototype.deleteHomework = function () {
        var _this = this;
        var action = this.homeworkService.deleteHomework(this.homework.id);
        action.subscribe(function (response) {
            if (response.status === 204) {
                _this.homework = null;
            }
        });
        action.connect();
    };
    ModalHomeworkComponent.prototype.getCheckClass = function () {
        return this.homework.accepted === null
            ? "fa fa-circle-o text-primary"
            : this.homework.accepted
                ? "fa fa-check text-success"
                : "fa fa-ban text-danger";
    };
    ModalHomeworkComponent.prototype.getCheckTooltip = function () {
        return this.homework.accepted === null
            ? "Не перевірено"
            : this.homework.accepted
                ? "Прийнято"
                : "Відхилено";
    };
    ModalHomeworkComponent.prototype.getNotificationText = function (student) {
        return student.firstName + " " + student.lastName + " \u0434\u043E\u0434\u0430\u0432 \u0434\u043E\u043C\u0430\u0448\u043D\u044E \u0440\u043E\u0431\u043E\u0442\u0443 " +
            ("\u0434\u043E \u043F\u0430\u0440\u0438 '" + this.currentClass.subjectName + "' ") +
            (moment(this.currentClass.dateTime).format("DD.MM.YYYY") + ".");
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], ModalHomeworkComponent.prototype, "classId", void 0);
    ModalHomeworkComponent = __decorate([
        core_1.Component({
            selector: "ip-student-modal-homework",
            templateUrl: "/templates/student/calendar/modal-homework",
            styleUrls: ["/dist/css/style.min.css"]
        }),
        __metadata("design:paramtypes", [common_1.ClassService,
            common_1.HomeworkService,
            common_1.NotificationService,
            common_1.StudentService])
    ], ModalHomeworkComponent);
    return ModalHomeworkComponent;
}());
exports.default = ModalHomeworkComponent;
//# sourceMappingURL=modal-homework.component.js.map