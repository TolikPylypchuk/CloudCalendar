import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Student } from "../../common/models";
import { StudentService } from "../../common/common";

@Component({
	selector: "admin-student-modal",
	templateUrl: "./student-modal.component.html",
	styleUrls: ["../../../css/style.css"]
})
export class StudentModalComponent {
	private activeModal: NgbActiveModal;

	private studentService: StudentService;

	student: Student = {
		email: null,
		firstName: null,
		middleName: null,
		lastName: null,
		groupId: 0,
		transcriptNumber: null,
		isGroupLeader: false
	};

	isEditing = false;
	error = false;
	errorText: string = null;

	constructor(
		activeModal: NgbActiveModal,
		studentService: StudentService) {
		this.activeModal = activeModal;
		this.studentService = studentService;
	}

	change(): void {
		this.error = false;
		this.errorText = null;
	}

	submit(): void {
		if (!this.isStudentValid()) {
			this.error = true;
			this.errorText = "Дані неправильно заповнені.";
		}

		const action = this.isEditing
			? this.studentService.updateStudent(this.student)
			: this.studentService.addStudent(this.student);

		action.subscribe(
			() => this.activeModal.close(this.student),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти";
			});

		action.connect();
	}

	isStudentValid(): boolean {
		return this.student.email && this.student.email.length !== 0 &&
			this.student.firstName && this.student.firstName.length !== 0 &&
			this.student.middleName && this.student.middleName.length !== 0 &&
			this.student.lastName && this.student.lastName.length !== 0 &&
			this.student.transcriptNumber && this.student.transcriptNumber.length !== 0 &&
			this.student.groupId !== 0;
	}
}
