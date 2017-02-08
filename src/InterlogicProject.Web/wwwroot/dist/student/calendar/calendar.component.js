System.register(["@angular/core", "@angular/http", "@ng-bootstrap/ng-bootstrap", "moment", "./modal-content.component", "../common/common"], function (exports_1, context_1) {
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
    var core_1, http_1, ng_bootstrap_1, moment, modal_content_component_1, common_1, CalendarComponent;
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
            },
            function (modal_content_component_1_1) {
                modal_content_component_1 = modal_content_component_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            }
        ],
        execute: function () {
            CalendarComponent = (function () {
                function CalendarComponent(http, modalService, studentService) {
                    this.currentSubscription = null;
                    this.http = http;
                    this.modalService = modalService;
                    this.studentService = studentService;
                    this.options = {
                        allDaySlot: false,
                        columnFormat: "dd, DD.MM",
                        defaultView: "agendaWeek",
                        eventClick: this.eventClicked.bind(this),
                        eventColor: "#0275D8",
                        eventDurationEditable: false,
                        events: this.getEvents.bind(this),
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
                CalendarComponent.prototype.getEvents = function (start, end, timezone, callback) {
                    var _this = this;
                    if (this.currentSubscription !== null) {
                        this.currentSubscription.unsubscribe();
                    }
                    this.currentSubscription = this.studentService.getCurrentGroup()
                        .subscribe(function (group) {
                        if (group) {
                            var request = _this.http.get("/api/classes/groupId/" + group.id +
                                ("/range/" + start.format("YYYY-MM-DD")) +
                                ("/" + end.format("YYYY-MM-DD")));
                            request.map(function (response) { return response.json(); })
                                .subscribe(function (data) {
                                var classes = data;
                                callback(classes.map(_this.classToEvent));
                            });
                        }
                    });
                };
                CalendarComponent.prototype.eventClicked = function (event) {
                    this.modalService.open(modal_content_component_1.default);
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
            CalendarComponent = __decorate([
                core_1.Component({
                    selector: "student-calendar",
                    template: "\n\t\t<div class=\"m-3 pb-3\">\n\t\t\t<angular2-fullcalendar [options]=\"options\">\n\t\t\t</angular2-fullcalendar>\n\t\t\n\t\t\t<template ngbModalContainer></template>\n\t\t</div>\n\t"
                }),
                __metadata("design:paramtypes", [http_1.Http,
                    ng_bootstrap_1.NgbModal,
                    common_1.StudentService])
            ], CalendarComponent);
            exports_1("default", CalendarComponent);
        }
    };
});
//# sourceMappingURL=calendar.component.js.map