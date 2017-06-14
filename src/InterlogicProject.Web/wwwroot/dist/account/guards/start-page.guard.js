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
var Observable_1 = require("rxjs/Observable");
var auth_service_1 = require("../services/auth.service");
var StartPageGuard = (function () {
    function StartPageGuard(router, authService) {
        this.router = router;
        this.authService = authService;
    }
    StartPageGuard.prototype.canActivate = function (route, state) {
        var _this = this;
        if (!this.authService.isLoggedIn()) {
            this.router.navigate(["/schedule"]);
            return Observable_1.Observable.of(false);
        }
        return this.authService.getCurrentUser()
            .map(function (user) {
            if (!user) {
                _this.router.navigate(["/schedule"]);
                return false;
            }
            var navigateUrl = "/schedule";
            if (navigateUrl) {
                _this.router.navigate([navigateUrl]);
            }
            return false;
        });
    };
    return StartPageGuard;
}());
StartPageGuard = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [router_1.Router, typeof (_a = typeof auth_service_1.AuthService !== "undefined" && auth_service_1.AuthService) === "function" && _a || Object])
], StartPageGuard);
exports.StartPageGuard = StartPageGuard;
var _a;
//# sourceMappingURL=start-page.guard.js.map