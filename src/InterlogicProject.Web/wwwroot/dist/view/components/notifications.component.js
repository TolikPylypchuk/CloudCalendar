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
var moment = require("moment");
var common_1 = require("../../common/common");
var NotificationsComponent = (function () {
    function NotificationsComponent(notificationService) {
        this.notifications = [];
        this.notificationService = notificationService;
    }
    NotificationsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.notificationService.getNotificationsForCurrentUser()
            .subscribe(function (notifications) { return _this.notifications = notifications; });
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
    NotificationsComponent = __decorate([
        core_1.Component({
            selector: "ip-view-notifications",
            templateUrl: "/templates/view/notifications"
        }),
        __metadata("design:paramtypes", [common_1.NotificationService])
    ], NotificationsComponent);
    return NotificationsComponent;
}());
exports.default = NotificationsComponent;
//# sourceMappingURL=notifications.component.js.map