import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs/Observable";

import { Faculty, Group } from "../../common/models";
import { FacultyService, GroupService } from "../../common/common";

@Component({
	selector: "ip-view-groups",
	templateUrl: "/templates/view/groups"
})
export default class GroupsComponent implements OnInit {
	private facultyService: FacultyService;
	private groupService: GroupService;

	private faculties: Faculty[] = [];
	private groups = new Map<number, Group[]>();

	constructor(
		facultyService: FacultyService,
		groupService: GroupService) {
		this.facultyService = facultyService;
		this.groupService = groupService;
	}

	ngOnInit(): void {
		this.facultyService.getFaculties()
			.subscribe(faculties => {
				this.faculties = faculties.sort(this.compareByName);

				for (const faculty of faculties) {
					this.groupService.getGroupsByFaculty(faculty.id)
						.subscribe(groups =>
							this.groups.set(
								faculty.id,
								groups.sort(this.compareByName)));
				}
			});
	}

	getFaculties(): Faculty[] {
		return this.faculties;
	}

	getGroups(facultyId: number): Group[] {
		return this.groups.get(facultyId);
	}

	private compareByName(a: { name: string }, b: { name: string }): number {
		return a.name.localeCompare(b.name);
	}
}
