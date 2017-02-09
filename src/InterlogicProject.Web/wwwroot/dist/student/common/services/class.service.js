System.register(["@angular/core", "@angular/http", "rxjs/Observable"], function (exports_1, context_1) {
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
    var __moduleName = context_1 && context_1.id;
    var core_1, http_1, Observable_1, ClassService;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            }
        ],
        execute: function () {
            ClassService = (function () {
                function ClassService(http) {
                    this.http = http;
                }
                ClassService.prototype.getClass = function (id) {
                    return this.http.get("api/classes/id/" + id)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                ClassService.prototype.getPlaces = function (classId) {
                    return this.http.get("api/classrooms/classId/" + classId)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                ClassService.prototype.getGroups = function (classId) {
                    return this.http.get("api/groups/classId/" + classId)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                ClassService.prototype.getLecturers = function (classId) {
                    return this.http.get("api/lecturers/classId/" + classId)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                ClassService.prototype.getComments = function (classId) {
                    return this.http.get("api/comments/classId/" + classId)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                ClassService.prototype.handleError = function (error) {
                    var message;
                    if (error instanceof http_1.Response) {
                        var body = error.json() || "";
                        var err = body.error || JSON.stringify(body);
                        message = error.status + " - " + (error.statusText || "") + " " + err;
                    }
                    else {
                        message = error.message ? error.message : error.toString();
                    }
                    console.error(message);
                    return Observable_1.Observable.throw(message);
                };
                return ClassService;
            }());
            ClassService = __decorate([
                core_1.Injectable(),
                __metadata("design:paramtypes", [http_1.Http])
            ], ClassService);
            exports_1("default", ClassService);
        }
    };
});
//# sourceMappingURL=class.service.js.map