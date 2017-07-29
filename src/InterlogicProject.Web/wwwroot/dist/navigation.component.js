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
var account_1 = require("./account/account");
var common_1 = require("./common/common");
var NavigationComponent = (function () {
    function NavigationComponent(accoutService, notificationService) {
        this.notificationCount = 0;
        this.accoutService = accoutService;
        this.notificationService = notificationService;
    }
    NavigationComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.notificationService.getNotificationsForCurrentUser()
            .map(function (notifications) {
            return notifications.filter(function (n) { return !n.isSeen; }).length;
        })
            .subscribe(function (count) { return _this.notificationCount = count; });
        this.notificationService.notificationsMarkObservable()
            .subscribe(function (seen) {
            return seen ? _this.notificationCount-- : _this.notificationCount++;
        });
    };
    NavigationComponent.prototype.isLoggedIn = function () {
        return this.accoutService.isLoggedIn();
    };
    NavigationComponent.prototype.getUserName = function () {
        return this.accoutService.getCurrentUser()
            .map(function (user) { return user.firstName + " " + user.lastName; })
            .first();
    };
    NavigationComponent = __decorate([
        core_1.Component({
            selector: "ip-navigation",
            templateUrl: "templates/navigation"
        }),
        __metadata("design:paramtypes", [account_1.AccountService,
            common_1.NotificationService])
    ], NavigationComponent);
    return NavigationComponent;
}());
exports.default = NavigationComponent;
//# sourceMappingURL=navigation.component.js.map