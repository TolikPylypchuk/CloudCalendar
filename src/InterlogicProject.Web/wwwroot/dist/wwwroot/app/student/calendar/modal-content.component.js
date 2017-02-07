System.register(["@angular/core", "@angular/http", "@ng-bootstrap/ng-bootstrap"], function (exports_1, context_1) {
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
    var core_1, http_1, ng_bootstrap_1, ModalContentComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (ng_bootstrap_1_1) {
                ng_bootstrap_1 = ng_bootstrap_1_1;
            }
        ],
        execute: function () {
            ModalContentComponent = (function () {
                function ModalContentComponent(activeModal, http) {
                    this.activeModal = activeModal;
                    this.http = http;
                }
                ModalContentComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var classRequest = this.http.get("/api/classes/id/" + this.classId);
                    classRequest.map(function (response) { return response.json(); })
                        .subscribe(function (data) {
                        _this.currentClass = data;
                    });
                    var lecturersRequest = this.http.get("/api/lecturers/classId/" + this.classId);
                    lecturersRequest.map(function (response) { return response.json(); })
                        .subscribe(function (data) {
                        _this.currentLecturers = data;
                    });
                };
                return ModalContentComponent;
            }());
            ModalContentComponent = __decorate([
                core_1.Component({
                    selector: "student-modal-content",
                    templateUrl: "app/student/calendar/modal-content.component.html"
                }),
                __metadata("design:paramtypes", [ng_bootstrap_1.NgbActiveModal, http_1.Http])
            ], ModalContentComponent);
            exports_1("default", ModalContentComponent);
        }
    };
});
//# sourceMappingURL=modal-content.component.js.map