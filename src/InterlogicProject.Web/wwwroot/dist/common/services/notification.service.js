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
var http_1 = require("@angular/http");
var functions_1 = require("../functions");
var NotificationService = (function () {
    function NotificationService(http) {
        this.notifications = "/api/notifications";
        this.http = http;
    }
    NotificationService.prototype.getNotifications = function () {
        return this.http.get(this.notifications, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    NotificationService.prototype.getNotification = function (id) {
        return this.http.get(this.notifications + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    NotificationService.prototype.getNotificationsForUser = function (userId) {
        return this.http.get(this.notifications + "/userId/" + userId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    NotificationService.prototype.getNotificationsInRange = function (start, end) {
        return this.http.get(this.notifications + "/range/" + start.format("YYYY-MM-DD") + "/" +
            ("" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    NotificationService.prototype.getNotificationsForUserInRange = function (userId, start, end) {
        return this.http.get(this.notifications + "/userId/" + userId + "/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")), { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    NotificationService.prototype.addNotification = function (notification) {
        var action = this.http.post(this.notifications, JSON.stringify(notification), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            notification.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    NotificationService.prototype.updateNotification = function (notification) {
        return this.http.put(this.notifications + "/" + notification.id, JSON.stringify(notification), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    NotificationService.prototype.deleteNotification = function (id) {
        return this.http.delete(this.notifications + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    return NotificationService;
}());
NotificationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], NotificationService);
exports.default = NotificationService;
//# sourceMappingURL=notification.service.js.map