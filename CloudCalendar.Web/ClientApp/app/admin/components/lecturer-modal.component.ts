import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Lecturer } from "../../common/models";
import { LecturerService } from "../../common/common";

@Component({
	selector: "admin-lecturer-modal",
	templateUrl: "./lecturer-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class LecturerModalComponent {
	private activeModal: NgbActiveModal;

	private lecturerService: LecturerService;

	lecturer: Lecturer = {
		email: null,
		firstName: null,
		middleName: null,
		lastName: null,
		departmentId: 0,
		isAdmin: false,
		isDean: false,
		isHead: false
	};
	
	isEditing = false;
	error = false;
	errorText: string = null;

	constructor(
		activeModal: NgbActiveModal,
		lecturerService: LecturerService) {
		this.activeModal = activeModal;
		this.lecturerService = lecturerService;
	}
	
	change(): void {
		this.error = false;
		this.errorText = null;
	}

	submit(): void {
		if (!this.isLecturerValid()) {
			this.error = true;
			this.errorText = "Дані неправильно заповнені.";
		}

		const action = this.isEditing
			? this.lecturerService.updateLecturer(this.lecturer)
			: this.lecturerService.addLecturer(this.lecturer);

		action.subscribe(
			() => this.activeModal.close(this.lecturer),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти";
		});

		action.connect();
	}

	isLecturerValid(): boolean {
		return this.lecturer.email && this.lecturer.email.length !== 0 &&
			this.lecturer.firstName && this.lecturer.firstName.length !== 0 &&
			this.lecturer.middleName && this.lecturer.middleName.length !== 0 &&
			this.lecturer.lastName && this.lecturer.lastName.length !== 0 &&
			this.lecturer.departmentId !== 0;
	}
}
