"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var notifications_component_1 = require("./components/notifications.component");
var view_component_1 = require("./view.component");
var routes_module_1 = require("./routes.module");
var ViewModule = (function () {
    function ViewModule() {
    }
    return ViewModule;
}());
ViewModule = __decorate([
    core_1.NgModule({
        declarations: [
            notifications_component_1.default,
            view_component_1.default
        ],
        imports: [
            platform_browser_1.BrowserModule,
            routes_module_1.default
        ],
        exports: [
            notifications_component_1.default,
            view_component_1.default
        ]
    })
], ViewModule);
exports.default = ViewModule;
//# sourceMappingURL=view.module.js.map