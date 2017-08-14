import { Component, Input, OnInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import * as moment from "moment";

import {
	ClassService, ClassroomService, LecturerService
} from "../../../common/common";
import { Lecturer, Classroom } from "../../../common/models";

@Component({
	selector: "ip-student-modal-content",
	templateUrl: "/templates/student/calendar/modal-content",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export class ModalContentComponent implements OnInit {
	@Input() classId: number;

	subjectName = "Завантаження...";
	type = "Завантаження...";
	dateTime = "";
	classrooms: Classroom[];
	lecturers: Lecturer[];

	private activeModal: NgbActiveModal;

	private classService: ClassService;
	private classroomService: ClassroomService;
	private lecturerService: LecturerService;

	constructor(
		activeModal: NgbActiveModal,
		classService: ClassService,
		classroomService: ClassroomService,
		lecturerService: LecturerService) {
		this.activeModal = activeModal;
		this.classService = classService;
		this.classroomService = classroomService;
		this.lecturerService = lecturerService;
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

		this.lecturerService.getLecturersByClass(this.classId)
			.subscribe(data => this.lecturers = data);
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

	formatLecturers(lecturers: Lecturer[]): string {
		return lecturers
			? lecturers.reduce(
				(a, l) => `${a}, ${l.lastName} ${l.firstName[0]}. ` +
					`${l.middleName[0]}.`,
				"").substring(2)
			: "";
	}
}
