import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Subject } from "../../common/models";
import { SubjectService } from "../../common/common";

@Component({
	selector: "admin-subject-modal",
	templateUrl: "./subject-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class SubjectModalComponent {
	private activeModal: NgbActiveModal;

	private subjectService: SubjectService;

	subject: Subject = {
		name: null
	};

	isEditing = false;
	error = false;
	errorText = "";

	constructor(
		activeModal: NgbActiveModal,
		subjectService: SubjectService) {
		this.activeModal = activeModal;
		this.subjectService = subjectService;
	}
	
	change(): void {
		this.error = false;
		this.errorText = "";
	}
	
	submit(): void {
		if (!this.subject.name || this.subject.name.length === 0) {
			this.error = true;
			this.errorText = "Введіть назву предмету.";
			return;
		}

		const action = this.isEditing
			? this.subjectService.updateSubject(this.subject)
			: this.subjectService.addSubject(this.subject);

		action.subscribe(
			() => this.activeModal.close(this.subject),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти.";
			});

		action.connect();
	}
}
