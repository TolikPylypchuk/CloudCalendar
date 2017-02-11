System.register(["@angular/core", "@angular/http", "./services/student.service", "./services/class.service"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, http_1, student_service_1, class_service_1, CommonModule;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (student_service_1_1) {
                student_service_1 = student_service_1_1;
            },
            function (class_service_1_1) {
                class_service_1 = class_service_1_1;
            }
        ],
        execute: function () {
            CommonModule = (function () {
                function CommonModule() {
                }
                return CommonModule;
            }());
            CommonModule = __decorate([
                core_1.NgModule({
                    providers: [
                        student_service_1.default,
                        class_service_1.default
                    ],
                    imports: [
                        http_1.HttpModule
                    ]
                })
            ], CommonModule);
            exports_1("default", CommonModule);
        }
    };
});
//# sourceMappingURL=common.module.js.map