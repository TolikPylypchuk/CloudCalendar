import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Building } from "../../common/models";
import { BuildingService } from "../../common/common";

@Component({
	selector: "admin-building-modal",
	templateUrl: "./building-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class BuildingModalComponent {
	private activeModal: NgbActiveModal;

	private buildingService: BuildingService;

	building: Building = {
		name: null,
		address: null
	};

	isEditing = false;
	error = false;
	errorText = "";

	constructor(activeModal: NgbActiveModal, buildingService: BuildingService) {
		this.activeModal = activeModal;
		this.buildingService = buildingService;
	}

	change(): void {
		this.error = false;
		this.errorText = "";
	}

	submit(): void {
		if (!this.building.name || this.building.name.length === 0 ||
			!this.building.address || this.building.address.length === 0) {
			this.error = true;
			this.errorText = "Заповніть усі поля.";
			return;
		}

		const action = this.isEditing
			? this.buildingService.updateBuilding(this.building)
			: this.buildingService.addBuilding(this.building);

		action.subscribe(
			() => this.activeModal.close(this.building),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти.";
			});

		action.connect();
	}
}
