import { Component, Input, OnInit } from "@angular/core";

import { Observable } from "rxjs/Observable";

import { FileUploader } from "ng2-file-upload";

import { Material } from "../../../common/models";
import { ClassService } from "../../common/common";

@Component({
	selector: "lecturer-modal-materials",
	templateUrl: "app/lecturer/calendar/modal/modal-materials.component.html",
	styleUrls: [ "app/lecturer/calendar/modal/modal-materials.component.css" ]
})
export default class ModalMaterialsComponent implements OnInit {
	@Input() classId: number;

	classService: ClassService;

	materials: Material[] = [];

	uploader: FileUploader;
	hasDropZoneOver = false;

	constructor(classService: ClassService) {
		this.classService = classService;
	}

	ngOnInit(): void {
		this.uploader = new FileUploader(
		{
			url: `api/materials/classId/${this.classId}`
		});

		this.classService.getMaterials(this.classId)
			.subscribe(materials => {
				this.materials = materials;
			});

		this.uploader.onCompleteAll = () => {
			this.uploader.clearQueue();

			this.classService.getMaterials(this.classId)
				.subscribe(materials => {
					this.materials = materials;
				});
		};
	}

	deleteMaterial(material: Material): void {
		this.classService.deleteMaterial(material.id)
			.subscribe(response => {
				if (response.status === 204) {
					this.materials = this.materials.filter(
						m => m.id !== material.id);
				}
			});
	}
	
	fileOverDropZone(e: any): void {
		this.hasDropZoneOver = e;
	}
}
