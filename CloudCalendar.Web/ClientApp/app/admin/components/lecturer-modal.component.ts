import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Department, Faculty, Lecturer } from "../../common/models";
import {
	DepartmentService, FacultyService, LecturerService
} from "../../common/common";

@Component({
	selector: "admin-lecturer-modal",
	templateUrl: "./lecturer-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class LecturerModalComponent {
	private activeModal: NgbActiveModal;

	private departmentService: DepartmentService;
	private facultyService: FacultyService;
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

	faculties: Faculty[] = [];
	departments: Map<number, Department[]> = new Map();

	currentFaculty = null as Faculty;
	currentDepartment = null as Department;

	isEditing = false;
	error = false;
	errorText: string = null;

	constructor(
		activeModal: NgbActiveModal,
		departmentService: DepartmentService,
		facultyService: FacultyService,
		lecturerService: LecturerService) {
		this.activeModal = activeModal;
		this.departmentService = departmentService;
		this.facultyService = facultyService;
		this.lecturerService = lecturerService;
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

	isStudentValid(): boolean {
		return this.lecturer.email && this.lecturer.email.length !== 0 &&
			this.lecturer.firstName && this.lecturer.firstName.length !== 0 &&
			this.lecturer.middleName && this.lecturer.middleName.length !== 0 &&
			this.lecturer.lastName && this.lecturer.lastName.length !== 0 &&
			this.lecturer.departmentId !== 0;
	}
}
