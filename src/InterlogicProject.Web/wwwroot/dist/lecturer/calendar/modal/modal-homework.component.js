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
var common_1 = require("../../common/common");
var ModalHomeworkComponent = (function () {
    function ModalHomeworkComponent(classService) {
        this.homeworks = [];
        this.classService = classService;
    }
    ModalHomeworkComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.classService.getClass(this.classId)
            .subscribe(function (c) { return _this.currentClass = c; });
        this.classService.getHomeworks(this.classId)
            .subscribe(function (homeworks) { return _this.homeworks = homeworks; });
    };
    return ModalHomeworkComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], ModalHomeworkComponent.prototype, "classId", void 0);
ModalHomeworkComponent = __decorate([
    core_1.Component({
        selector: "lecturer-modal-homework",
        templateUrl: "app/lecturer/calendar/modal/modal-homework.component.html",
        styleUrls: ["app/lecturer/calendar/modal/modal-homework.component.css"]
    }),
    __metadata("design:paramtypes", [common_1.ClassService])
], ModalHomeworkComponent);
exports.default = ModalHomeworkComponent;
//# sourceMappingURL=modal-homework.component.js.map