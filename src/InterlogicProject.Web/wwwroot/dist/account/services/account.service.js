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
var router_1 = require("@angular/router");
var Observable_1 = require("rxjs/Observable");
var ReplaySubject_1 = require("rxjs/ReplaySubject");
var functions_1 = require("../../common/functions");
var AccountService = (function () {
    function AccountService(http, router) {
        var _this = this;
        this.authUrl = "/api/token";
        this.currentUserUrl = "/api/users/current";
        this.currentUserSource = new ReplaySubject_1.ReplaySubject();
        this.loggedIn = false;
        this.loggingIn = false;
        this.returnUrl = null;
        this.http = http;
        this.router = router;
        var token = this.getToken();
        if (token) {
            this.loggedIn = true;
            this.http.get(this.currentUserUrl, {
                headers: this.getHeaders()
            })
                .map(function (response) {
                return response.status === 200
                    ? response.json()
                    : null;
            })
                .catch(functions_1.handleError)
                .first()
                .subscribe(function (user) { return _this.currentUserSource.next(user); });
        }
    }
    AccountService.prototype.getCurrentUser = function () {
        return this.currentUserSource.asObservable().first();
    };
    AccountService.prototype.getReturnUrl = function () {
        return this.returnUrl;
    };
    AccountService.prototype.setReturnUrl = function (returnUrl) {
        this.returnUrl = returnUrl;
    };
    AccountService.prototype.login = function (model) {
        var _this = this;
        if (this.loggedIn) {
            return Observable_1.Observable.of(null);
        }
        return this.http.post(this.authUrl, JSON.stringify(model), { headers: this.getHeaders() })
            .map(function (response) {
            var token = response.json() && response.json().token;
            if (token) {
                localStorage.setItem("ipAuthToken", token);
                _this.loggedIn = true;
                _this.http.get(_this.currentUserUrl, {
                    headers: _this.getHeaders()
                })
                    .map(function (response) {
                    return response.status === 200
                        ? response.json()
                        : null;
                })
                    .catch(functions_1.handleError)
                    .subscribe(function (user) {
                    _this.currentUserSource.next(user);
                    _this.router.navigate([_this.returnUrl ? _this.returnUrl : ""]);
                    _this.setReturnUrl("");
                });
                _this.loggingIn = false;
                return true;
            }
            return false;
        })
            .catch(functions_1.handleError)
            .first();
    };
    AccountService.prototype.logout = function () {
        localStorage.removeItem("ipAuthToken");
        this.currentUserSource = new ReplaySubject_1.ReplaySubject();
        this.loggedIn = false;
        this.router.navigate([""]);
    };
    AccountService.prototype.isLoggedIn = function () {
        return this.loggedIn;
    };
    AccountService.prototype.isLoggingIn = function () {
        return this.loggingIn;
    };
    AccountService.prototype.setLoggingIn = function (value) {
        this.loggingIn = value;
    };
    AccountService.prototype.getToken = function () {
        var token = localStorage.getItem("ipAuthToken");
        return token ? JSON.parse(token).token : null;
    };
    AccountService.prototype.getHeaders = function () {
        return this.isLoggedIn()
            ? new http_1.Headers({
                "Content-Type": "application/json",
                "Authorization": "Bearer " + this.getToken()
            })
            : new http_1.Headers({ "Content-Type": "application/json" });
    };
    return AccountService;
}());
AccountService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http, router_1.Router])
], AccountService);
exports.default = AccountService;
//# sourceMappingURL=account.service.js.map