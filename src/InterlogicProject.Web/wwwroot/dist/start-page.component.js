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
var account_1 = require("./account/account");
var StartPageComponent = (function () {
    function StartPageComponent(router, accountService) {
        this.router = router;
        this.accountService = accountService;
    }
    StartPageComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.accountService.isStudent()
            .subscribe(function (isStudent) {
            return _this.router.navigate([(isStudent ? "student" : "lecturer") + "/calendar"]);
        });
    };
    return StartPageComponent;
}());
StartPageComponent = __decorate([
    core_1.Component({
        selector: "ip-start-page",
        template: ""
    }),
    __metadata("design:paramtypes", [router_1.Router, account_1.AccountService])
], StartPageComponent);
exports.default = StartPageComponent;
//# sourceMappingURL=start-page.component.js.map