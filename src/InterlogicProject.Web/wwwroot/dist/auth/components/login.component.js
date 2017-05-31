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
var auth_service_1 = require("../services/auth.service");
var LoginComponent = (function () {
    function LoginComponent(router, authService) {
        this.model = {
            username: "",
            password: ""
        };
        this.loginError = false;
        this.router = router;
        this.authService = authService;
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.authService.setLoggingIn(true);
    };
    LoginComponent.prototype.ngOnDestroy = function () {
        this.authService.setLoggingIn(false);
    };
    LoginComponent.prototype.returnUrl = function () {
        var result = this.authService.getReturnUrl();
        return result ? result : "";
    };
    LoginComponent.prototype.change = function () {
        this.loginError = false;
    };
    LoginComponent.prototype.submit = function () {
        var _this = this;
        this.authService.login(this.model)
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
    return LoginComponent;
}());
LoginComponent = __decorate([
    core_1.Component({
        selector: "schedule-login",
        templateUrl: "./login.component.html"
    }),
    __metadata("design:paramtypes", [router_1.Router, auth_service_1.AuthService])
], LoginComponent);
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map