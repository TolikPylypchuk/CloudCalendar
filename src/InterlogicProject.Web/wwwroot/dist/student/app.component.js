System.register(["@angular/core"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, AppComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            AppComponent = (function () {
                function AppComponent() {
                }
                AppComponent.prototype.getStudentId = function () {
                    return $("student-app").data("student-id");
                };
                AppComponent.prototype.getGroupId = function () {
                    return $("student-app").data("group-id");
                };
                return AppComponent;
            }());
            AppComponent = __decorate([
                core_1.Component({
                    selector: "student-app",
                    template: "\n\t\t<student-calendar [studentId]=\"getStudentId()\"\n\t\t                  [groupId]=\"getGroupId()\">\n\t\t<student-calendar>\n\t"
                })
            ], AppComponent);
            exports_1("default", AppComponent);
        }
    };
});
//# sourceMappingURL=app.component.js.map