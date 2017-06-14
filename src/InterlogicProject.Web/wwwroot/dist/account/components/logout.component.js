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
var LogoutComponent = (function () {
    function LogoutComponent(router, accountService) {
        this.router = router;
        this.accountService = accountService;
    }
    LogoutComponent.prototype.ngOnInit = function () {
        this.accountService.logout();
        this.router.navigate([""]);
    };
    return LogoutComponent;
}());
LogoutComponent = __decorate([
    core_1.Component({
        selector: "ip-logout",
        template: ""
    }),
    __metadata("design:paramtypes", [router_1.Router, account_service_1.default])
], LogoutComponent);
exports.default = LogoutComponent;
//# sourceMappingURL=logout.component.js.map