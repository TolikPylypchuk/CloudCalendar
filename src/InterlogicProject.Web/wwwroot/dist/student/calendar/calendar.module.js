System.register(["@angular/core", "@angular/http", "@angular/platform-browser", "@ng-bootstrap/ng-bootstrap", "angular2-fullcalendar/src/calendar/calendar", "../common/common", "./calendar.component", "./modal/modal-content.component", "./modal/modal-comments.component"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, http_1, platform_browser_1, ng_bootstrap_1, calendar_1, common_1, calendar_component_1, modal_content_component_1, modal_comments_component_1, CalendarModule;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (ng_bootstrap_1_1) {
                ng_bootstrap_1 = ng_bootstrap_1_1;
            },
            function (calendar_1_1) {
                calendar_1 = calendar_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            },
            function (calendar_component_1_1) {
                calendar_component_1 = calendar_component_1_1;
            },
            function (modal_content_component_1_1) {
                modal_content_component_1 = modal_content_component_1_1;
            },
            function (modal_comments_component_1_1) {
                modal_comments_component_1 = modal_comments_component_1_1;
            }
        ],
        execute: function () {
            CalendarModule = (function () {
                function CalendarModule() {
                }
                return CalendarModule;
            }());
            CalendarModule = __decorate([
                core_1.NgModule({
                    declarations: [
                        calendar_1.CalendarComponent,
                        calendar_component_1.default,
                        modal_content_component_1.default,
                        modal_comments_component_1.default
                    ],
                    entryComponents: [
                        modal_content_component_1.default
                    ],
                    imports: [
                        platform_browser_1.BrowserModule,
                        http_1.HttpModule,
                        ng_bootstrap_1.NgbModalModule.forRoot(),
                        common_1.CommonModule
                    ],
                    exports: [
                        calendar_component_1.default,
                        modal_content_component_1.default,
                        modal_comments_component_1.default
                    ]
                })
            ], CalendarModule);
            exports_1("default", CalendarModule);
        }
    };
});
//# sourceMappingURL=calendar.module.js.map