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
var router_1 = require("@angular/router");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var moment = require("moment");
var modal_content_component_1 = require("./modal/modal-content.component");
var common_1 = require("../../common/common");
var CalendarComponent = (function () {
    function CalendarComponent(router, route, modalService, classService, lecturerService) {
        this.router = router;
        this.route = route;
        this.modalService = modalService;
        this.classService = classService;
        this.lecturerService = lecturerService;
    }
    CalendarComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            var paramDate = params["date"];
            var date;
            if (paramDate) {
                date = moment(params["date"], "YYYY-MM-DD", true);
                if (!date.isValid()) {
                    date = moment();
                    _this.router.navigate(["lecturer/calendar"]);
                }
            }
            else {
                date = moment();
            }
            _this.options = {
                allDaySlot: false,
                columnFormat: "dd, DD.MM",
                defaultDate: date,
                defaultView: "agendaWeek",
                eventClick: _this.eventClicked.bind(_this),
                eventBackgroundColor: "#0275D8",
                eventBorderColor: "#0275D8",
                eventDurationEditable: false,
                eventRender: function (event, element) {
                    element.css("cursor", "pointer");
                },
                events: _this.getEvents.bind(_this),
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
        });
    };
    CalendarComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            var date = moment(params["date"], "YYYY-MM-DD", true);
            var paramTime = params["time"];
            if (paramTime) {
                var time = moment(paramTime, ["HH:mm", "H:mm", "HH:m", "H:m"], true);
                var dateTime_1 = moment(date.format("YYYY-MM-DD") + "T" +
                    time.format("HH:mm"));
                if (time.isValid()) {
                    _this.lecturerService.getCurrentLecturer()
                        .subscribe(function (lecturer) {
                        return _this.classService.getClassForLecturerByDateTime(lecturer.id, dateTime_1)
                            .subscribe(function (c) {
                            if (c) {
                                _this.createModal(c.id);
                            }
                        });
                    });
                }
            }
        });
    };
    CalendarComponent.prototype.getEvents = function (start, end, timezone, callback) {
        var _this = this;
        this.lecturerService.getCurrentLecturer()
            .subscribe(function (lecturer) {
            if (lecturer) {
                _this.classService.getClassesForLecturerInRange(lecturer.id, start, end)
                    .subscribe(function (classes) {
                    return callback(classes.map(_this.classToEvent));
                });
            }
        });
    };
    CalendarComponent.prototype.eventClicked = function (event) {
        this.createModal(event.id);
    };
    CalendarComponent.prototype.createModal = function (classId) {
        var modalRef = this.modalService.open(modal_content_component_1.default);
        var modal = modalRef.componentInstance;
        modal.classId = classId;
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
            selector: "ip-lecturer-calendar",
            templateUrl: "/templates/lecturer/calendar",
            styleUrls: ["/dist/css/style.min.css"]
        }),
        __metadata("design:paramtypes", [router_1.Router,
            router_1.ActivatedRoute,
            ng_bootstrap_1.NgbModal,
            common_1.ClassService,
            common_1.LecturerService])
    ], CalendarComponent);
    return CalendarComponent;
}());
exports.default = CalendarComponent;
//# sourceMappingURL=calendar.component.js.map