System.register(["@angular/core", "@angular/http", "../common/common"], function (exports_1, context_1) {
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
    var core_1, http_1, common_1, ModalCommentsComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            }
        ],
        execute: function () {
            ModalCommentsComponent = (function () {
                function ModalCommentsComponent(http, classService) {
                    this.comments = [];
                    this.http = http;
                    this.classService = classService;
                }
                ModalCommentsComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.classService.getComments(this.classId)
                        .subscribe(function (data) { return _this.comments = data; });
                };
                return ModalCommentsComponent;
            }());
            __decorate([
                core_1.Input(),
                __metadata("design:type", Number)
            ], ModalCommentsComponent.prototype, "classId", void 0);
            ModalCommentsComponent = __decorate([
                core_1.Component({
                    selector: "student-modal-comments",
                    templateUrl: "app/student/calendar/modal-comments.component.html"
                }),
                __metadata("design:paramtypes", [http_1.Http, common_1.ClassService])
            ], ModalCommentsComponent);
            exports_1("default", ModalCommentsComponent);
        }
    };
});
//# sourceMappingURL=modal-comments.component.js.map