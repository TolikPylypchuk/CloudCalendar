import { Component, Input, OnInit } from "@angular/core";

import { Observable } from "rxjs/Observable";

import { FileUploader } from "ng2-file-upload";

@Component({
	selector: "lecturer-modal-materials",
	templateUrl: "app/lecturer/calendar/modal/modal-materials.component.html",
	styleUrls: [ "app/lecturer/calendar/modal/modal-materials.component.css" ]
})
export default class ModalMaterialsComponent implements OnInit {
	@Input() classId: number;

	uploader: FileUploader;
	hasDropZoneOver = false;

	ngOnInit(): void {
		this.uploader = new FileUploader(
		{
			url: `api/materials/classId/${this.classId}`
		});
	}

	fileOverDropZone(e: any): void {
		this.hasDropZoneOver = e;
	}
}
