System.register(["@angular/core", "@angular/http", "@ng-bootstrap/ng-bootstrap", "moment"], function (exports_1, context_1) {
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
    var __moduleName = context_1 && context_1.id;
    var core_1, http_1, ng_bootstrap_1, moment, CalendarComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (ng_bootstrap_1_1) {
                ng_bootstrap_1 = ng_bootstrap_1_1;
            },
            function (moment_1) {
                moment = moment_1;
            }
        ],
        execute: function () {
            CalendarComponent = (function () {
                function CalendarComponent(http, modalService) {
                    this.http = http;
                    this.modalService = modalService;
                    this.options = {
                        allDaySlot: false,
                        columnFormat: "dd, DD.MM",
                        defaultView: "agendaWeek",
                        // eventClick: eventClicked,
                        eventColor: "#0275D8",
                        eventDurationEditable: false,
                        events: this.getEvents,
                        header: {
                            left: "title",
                            center: "agendaWeek,listWeek",
                            right: "today,prev,next"
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
                CalendarComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var request = this.http.get("/api/students/id/" + this.studentId);
                    request.map(function (res) { return res.json(); })
                        .subscribe(function (data) { return _this.currentStudent = data; });
                };
                CalendarComponent.prototype.getEvents = function (start, end, timezone, callback) {
                    var _this = this;
                    var request = this.http.get("/api/classes/groupId/" + this.currentStudent.groupId +
                        ("range/" + start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")));
                    request.map(function (res) { return res.json(); })
                        .subscribe(function (data) {
                        var classes = data;
                        callback(classes.map(_this.classToEvent));
                    });
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
                return CalendarComponent;
            }());
            __decorate([
                core_1.Input(),
                __metadata("design:type", Number)
            ], CalendarComponent.prototype, "studentId", void 0);
            CalendarComponent = __decorate([
                core_1.Component({
                    selector: "student-calendar",
                    template: "\n\t\t<angular2-fullcalendar [options]=\"options\" class=\"m-1 pb-1\">\n\t\t</angular2-fullcalendar>\n\t\t\n\t\t<template ngbModalContainer></template>\n\t"
                }),
                __metadata("design:paramtypes", [http_1.Http, ng_bootstrap_1.NgbModal])
            ], CalendarComponent);
            exports_1("default", CalendarComponent);
        }
    };
});
//# sourceMappingURL=calendar.component.js.map