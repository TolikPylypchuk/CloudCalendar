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
var ng2_file_upload_1 = require("ng2-file-upload");
var moment = require("moment");
var common_1 = require("../../../common/common");
var functions_1 = require("../../../common/functions");
var ModalMaterialsComponent = (function () {
    function ModalMaterialsComponent(classService, lecturerService, materialService, notificationService) {
        this.materials = [];
        this.hasDropZoneOver = false;
        this.classService = classService;
        this.lecturerService = lecturerService;
        this.materialService = materialService;
        this.notificationService = notificationService;
    }
    ModalMaterialsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.uploader = new ng2_file_upload_1.FileUploader({
            url: "/api/materials/classId/" + this.classId,
            authToken: "Bearer " + functions_1.getAuthToken(),
            removeAfterUpload: true
        });
        this.classService.getClass(this.classId)
            .subscribe(function (c) { return _this.currentClass = c; });
        this.lecturerService.getCurrentLecturer()
            .subscribe(function (lecturer) { return _this.currentLecturer = lecturer; });
        this.materialService.getMaterialsByClass(this.classId)
            .subscribe(function (materials) { return _this.materials = materials; });
        this.uploader.onCompleteAll = function () {
            _this.materialService.getMaterialsByClass(_this.classId)
                .subscribe(function (materials) { return _this.materials = materials; });
            _this.notificationService.addNotificationForGroupsInClass({
                dateTime: moment().toISOString(),
                text: _this.getNotificationText(),
                classId: _this.classId
            }, _this.classId)
                .connect();
        };
    };
    ModalMaterialsComponent.prototype.deleteMaterial = function (material) {
        var _this = this;
        var action = this.materialService.deleteMaterial(material.id);
        action.subscribe(function (response) {
            if (response.status === 204) {
                _this.materials = _this.materials.filter(function (m) { return m.id !== material.id; });
            }
        });
        action.connect();
    };
    ModalMaterialsComponent.prototype.fileOverDropZone = function (e) {
        this.hasDropZoneOver = e;
    };
    ModalMaterialsComponent.prototype.getNotificationText = function () {
        return this.currentLecturer.firstName + " " +
            (this.currentLecturer.lastName + " \u0434\u043E\u0434\u0430\u0432 \u043C\u0430\u0442\u0435\u0440\u0456\u0430\u043B\u0438") +
            ("\u0434\u043E \u043F\u0430\u0440\u0438 '" + this.currentClass.subjectName + "' ") +
            (moment(this.currentClass.dateTime).format("DD.MM.YYYY") + ".");
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], ModalMaterialsComponent.prototype, "classId", void 0);
    ModalMaterialsComponent = __decorate([
        core_1.Component({
            selector: "ip-lecturer-modal-materials",
            templateUrl: "/templates/lecturer/calendar/modal-materials",
            styleUrls: ["/dist/css/style.min.css"]
        }),
        __metadata("design:paramtypes", [common_1.ClassService,
            common_1.LecturerService,
            common_1.MaterialService,
            common_1.NotificationService])
    ], ModalMaterialsComponent);
    return ModalMaterialsComponent;
}());
exports.default = ModalMaterialsComponent;
//# sourceMappingURL=modal-materials.component.js.map