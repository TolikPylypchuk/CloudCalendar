"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var login_component_1 = require("./components/login.component");
var logout_component_1 = require("./components/logout.component");
var not_auth_guard_1 = require("./guards/not-auth.guard");
var routes = [
    { path: "login", component: login_component_1.default, canActivate: [not_auth_guard_1.default] },
    { path: "logout", component: logout_component_1.default }
];
var RoutesModule = (function () {
    function RoutesModule() {
    }
    RoutesModule = __decorate([
        core_1.NgModule({
            imports: [
                router_1.RouterModule.forChild(routes)
            ],
            exports: [
                router_1.RouterModule
            ]
        })
    ], RoutesModule);
    return RoutesModule;
}());
exports.default = RoutesModule;
//# sourceMappingURL=routes.module.js.map