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
var modal_content_component_1 = require("./modal/modal-content.component");
var CalendarComponent = (function () {
    function CalendarComponent(modalService, classService, groupService, studentService) {
        this.modalService = modalService;
        this.classService = classService;
        this.groupService = groupService;
        this.studentService = studentService;
        this.options = {
            allDaySlot: false,
            columnFormat: "dd, DD.MM",
            defaultView: "agendaWeek",
            eventClick: this.eventClicked.bind(this),
            eventBackgroundColor: "#0275D8",
            eventBorderColor: "#0275D8",
            eventDurationEditable: false,
            eventRender: function (event, element) {
                element.css("cursor", "pointer");
            },
            events: this.getEvents.bind(this),
            header: {
                left: "title",
                center: "agendaWeek,listWeek",
                right: "today prev,next"
            },
            height: "auto",
            minTime: moment.duration("08:00:00"),
            maxTime: moment.duration("21:00:00"),
            slotDuration: moment.duration("00:30:00"),
            slotLabelFormat: "HH:mm",
            slotLabelInterval: moment.duration("01:00:00"),
            titleFormat: "DD MMM YYYY",
            weekends: false,
            weekNumbers: true,
            weekNumberTitle: "Тиж "
        };
    }
    CalendarComponent.prototype.getEvents = function (start, end, timezone, callback) {
        var _this = this;
        this.studentService.getCurrentStudent()
            .subscribe(function (student) {
            return _this.groupService.getGroup(student.groupId)
                .subscribe(function (group) {
                if (group) {
                    _this.classService.getClassesForGroupInRange(group.id, start, end)
                        .subscribe(function (data) {
                        var classes = data;
                        callback(classes.map(_this.classToEvent));
                    });
                }
            });
        });
    };
    CalendarComponent.prototype.eventClicked = function (event) {
        var modalRef = this.modalService.open(modal_content_component_1.default);
        var modal = modalRef.componentInstance;
        modal.classId = event.id;
    };
    CalendarComponent.prototype.classToEvent = function (classInfo) {
        return {
            id: classInfo.id,
            title: classInfo.subjectName + ": " + classInfo.type,
            start: moment.utc(classInfo.dateTime).format(),
            end: moment.utc(classInfo.dateTime)
                .add(1, "hours")
                .add(20, "minutes")
                .format()
        };
    };
    CalendarComponent = __decorate([
        core_1.Component({
            selector: "ip-student-calendar",
            templateUrl: "templates/student/calendar",
            styleUrls: ["/dist/css/style.min.css"]
        }),
        __metadata("design:paramtypes", [ng_bootstrap_1.NgbModal,
            common_1.ClassService,
            common_1.GroupService,
            common_1.StudentService])
    ], CalendarComponent);
    return CalendarComponent;
}());
exports.default = CalendarComponent;
//# sourceMappingURL=calendar.component.js.map