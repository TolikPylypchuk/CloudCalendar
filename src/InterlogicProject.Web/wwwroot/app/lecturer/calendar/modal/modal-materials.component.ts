import { Component, Input, OnInit } from "@angular/core";

import { FileUploader } from "ng2-file-upload";

import { MaterialService } from "../../../common/common";
import { Material } from "../../../common/models";

@Component({
	selector: "ip-lecturer-modal-materials",
	templateUrl: "/templates/lecturer/calendar/modal-materials",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class ModalMaterialsComponent implements OnInit {
	@Input() classId: number;

	materials: Material[] = [];

	uploader: FileUploader;
	hasDropZoneOver = false;

	private materialService: MaterialService;

	constructor(materialService: MaterialService) {
		this.materialService = materialService;
	}

	ngOnInit(): void {
		this.uploader = new FileUploader(
		{
			url: `api/materials/classId/${this.classId}`
		});

		this.materialService.getMaterialsByClass(this.classId)
			.subscribe(materials => {
				this.materials = materials;
			});

		this.uploader.onCompleteAll = () => {
			this.uploader.clearQueue();

			this.materialService.getMaterialsByClass(this.classId)
				.subscribe(materials => {
					this.materials = materials;
				});
		};
	}

	deleteMaterial(material: Material): void {
		const action = this.materialService.deleteMaterial(material.id);

		action.subscribe(response => {
			if (response.status === 204) {
				this.materials = this.materials.filter(
					m => m.id !== material.id);
			}
		});

		action.connect();
	}
	
	fileOverDropZone(e: any): void {
		this.hasDropZoneOver = e;
	}
}
