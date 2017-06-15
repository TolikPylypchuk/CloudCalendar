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
var HomeworkService = (function () {
    function HomeworkService(http) {
        this.materials = "/api/materials";
        this.http = http;
    }
    HomeworkService.prototype.getMaterials = function () {
        return this.http.get(this.materials, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.getMaterial = function (id) {
        return this.http.get(this.materials + "/" + id, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.getMaterialsByClass = function (classId) {
        return this.http.get(this.materials + "/classId/" + classId, { headers: functions_1.getHeaders() })
            .map(function (response) { return response.json(); })
            .first();
    };
    HomeworkService.prototype.updateMaterial = function (material) {
        return this.http.put(this.materials + "/" + material.id, JSON.stringify(material), {
            headers: new http_1.Headers({ "Content-Type": "application/json" })
        })
            .first()
            .publish();
    };
    HomeworkService.prototype.deleteMaterial = function (id) {
        return this.http.delete(this.materials + "/" + id)
            .first()
            .publish();
    };
    return HomeworkService;
}());
HomeworkService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], HomeworkService);
exports.default = HomeworkService;
//# sourceMappingURL=material.service.js.map