"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var $ = require("jquery");
var account_service_1 = require("../services/account.service");
var LoginComponent = (function () {
    function LoginComponent(router, accountService) {
        this.model = {
            username: "",
            password: ""
        };
        this.loginError = false;
        this.router = router;
        this.accountService = accountService;
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.accountService.setLoggingIn(true);
    };
    LoginComponent.prototype.ngOnDestroy = function () {
        this.accountService.setLoggingIn(false);
    };
    LoginComponent.prototype.returnUrl = function () {
        var result = this.accountService.getReturnUrl();
        return result ? result : "";
    };
    LoginComponent.prototype.change = function () {
        this.loginError = false;
    };
    LoginComponent.prototype.submit = function () {
        var _this = this;
        var modelToSubmit = {
            username: this.model.username + $("#emailDomain").text().trim(),
            password: this.model.password
        };
        this.accountService.login(modelToSubmit)
            .subscribe(function (success) {
            if (!success) {
                _this.loginError = true;
                _this.model.password = "";
            }
        }, function () {
            _this.loginError = true;
            _this.model.password = "";
        });
    };
    LoginComponent.prototype.cancel = function () {
        this.router.navigate([this.returnUrl()]);
    };
    LoginComponent = __decorate([
        core_1.Component({
            selector: "ip-login",
            templateUrl: "/templates/account/login"
        }),
        __metadata("design:paramtypes", [router_1.Router, account_service_1.default])
    ], LoginComponent);
    return LoginComponent;
}());
exports.default = LoginComponent;
//# sourceMappingURL=login.component.js.map