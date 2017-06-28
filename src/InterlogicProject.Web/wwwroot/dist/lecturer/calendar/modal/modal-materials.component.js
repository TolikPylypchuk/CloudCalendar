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
var common_1 = require("../../../common/common");
var ModalMaterialsComponent = (function () {
    function ModalMaterialsComponent(materialService) {
        this.materials = [];
        this.hasDropZoneOver = false;
        this.materialService = materialService;
    }
    ModalMaterialsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.uploader = new ng2_file_upload_1.FileUploader({
            url: "api/materials/classId/" + this.classId
        });
        this.materialService.getMaterialsByClass(this.classId)
            .subscribe(function (materials) {
            _this.materials = materials;
        });
        this.uploader.onCompleteAll = function () {
            _this.uploader.clearQueue();
            _this.materialService.getMaterialsByClass(_this.classId)
                .subscribe(function (materials) {
                _this.materials = materials;
            });
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
        __metadata("design:paramtypes", [common_1.MaterialService])
    ], ModalMaterialsComponent);
    return ModalMaterialsComponent;
}());
exports.default = ModalMaterialsComponent;
//# sourceMappingURL=modal-materials.component.js.map