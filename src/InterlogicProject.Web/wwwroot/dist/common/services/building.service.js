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
var BuildingService = (function () {
    function BuildingService(http) {
        this.buildings = "/api/buildings";
        this.http = http;
    }
    BuildingService.prototype.getBuildings = function () {
        return this.http.get(this.buildings, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    BuildingService.prototype.getBuilding = function (id) {
        return this.http.get(this.buildings + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    BuildingService.prototype.addBuilding = function (building) {
        var action = this.http.post(this.buildings, JSON.stringify(building), { headers: functions_1.getHeaders() })
            .first()
            .publish();
        action.subscribe(function (response) {
            var location = response.headers.get("Location");
            building.id = +location.substr(location.lastIndexOf("/") + 1);
        });
        return action;
    };
    BuildingService.prototype.updateBuilding = function (building) {
        return this.http.put(this.buildings + "/" + building.id, JSON.stringify(building), { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    BuildingService.prototype.deleteBuilding = function (id) {
        return this.http.delete(this.buildings + "/" + id, { headers: functions_1.getHeaders() })
            .first()
            .publish();
    };
    return BuildingService;
}());
BuildingService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], BuildingService);
exports.default = BuildingService;
//# sourceMappingURL=building.service.js.map