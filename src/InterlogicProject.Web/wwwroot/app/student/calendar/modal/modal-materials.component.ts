import { Component, Input, OnInit } from "@angular/core";

import { ClassService } from "../../../common/common";
import { Material } from "../../../common/models";

@Component({
	selector: "ip-student-modal-materials",
	templateUrl: "/templates/student/calendarModalMaterials",
	styleUrls: [ "/dist/css/style.min.css" ]
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
