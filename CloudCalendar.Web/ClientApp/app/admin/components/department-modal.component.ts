import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Department } from "../../common/models";
import { DepartmentService } from "../../common/common";

@Component({
	selector: "admin-department-modal",
	templateUrl: "./department-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class DepartmentModalComponent {
	private activeModal: NgbActiveModal;

	private departmentService: DepartmentService;

	department: Department = {
		name: null
	};

	isEditing = false;
	error = false;
	errorText = "";

	constructor(
		activeModal: NgbActiveModal,
		departmentService: DepartmentService) {
		this.activeModal = activeModal;
		this.departmentService = departmentService;
	}
	
	change(): void {
		this.error = false;
		this.errorText = "";
	}
	
	submit(): void {
		if (!this.department.name || this.department.name.length === 0) {
			this.error = true;
			this.errorText = "Введіть назву кафедри.";
			return;
		}

		const action = this.isEditing
			? this.departmentService.updateDepartment(this.department)
			: this.departmentService.addDepartment(this.department);

		action.subscribe(
			() => this.activeModal.close(this.department),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти.";
			});

		action.connect();
	}
}
