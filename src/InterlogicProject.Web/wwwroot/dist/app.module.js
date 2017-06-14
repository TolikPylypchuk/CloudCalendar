"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var account_1 = require("./account/account");
var common_1 = require("./common/common");
var routes_module_1 = require("./routes.module");
var calendar_1 = require("./lecturer/calendar/calendar");
var calendar_2 = require("./student/calendar/calendar");
var app_component_1 = require("./app.component");
var navigation_component_1 = require("./navigation.component");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        declarations: [
            app_component_1.default,
            navigation_component_1.default
        ],
        imports: [
            ng_bootstrap_1.NgbModule.forRoot(),
            common_1.CommonModule,
            account_1.AccountModule,
            calendar_1.CalendarModule,
            calendar_2.CalendarModule,
            routes_module_1.default
        ],
        bootstrap: [
            app_component_1.default
        ]
    })
], AppModule);
exports.default = AppModule;
//# sourceMappingURL=app.module.js.map