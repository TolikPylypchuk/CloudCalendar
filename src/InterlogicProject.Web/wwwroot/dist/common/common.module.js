"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var ng2_file_upload_1 = require("ng2-file-upload");
var fullcalendar_component_1 = require("./components/fullcalendar.component");
var building_service_1 = require("./services/building.service");
var class_service_1 = require("./services/class.service");
var classroom_service_1 = require("./services/classroom.service");
var comment_service_1 = require("./services/comment.service");
var department_service_1 = require("./services/department.service");
var faculty_service_1 = require("./services/faculty.service");
var lecturer_service_1 = require("./services/lecturer.service");
var student_service_1 = require("./services/student.service");
var CommonModule = (function () {
    function CommonModule() {
    }
    return CommonModule;
}());
CommonModule = __decorate([
    core_1.NgModule({
        declarations: [
            ng2_file_upload_1.FileSelectDirective,
            ng2_file_upload_1.FileDropDirective,
            fullcalendar_component_1.default
        ],
        providers: [
            building_service_1.default,
            lecturer_service_1.default,
            class_service_1.default,
            classroom_service_1.default,
            comment_service_1.default,
            department_service_1.default,
            faculty_service_1.default,
            student_service_1.default
        ],
        imports: [
            http_1.HttpModule
        ]
    })
], CommonModule);
exports.default = CommonModule;
//# sourceMappingURL=common.module.js.map