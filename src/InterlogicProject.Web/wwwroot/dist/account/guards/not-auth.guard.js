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
var account_service_1 = require("../services/account.service");
var NotAuthGuard = (function () {
    function NotAuthGuard(router, accountService) {
        this.router = router;
        this.accountService = accountService;
    }
    NotAuthGuard.prototype.canActivate = function (route, state) {
        if (!this.accountService.isLoggedIn()) {
            return true;
        }
        this.router.navigate([""]);
        return false;
    };
    NotAuthGuard.prototype.canActivateChild = function (route, state) {
        return this.canActivate(route, state);
    };
    return NotAuthGuard;
}());
NotAuthGuard = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [router_1.Router, account_service_1.AccountService])
], NotAuthGuard);
exports.NotAuthGuard = NotAuthGuard;
//# sourceMappingURL=not-auth.guard.js.map