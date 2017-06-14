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
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var login_component_1 = require("./components/login.component");
var logout_component_1 = require("./components/logout.component");
var account_service_1 = require("./services/account.service");
var auth_guard_1 = require("./guards/auth.guard");
var not_auth_guard_1 = require("./guards/not-auth.guard");
var routes_module_1 = require("./routes.module");
var AccountModule = (function () {
    function AccountModule() {
    }
    return AccountModule;
}());
AccountModule = __decorate([
    core_1.NgModule({
        declarations: [
            login_component_1.default,
            logout_component_1.default
        ],
        imports: [
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            http_1.HttpModule,
            routes_module_1.default
        ],
        providers: [
            account_service_1.default,
            auth_guard_1.default,
            not_auth_guard_1.default
        ]
    })
], AccountModule);
exports.default = AccountModule;
//# sourceMappingURL=account.module.js.map