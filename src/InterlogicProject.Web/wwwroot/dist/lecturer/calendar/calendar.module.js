"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var calendar_1 = require("angular2-fullcalendar/src/calendar/calendar");
var ng2_file_upload_1 = require("ng2-file-upload");
var common_1 = require("../common/common");
var calendar_component_1 = require("./calendar.component");
var modal_content_component_1 = require("./modal/modal-content.component");
var modal_comments_component_1 = require("./modal/modal-comments.component");
var modal_homework_component_1 = require("./modal/modal-homework.component");
var modal_materials_component_1 = require("./modal/modal-materials.component");
var CalendarModule = (function () {
    function CalendarModule() {
    }
    return CalendarModule;
}());
CalendarModule = __decorate([
    core_1.NgModule({
        declarations: [
            calendar_1.CalendarComponent,
            ng2_file_upload_1.FileSelectDirective,
            ng2_file_upload_1.FileDropDirective,
            calendar_component_1.default,
            modal_content_component_1.default,
            modal_comments_component_1.default,
            modal_homework_component_1.default,
            modal_materials_component_1.default
        ],
        entryComponents: [
            modal_content_component_1.default
        ],
        imports: [
            platform_browser_1.BrowserModule,
            http_1.HttpModule,
            forms_1.FormsModule,
            ng_bootstrap_1.NgbModalModule,
            ng_bootstrap_1.NgbTooltipModule,
            common_1.CommonModule
        ],
        exports: [
            calendar_component_1.default,
            modal_content_component_1.default,
            modal_comments_component_1.default,
            modal_homework_component_1.default,
            modal_materials_component_1.default
        ]
    })
], CalendarModule);
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = CalendarModule;
//# sourceMappingURL=calendar.module.js.map