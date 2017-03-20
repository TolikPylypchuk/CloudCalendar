import { Component, Input, OnInit } from "@angular/core";

import { Observable } from "rxjs/Observable";

import { Material } from "../../../common/models";
import { ClassService } from "../../common/common";

@Component({
	selector: "student-modal-materials",
	templateUrl: "app/student/calendar/modal/modal-materials.component.html",
	styleUrls: [ "app/student/calendar/modal/modal-materials.component.css" ]
})
export default class ModalMaterialsComponent implements OnInit {
	@Input() classId: number;

	classService: ClassService;

	materials: Material[] = [];
	
	constructor(classService: ClassService) {
		this.classService = classService;
	}

	ngOnInit(): void {
		this.classService.getMaterials(this.classId)
			.subscribe(materials => {
				this.materials = materials;
			});
	}
}
