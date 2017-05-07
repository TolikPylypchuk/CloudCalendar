import { Component, Input, OnInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import * as moment from "moment";

import {
	Lecturer, Classroom
} from "../../../common/models";

import { ClassService } from "../../common/common";

@Component({
	selector: "student-modal-content",
	templateUrl: "app/student/calendar/modal/modal-content.component.html"
})
export default class ModalContentComponent implements OnInit {
	@Input() classId: number;

	subjectName = "Завантаження...";
	type = "Завантаження...";
	dateTime = "";
	classrooms: Classroom[];
	lecturers: Lecturer[];

	private activeModal: NgbActiveModal;

	private classService: ClassService;
	
	constructor(
		activeModal: NgbActiveModal,
		classService: ClassService) {
		this.activeModal = activeModal;
		this.classService = classService;
	}

	ngOnInit() {
		this.classService.getClass(this.classId)
			.subscribe(data => {
				this.subjectName = data.subjectName;
				this.type = data.type;
				this.dateTime = data.dateTime;
			});

		this.classService.getPlaces(this.classId)
			.subscribe(data => this.classrooms = data);
		
		this.classService.getLecturers(this.classId)
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
