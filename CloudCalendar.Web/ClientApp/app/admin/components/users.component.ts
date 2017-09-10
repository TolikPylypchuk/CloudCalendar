import { Component, OnInit } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import { compareByName } from "../../common/functions";

import {
	Department, Faculty, Group, Lecturer, Student
} from "../../common/models";
import { FacultyService, LecturerService } from "../../common/common";

import { AccountService } from "../../account/account";

import { LecturerModalComponent } from "./lecturer-modal.component";
import { StudentModalComponent } from "./student-modal.component";

@Component({
	selector: "admin-users",
	templateUrl: "./users.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class UsersComponent implements OnInit {
	private modalService: NgbModal;

	private accountService: AccountService;
	private facultyService: FacultyService;
	private lecturerService: LecturerService;

	faculties: Faculty[] = [];
	groups: Map<number, Group> = new Map();
	departments: Map<number, Department[]> = new Map();
	lecturers: Map<number, Lecturer[]> = new Map();
	students: Map<number, Student[]> = new Map();

	currentAdmin: Lecturer;

	constructor(
		modalService: NgbModal,
		accountService: AccountService,
		facultyService: FacultyService,
		lecturerService: LecturerService) {
		this.modalService = modalService;
		this.accountService = accountService;
		this.facultyService = facultyService;
		this.lecturerService = lecturerService;
	}

	ngOnInit(): void {
		this.facultyService.getFaculties()
			.subscribe((faculties: Faculty[]) => {
				this.faculties = faculties.sort(
					(f1, f2) => f1.name.localeCompare(f2.name));
				
				for (const faculty of faculties) {
					this.lecturerService.getLecturersByFaculty(faculty.id)
						.subscribe((lecturers: Lecturer[]) =>
							this.lecturers.set(
								faculty.id, lecturers.sort(compareByName)));
				}
			});
	}

	addLecturer(): void {
		const modalRef = this.modalService.open(LecturerModalComponent);
		const modal = modalRef.componentInstance as LecturerModalComponent;
		
		modal.faculties = this.faculties;
	}

	editLecturer(lecturer: Lecturer): void {
		const modalRef = this.modalService.open(LecturerModalComponent);
		const modal = modalRef.componentInstance as LecturerModalComponent;
		
		modal.faculties = this.faculties;
		modal.isEditing = true;

		modal.lecturer = {
			id: lecturer.id,
			email: lecturer.email,
			firstName: lecturer.firstName,
			middleName: lecturer.middleName,
			lastName: lecturer.lastName,
			departmentId: lecturer.departmentId,
			isAdmin: lecturer.isAdmin,
			isDean: lecturer.isDean,
			isHead: lecturer.isHead
		};
		
		modalRef.result.then(
			(updatedLecturer: Lecturer) => {
				lecturer.email = updatedLecturer.email;
				lecturer.firstName = updatedLecturer.firstName;
				lecturer.middleName = updatedLecturer.middleName;
				lecturer.lastName = updatedLecturer.lastName;
				lecturer.isAdmin = updatedLecturer.isAdmin;
				lecturer.isDean = updatedLecturer.isDean;
				lecturer.isHead = updatedLecturer.isHead;
			},
			() => { });
	}

	deleteLecturer(lecturer: Lecturer): void {
		const action = this.lecturerService.deleteLecturer(lecturer.id);

		action.subscribe(
				() => this.lecturers.set(
					lecturer.departmentId,
					this.lecturers.get(lecturer.departmentId).filter(
						l => l.id !== lecturer.id)),
				() => { });

		action.connect();
	}


	addStudent(): void {
		this.modalService.open(StudentModalComponent);
	}

	editStudent(student: Student): void {
		const modalRef = this.modalService.open(StudentModalComponent);
		const modal = modalRef.componentInstance as StudentModalComponent;

		modal.isEditing = true;

		modal.student = {
			id: student.id,
			email: student.email,
			firstName: student.firstName,
			middleName: student.middleName,
			lastName: student.lastName,
			groupId: student.groupId,
			isGroupLeader: student.isGroupLeader
		};

		modalRef.result.then(
			(updatedStudent: Student) => {
				student.email = updatedStudent.email;
				student.firstName = updatedStudent.firstName;
				student.middleName = updatedStudent.middleName;
				student.lastName = updatedStudent.lastName;
				student.isGroupLeader = updatedStudent.isGroupLeader;
			},
			() => { });
	}

	deleteStudent(student: Student): void {
		const action = this.lecturerService.deleteLecturer(student.id);

		action.subscribe(
			() => this.students.set(
				student.groupId,
				this.lecturers.get(student.groupId).filter(
					l => l.id !== student.id)),
			() => { });

		action.connect();
	}
}
