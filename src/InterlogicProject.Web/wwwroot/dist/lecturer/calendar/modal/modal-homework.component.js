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
var common_1 = require("../../../common/common");
var ModalHomeworkComponent = (function () {
    function ModalHomeworkComponent(classService, homeworkService, lecturerService, studentService) {
        this.homeworks = [];
        this.students = new Map();
        this.allowText = "Дозволити надсилання домашніх завдань";
        this.forbidText = "Обмежити надсилання домашніх завдань";
        this.classService = classService;
        this.homeworkService = homeworkService;
        this.lecturerService = lecturerService;
        this.studentService = studentService;
    }
    ModalHomeworkComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.classService.getClass(this.classId)
            .subscribe(function (c) {
            _this.currentClass = c;
            _this.text = _this.currentClass.homeworkEnabled
                ? _this.forbidText
                : _this.allowText;
        });
        this.homeworkService.getHomeworksByClass(this.classId)
            .subscribe(function (homeworks) {
            _this.homeworks = homeworks;
            for (var _i = 0, homeworks_1 = homeworks; _i < homeworks_1.length; _i++) {
                var homework = homeworks_1[_i];
                _this.studentService.getStudent(homework.studentId)
                    .subscribe(function (student) {
                    return _this.students.set(student.id, student);
                });
            }
        });
    };
    ModalHomeworkComponent.prototype.homeworkEnabledClick = function () {
        var _this = this;
        this.currentClass.homeworkEnabled = !this.currentClass.homeworkEnabled;
        var action = this.classService.updateClass(this.currentClass);
        action.subscribe(function (response) {
            if (response.status === 204) {
                _this.text = _this.currentClass.homeworkEnabled
                    ? _this.forbidText
                    : _this.allowText;
            }
        });
        action.connect();
    };
    ModalHomeworkComponent.prototype.acceptHomework = function (homework, accepted) {
        var h = {
            id: homework.id,
            accepted: accepted
        };
        var action = this.homeworkService.updateHomework(h);
        action.subscribe(function (response) {
            if (response.status === 204) {
                homework.accepted = accepted;
            }
        });
        action.connect();
    };
    ModalHomeworkComponent.prototype.deleteHomework = function (homework) {
        var _this = this;
        var action = this.homeworkService.deleteHomework(homework.id);
        action.subscribe(function (response) {
            if (response.status === 204) {
                _this.homeworks = _this.homeworks.filter(function (h) { return h.id !== homework.id; });
            }
        });
        action.connect();
    };
    ModalHomeworkComponent.prototype.getStudentName = function (id) {
        var student = this.students.get(id);
        return student
            ? student.lastName + " " + student.firstName
            : "";
    };
    ModalHomeworkComponent.prototype.getAcceptedText = function (accepted) {
        return accepted === null
            ? "Не перевірено"
            : accepted
                ? "Прийнято"
                : "Відхилено";
    };
    ModalHomeworkComponent.prototype.getAcceptedClass = function (accepted) {
        return accepted === null
            ? "text-primary float-right"
            : accepted
                ? "text-success float-right"
                : "text-danger float-right";
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], ModalHomeworkComponent.prototype, "classId", void 0);
    ModalHomeworkComponent = __decorate([
        core_1.Component({
            selector: "ip-lecturer-modal-homework",
            templateUrl: "/templates/lecturer/calendar/modal-homework",
            styleUrls: ["/dist/css/style.min.css"]
        }),
        __metadata("design:paramtypes", [common_1.ClassService,
            common_1.HomeworkService,
            common_1.LecturerService,
            common_1.StudentService])
    ], ModalHomeworkComponent);
    return ModalHomeworkComponent;
}());
exports.default = ModalHomeworkComponent;
//# sourceMappingURL=modal-homework.component.js.map