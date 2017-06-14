"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var LoginComponent = (function () {
    function LoginComponent() {
        this.model = {
            username: "",
            password: ""
        };
        this.loginError = false;
    }
    LoginComponent.prototype.nstructor = function (router, accountService) {
        this.router = router;
        this.accountService = accountService;
    };
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
        this.accountService.login(this.model)
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
        selector: "ip-login",
        templateUrl: "/templates/account/login"
    })
], LoginComponent);
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map