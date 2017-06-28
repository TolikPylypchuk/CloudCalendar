"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var calendar_1 = require("./calendar/calendar");
var routes_module_1 = require("./routes.module");
var student_component_1 = require("./student.component");
var StudentModule = (function () {
    function StudentModule() {
    }
    StudentModule = __decorate([
        core_1.NgModule({
            declarations: [
                student_component_1.default
            ],
            imports: [
                calendar_1.CalendarModule,
                routes_module_1.default
            ],
            exports: [
                calendar_1.CalendarModule,
                student_component_1.default
            ]
        })
    ], StudentModule);
    return StudentModule;
}());
exports.default = StudentModule;
//# sourceMappingURL=student.module.js.map