import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Classroom } from "../../common/models";
import { ClassroomService } from "../../common/common";

@Component({
	selector: "admin-classroom-modal",
	templateUrl: "./classroom-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class ClassroomModalComponent {
	private activeModal: NgbActiveModal;

	private classroomService: ClassroomService;

	classroom: Classroom = {
		buildingId: null,
		name: null
	};
	
	isEditing = false;
	error = false;
	errorText = "";

	constructor(
		activeModal: NgbActiveModal,
		classroomService: ClassroomService) {
		this.activeModal = activeModal;
		this.classroomService = classroomService;
	}
	
	change(): void {
		this.error = false;
		this.errorText = "";
	}

	submit(): void {
		if (!this.classroom.name || this.classroom.name.length === 0) {
			this.error = true;
			this.errorText = "Введіть назву аудиторії.";
			return;
		}

		const action = this.isEditing
			? this.classroomService.updateClassroom(this.classroom)
			: this.classroomService.addClassroom(this.classroom);

		action.subscribe(
			() => this.activeModal.close(this.classroom),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти.";
			});

		action.connect();
	}
}
