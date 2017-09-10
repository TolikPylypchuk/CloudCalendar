import { Component, OnInit } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import {
	Building, Department, Faculty, Classroom, Subject
} from "../../common/models";

import {
	BuildingService, ClassroomService, DepartmentService,
	FacultyService, SubjectService
} from "../../common/common";

import { BuildingModalComponent } from "./building-modal.component";
import { ClassroomModalComponent } from "./classroom-modal.component";
import { DepartmentModalComponent } from "./department-modal.component";
import { FacultyModalComponent } from "./faculty-modal.component";
import { SubjectModalComponent } from "./subject-modal.component";

@Component({
	selector: "admin-settings",
	templateUrl: "./settings.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class SettingsComponent implements OnInit {
	private modalService: NgbModal;

	private buildingService: BuildingService;
	private classroomService: ClassroomService;
	private departmentService: DepartmentService;
	private facultyService: FacultyService;
	private subjectService: SubjectService;

	faculties: Faculty[] = [];
	departments: Map<number, Department[]> = new Map();

	buildings: Building[] = [];
	classrooms: Map<number, Classroom[]> = new Map();

	subjects: Subject[];

	constructor(
		modalService: NgbModal,
		buildingService: BuildingService,
		classroomService: ClassroomService,
		departmentService: DepartmentService,
		facultyService: FacultyService,
		subjectService: SubjectService) {
		this.modalService = modalService;

		this.buildingService = buildingService;
		this.classroomService = classroomService;
		this.departmentService = departmentService;
		this.facultyService = facultyService;
		this.subjectService = subjectService;
	}

	ngOnInit(): void {
		this.facultyService.getFaculties()
			.subscribe((faculties: Faculty[]) => {
				this.faculties = faculties.sort(
					(b1, b2) => b1.name.localeCompare(b2.name));

				for (const faculty of faculties) {
					this.departmentService.getDepartmentsByFaculty(faculty.id)
						.subscribe((departments: Department[]) =>
							this.departments.set(
								faculty.id,
								departments.sort(
									(c1, c2) => c1.name.localeCompare(c2.name))));
				}
			});
		
		this.buildingService.getBuildings()
			.subscribe((buildings: Building[]) => {
					this.buildings = buildings.sort(
						(b1, b2) => b1.name.localeCompare(b2.name));

					for (const building of buildings) {
						this.classroomService.getClassroomsByBuilding(building.id)
							.subscribe((classrooms: Classroom[]) =>
								this.classrooms.set(
									building.id,
									classrooms.sort(
										(c1, c2) => c1.name.localeCompare(c2.name))));
					}
				});

		this.subjectService.getSubjects()
			.subscribe((subjects: Subject[]) =>
				this.subjects = subjects.sort(
					(s1, s2) => s1.name.localeCompare(s2.name)));
	}

	addFacultyClicked(): void {
		const modalRef = this.modalService.open(FacultyModalComponent);

		modalRef.result.then(
			(faculty: Faculty) => {
				this.faculties.push(faculty);
				this.faculties.sort(
					(f1, f2) => f1.name.localeCompare(f2.name));
			},
			() => { });
	}

	editFacultyClicked(faculty: Faculty): void {
		const modalRef = this.modalService.open(FacultyModalComponent);
		const modal = modalRef.componentInstance as FacultyModalComponent;

		modal.isEditing = true;
		modal.faculty = {
			id: faculty.id,
			name: faculty.name
		};

		modalRef.result.then(
			(updatedFaculty: Faculty) => faculty.name = updatedFaculty.name,
			() => { });
	}

	deleteFacultyClicked(id: number): void {
		const action = this.facultyService.deleteFaculty(id);
		action.subscribe(
			() => this.faculties = this.faculties.filter(f => f.id !== id),
			() => { });

		action.connect();
	}

	addDepartmentClicked(faculty: Faculty): void {
		const modalRef = this.modalService.open(DepartmentModalComponent);
		const modal = modalRef.componentInstance as DepartmentModalComponent;

		modal.department.facultyId = faculty.id;

		modalRef.result.then(
			(department: Classroom) => {
				this.departments.get(faculty.id).push(department);
				this.departments.get(faculty.id).sort(
					(d1, d2) => d1.name.localeCompare(d2.name));
			},
			() => { });
	}

	editDepartmentClicked(department: Department): void {
		const modalRef = this.modalService.open(DepartmentModalComponent);
		const modal = modalRef.componentInstance as DepartmentModalComponent;

		modal.isEditing = true;

		modal.department = {
			id: department.id,
			facultyId: department.facultyId,
			name: department.name
		};

		modalRef.result.then(
			(updatedDepartment: Department) =>
				department.name = updatedDepartment.name,
			() => { });
	}

	deleteDepartmentClicked(deleteDepartment: Department): void {
		const action = this.departmentService.deleteDepartment(deleteDepartment.id);
		action.subscribe(
			() => this.departments.set(
				deleteDepartment.facultyId,
				this.departments.get(deleteDepartment.facultyId).filter(
					d => d.id !== deleteDepartment.id)),
			() => { });

		action.connect();
	}

	addBuildingClicked(): void {
		const modalRef = this.modalService.open(BuildingModalComponent);

		modalRef.result.then(
			(building: Building) => {
				this.classrooms.set(building.id, []);
				this.buildings.push(building);
				this.buildings.sort(
					(b1, b2) => b1.name.localeCompare(b2.name));
			},
			() => { });
	}

	editBuildingClicked(building: Building): void {
		const modalRef = this.modalService.open(BuildingModalComponent);
		const modal = modalRef.componentInstance as BuildingModalComponent;

		modal.isEditing = true;
		modal.building = {
			id: building.id,
			name: building.name,
			address: building.address
		};

		modalRef.result.then(
			(updatedBuilding: Building) => {
				building.name = updatedBuilding.name;
				building.address = updatedBuilding.address;
			},
			() => { });
	}

	deleteBuildingClicked(id: number): void {
		const action = this.buildingService.deleteBuilding(id);
		action.subscribe(
			() => {
				this.buildings = this.buildings.filter(b => b.id !== id);
				this.classrooms.delete(id);
			},
			() => { });

		action.connect();
	}

	addClassroomClicked(building: Building): void {
		const modalRef = this.modalService.open(ClassroomModalComponent);
		const modal = modalRef.componentInstance as ClassroomModalComponent;

		modal.classroom.buildingId = building.id;

		modalRef.result.then(
			(classroom: Classroom) => {
				this.classrooms.get(building.id).push(classroom);
				this.classrooms.get(building.id).sort(
					(c1, c2) => c1.name.localeCompare(c2.name));
			},
			() => { });
	}

	editClassroomClicked(classroom: Classroom): void {
		const modalRef = this.modalService.open(ClassroomModalComponent);
		const modal = modalRef.componentInstance as ClassroomModalComponent;

		modal.isEditing = true;

		modal.classroom = {
			id: classroom.id,
			buildingId: classroom.buildingId,
			name: classroom.name
		};

		modalRef.result.then(
			(updatedClassroom: Classroom) =>
				classroom.name = updatedClassroom.name,
			() => { });
	}

	deleteClassroomClicked(classroom: Classroom): void {
		const action = this.classroomService.deleteClassroom(classroom.id);
		action.subscribe(
			() => this.classrooms.set(
				classroom.buildingId,
				this.classrooms.get(classroom.buildingId).filter(
					c => c.id !== classroom.id)),
			() => { });

		action.connect();
	}
	
	addSubjectClicked(): void {
		const modalRef = this.modalService.open(SubjectModalComponent);

		modalRef.result.then(
			(subject: Subject) => {
				this.subjects.push(subject);
				this.subjects.sort(
					(s1, s2) => s1.name.localeCompare(s2.name));
			},
			() => { });
	}

	editSubjectClicked(subject: Subject): void {
		const modalRef = this.modalService.open(SubjectModalComponent);
		const modal = modalRef.componentInstance as SubjectModalComponent;

		modal.isEditing = true;
		modal.subject = {
			id: subject.id,
			name: subject.name
		};

		modalRef.result.then(
			(updatedSubject: Subject) => 
				subject.name = updatedSubject.name,
			() => { });
	}

	deleteSubjectClicked(id: number): void {
		const action = this.subjectService.deleteSubject(id);
		action.subscribe(
			() => this.subjects = this.subjects.filter(s => s.id !== id),
			() => { });

		action.connect();
	}
}
