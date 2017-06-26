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
var account_1 = require("../account/account");
var view_component_1 = require("./view.component");
var notifications_component_1 = require("./components/notifications.component");
var routes = [
    {
        path: "view",
        component: view_component_1.default,
        children: [
            { path: "notifications", component: notifications_component_1.default }
        ],
        canActivate: [account_1.AuthGuard],
        canActivateChild: [account_1.AuthGuard]
    }
];
var RoutesModule = (function () {
    function RoutesModule() {
    }
    return RoutesModule;
}());
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
exports.default = RoutesModule;
//# sourceMappingURL=routes.module.js.map