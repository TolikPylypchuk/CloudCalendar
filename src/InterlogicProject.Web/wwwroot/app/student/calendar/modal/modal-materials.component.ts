import { Component, Input, OnInit } from "@angular/core";

import { MaterialService } from "../../../common/common";
import { Material } from "../../../common/models";

@Component({
	selector: "ip-student-modal-materials",
	templateUrl: "/templates/student/calendar/modal-materials",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class ModalMaterialsComponent implements OnInit {
	@Input() classId: number;

	materialService: MaterialService;

	materials: Material[] = [];
	
	constructor(classService: MaterialService) {
		this.materialService = classService;
	}

	ngOnInit(): void {
		this.materialService.getMaterialsByClass(this.classId)
			.subscribe(materials => {
				this.materials = materials;
			});
	}
}
