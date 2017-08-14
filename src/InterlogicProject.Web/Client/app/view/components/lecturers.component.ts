import { Component, OnInit } from "@angular/core";

import { Faculty, Department, Lecturer } from "../../common/models";
import {
	FacultyService, DepartmentService, LecturerService
} from "../../common/common";
import { compareByName, compareByLastName } from "../../common/functions";

@Component({
	selector: "ip-view-lecturers",
	templateUrl: "./lecturers.component.html"
})
export class LecturersComponent implements OnInit {
	private facultyService: FacultyService;
	private departmentService: DepartmentService;
	private lecturerService: LecturerService;

	private faculties: Faculty[] = [];
	private departments = new Map<number, Department[]>();
	private lecturers = new Map<number, Lecturer[]>();

	constructor(
		facultyService: FacultyService,
		departmentService: DepartmentService,
		lecturerService: LecturerService) {
		this.facultyService = facultyService;
		this.departmentService = departmentService;
		this.lecturerService = lecturerService;
	}

	ngOnInit(): void {
		this.facultyService.getFaculties()
			.subscribe(faculties => {
				this.faculties = faculties.sort(compareByName);

				for (const faculty of faculties) {
					this.departmentService.getDepartmentsByFaculty(faculty.id)
						.subscribe(departments => {
							this.departments.set(
								faculty.id,
								departments.sort(compareByName));

							for (const dep of departments) {
								this.lecturerService.getLecturersByDepartment(
									dep.id)
									.subscribe(lecturers =>
										this.lecturers.set(
											dep.id,
											lecturers.sort(compareByLastName)));
							}
						});
				}
			});
	}

	getFaculties(): Faculty[] {
		return this.faculties;
	}

	getDepartments(facultyId: number): Department[] {
		return this.departments.get(facultyId);
	}

	getLecturers(depId: number): Lecturer[] {
		return this.lecturers.get(depId);
	}
}
