import { Component, Input, OnInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import * as moment from "moment";

import { ClassService, ClassroomService, GroupService } from "../../../common/common";
import { Classroom, Group } from "../../../common/models";

@Component({
	selector: "ip-lecturer-modal-content",
	templateUrl: "/templates/lecturer/calendar/modal-content",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export class ModalContentComponent implements OnInit {
	@Input() classId: number;

	subjectName = "Завантаження...";
	type = "Завантаження...";
	dateTime = "";
	classrooms: Classroom[];
	groups: Group[];

	private activeModal: NgbActiveModal;

	private classService: ClassService;
	private classroomService: ClassroomService;
	private groupService: GroupService;

	constructor(
		activeModal: NgbActiveModal,
		classService: ClassService,
		classroomService: ClassroomService,
		groupService: GroupService) {
		this.activeModal = activeModal;
		this.classService = classService;
		this.classroomService = classroomService;
		this.groupService = groupService;
	}

	ngOnInit() {
		this.classService.getClass(this.classId)
			.subscribe(data => {
				this.subjectName = data.subjectName;
				this.type = data.type;
				this.dateTime = data.dateTime;
			});

		this.classroomService.getClassroomsByClass(this.classId)
			.subscribe(data => this.classrooms = data);

		this.groupService.getGroupsByClass(this.classId)
			.subscribe(data => this.groups = data);
	}

	formatDateTime(dateTime: string): string {
		return dateTime === ""
			? "Завантаження..."
			: moment.utc(dateTime).format("DD.MM.YYYY, dddd, HH:mm");
	}

	formatClassrooms(classrooms: Classroom[]): string {
		return classrooms
			? classrooms.reduce((a, c) => `${a}, ${c.name}`, "").substring(2)
			: "";
	}

	formatGroups(groups: Group[]): string {
		return groups
			? groups.reduce((a, g) => `${a}, ${g.name}`, "").substring(2)
			: "";
	}
}
