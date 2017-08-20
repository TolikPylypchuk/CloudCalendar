import { Component, Input, OnInit } from "@angular/core";

import { MaterialService } from "../../../common/common";
import { Material } from "../../../common/models";

@Component({
	selector: "student-modal-materials",
	templateUrl: "./modal-materials.component.html",
	styleUrls: [ "../../../../css/style.css" ]
})
export class ModalMaterialsComponent implements OnInit {
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
