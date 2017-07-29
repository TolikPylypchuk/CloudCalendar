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
var moment = require("moment");
var account_1 = require("../../account/account");
var common_1 = require("../../common/common");
var NotificationsComponent = (function () {
    function NotificationsComponent(router, accountService, classService, notificationService) {
        this.notifications = [];
        this.router = router;
        this.accountService = accountService;
        this.classService = classService;
        this.notificationService = notificationService;
    }
    NotificationsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.notificationService.getNotificationsForCurrentUser()
            .subscribe(function (notifications) {
            return _this.notifications = notifications.sort(_this.compareByTimeDescending);
        });
    };
    NotificationsComponent.prototype.formatDateTime = function (dateTime) {
        return moment(dateTime).format("DD.MM.YYYY HH:mm");
    };
    NotificationsComponent.prototype.markAsSeen = function (notification) {
        this.notificationService
            .markNotificationAsSeen(notification)
            .connect();
    };
    NotificationsComponent.prototype.markAsNotSeen = function (notification) {
        this.notificationService
            .markNotificationAsNotSeen(notification)
            .connect();
    };
    NotificationsComponent.prototype.goToClass = function (notification) {
        var _this = this;
        this.classService.getClass(notification.classId)
            .subscribe(function (c) {
            if (c) {
                _this.accountService.isStudent()
                    .subscribe(function (isStudent) {
                    var path = isStudent ? "student" : "lecturer";
                    var dateTime = moment(c.dateTime);
                    _this.router.navigate([
                        path + "/calendar",
                        dateTime.format("YYYY-MM-DD"),
                        dateTime.format("HH:mm")
                    ]);
                });
            }
        });
    };
    NotificationsComponent.prototype.compareByTimeDescending = function (a, b) {
        var moment1 = moment(a.dateTime);
        var moment2 = moment(b.dateTime);
        return moment1.isBefore(moment2)
            ? 1
            : moment1.isAfter(moment2)
                ? -1
                : 0;
    };
    NotificationsComponent = __decorate([
        core_1.Component({
            selector: "ip-view-notifications",
            templateUrl: "/templates/view/notifications"
        }),
        __metadata("design:paramtypes", [router_1.Router,
            account_1.AccountService,
            common_1.ClassService,
            common_1.NotificationService])
    ], NotificationsComponent);
    return NotificationsComponent;
}());
exports.default = NotificationsComponent;
//# sourceMappingURL=notifications.component.js.map