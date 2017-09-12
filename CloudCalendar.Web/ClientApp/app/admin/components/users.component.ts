import { Component, OnInit } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import {
	compareByName, compareByLastName, getUserInitials
} from "../../common/functions";

import {
	Department, Faculty, Group, Lecturer, Student
} from "../../common/models";
import {
	DepartmentService, FacultyService, GroupService,
	LecturerService, StudentService
} from "../../common/common";

import { AccountService } from "../../account/account";

import { GroupModalComponent } from "./group-modal.component";
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
	private departmentService: DepartmentService;
	private facultyService: FacultyService;
	private groupService: GroupService;
	private lecturerService: LecturerService;
	private studentService: StudentService;

	faculties: Faculty[] = [];
	groups: Map<number, Group[]> = new Map();
	departments: Map<number, Department[]> = new Map();
	lecturers: Map<number, Lecturer[]> = new Map();
	students: Map<number, Student[]> = new Map();

	currentAdmin: Lecturer;

	constructor(
		modalService: NgbModal,
		accountService: AccountService,
		departmentService: DepartmentService,
		facultyService: FacultyService,
		groupService: GroupService,
		lecturerService: LecturerService,
		studentService: StudentService) {
		this.modalService = modalService;
		this.accountService = accountService;
		this.departmentService = departmentService;
		this.facultyService = facultyService;
		this.groupService = groupService;
		this.lecturerService = lecturerService;
		this.studentService = studentService;
	}

	ngOnInit(): void {
		this.facultyService.getFaculties()
			.subscribe((faculties: Faculty[]) => {
				this.faculties = faculties.sort(compareByName);

				for (const faculty of faculties) {
					this.departmentService.getDepartmentsByFaculty(faculty.id)
						.subscribe((departments: Department[]) => {
							this.departments.set(
								faculty.id,
								departments.sort(compareByName));

							for (const department of departments) {
								this.lecturerService.getLecturersByDepartment(
									department.id)
									.subscribe((lecturers: Lecturer[]) => 
										this.lecturers.set(
											department.id,
											lecturers.sort(compareByLastName)));

								this.groupService.getGroupsByDepartment(
										department.id)
									.subscribe((groups: Group[]) => {
										this.groups.set(
											department.id,
											groups.sort(compareByName));

										for (const group of groups) {
											this.studentService.getStudentsByGroup(group.id)
												.subscribe((students: Student[]) =>
													this.students.set(
														group.id,
														students.sort(compareByLastName)));
										}
									});
							}
						});
				}
			});
	}

	addLecturer(departmentId: number): void {
		const modalRef = this.modalService.open(LecturerModalComponent);
		const modal = modalRef.componentInstance as LecturerModalComponent;
		
		modal.lecturer.departmentId = departmentId;

		modalRef.result.then(
			() => this.lecturers.get(departmentId).push(modal.lecturer),
			() => { });
	}

	editLecturer(lecturer: Lecturer): void {
		const modalRef = this.modalService.open(LecturerModalComponent);
		const modal = modalRef.componentInstance as LecturerModalComponent;
		
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
	
	addStudent(groupId: number): void {
		const modalRef = this.modalService.open(StudentModalComponent);
		const modal = modalRef.componentInstance as StudentModalComponent;

		modal.student.groupId = groupId;

		modalRef.result.then(
			() => this.students.get(groupId).push(modal.student),
			() => { });
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
			transcriptNumber: student.transcriptNumber,
			isGroupLeader: student.isGroupLeader
		};

		modalRef.result.then(
			(updatedStudent: Student) => {
				student.email = updatedStudent.email;
				student.firstName = updatedStudent.firstName;
				student.middleName = updatedStudent.middleName;
				student.lastName = updatedStudent.lastName;
				student.transcriptNumber = updatedStudent.transcriptNumber;
				student.isGroupLeader = updatedStudent.isGroupLeader;
			},
			() => { });
	}

	deleteStudent(student: Student): void {
		const action = this.studentService.deleteStudent(student.id);

		action.subscribe(
			() => this.students.set(
				student.groupId,
				this.students.get(student.groupId).filter(
					s => s.id !== student.id)),
			() => { });

		action.connect();
	}

	addGroup(departmentId: number): void {
		const modalRef = this.modalService.open(GroupModalComponent);
		const modal = modalRef.componentInstance as GroupModalComponent;

		modal.departmentId = departmentId;
		modal.lecturers = this.lecturers.get(departmentId);

		modalRef.result.then(
			() => this.groups.get(departmentId).push(modal.group),
			() => { });
	}

	editGroup(group: Group, departmentId: number): void {
		const modalRef = this.modalService.open(GroupModalComponent);
		const modal = modalRef.componentInstance as GroupModalComponent;

		modal.isEditing = true;
		modal.lecturers = this.lecturers.get(departmentId);

		modal.group = {
			id: group.id,
			name: group.name,
			curatorId: group.curatorId,
			year: group.year
		};

		modalRef.result.then(
			(updatedStudent: Group) => {
				group.name = updatedStudent.name;
				group.curatorId = updatedStudent.curatorId;
				group.year = updatedStudent.year;
			},
			() => { });
	}

	deleteGroup(group: Group, departmentId: number): void {
		const action = this.groupService.deleteGroup(group.id);

		action.subscribe(
			() => this.groups.set(
				departmentId,
				this.groups.get(departmentId).filter(
					g => g.id !== group.id)),
			() => { });

		action.connect();
	}

	getUserInitials = getUserInitials;
}
