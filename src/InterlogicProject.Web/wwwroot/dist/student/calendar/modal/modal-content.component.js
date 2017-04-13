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
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var moment = require("moment");
var common_1 = require("../../common/common");
var ModalContentComponent = (function () {
    function ModalContentComponent(activeModal, classService) {
        this.subjectName = "Завантаження...";
        this.type = "Завантаження...";
        this.dateTime = "";
        this.activeModal = activeModal;
        this.classService = classService;
    }
    ModalContentComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.classService.getClass(this.classId)
            .subscribe(function (data) {
            _this.subjectName = data.subjectName;
            _this.type = data.type;
            _this.dateTime = data.dateTime;
        });
        this.classService.getPlaces(this.classId)
            .subscribe(function (data) { return _this.classrooms = data; });
        this.classService.getLecturers(this.classId)
            .subscribe(function (data) { return _this.lecturers = data; });
    };
    ModalContentComponent.prototype.formatDateTime = function (dateTime) {
        return dateTime === ""
            ? "Завантаження..."
            : moment.utc(dateTime).format("DD.MM.YYYY, dddd, HH:mm");
    };
    ModalContentComponent.prototype.formatClassrooms = function (classrooms) {
        return classrooms
            ? classrooms.reduce(function (a, c) { return a + ", " + c.name; }, "").substring(2)
            : "";
    };
    ModalContentComponent.prototype.formatLecturers = function (lecturers) {
        return lecturers
            ? lecturers.reduce(function (a, l) { return a + ", " + l.userLastName + " " + l.userFirstName[0] + ". " +
                (l.userMiddleName[0] + "."); }, "").substring(2)
            : "";
    };
    return ModalContentComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], ModalContentComponent.prototype, "classId", void 0);
ModalContentComponent = __decorate([
    core_1.Component({
        selector: "student-modal-content",
        templateUrl: "app/student/calendar/modal/modal-content.component.html"
    }),
    __metadata("design:paramtypes", [ng_bootstrap_1.NgbActiveModal,
        common_1.ClassService])
], ModalContentComponent);
exports.default = ModalContentComponent;
//# sourceMappingURL=modal-content.component.js.map