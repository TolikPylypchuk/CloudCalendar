import { Component, Input, OnInit } from "@angular/core";
import { Http } from "@angular/http";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { Observable } from "rxjs/Observable";

import * as moment from "moment";

import {
	Student, Lecturer, Class, Classroom, Group
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
	dateTime = "Завантаження...";
	classrooms: Classroom[];
	lecturers: Lecturer[];

	private activeModal: NgbActiveModal;
	private http: Http;

	private classService: ClassService;
	
	constructor(
		activeModal: NgbActiveModal,
		http: Http,
		classService: ClassService) {
		this.activeModal = activeModal;
		this.http = http;
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
		return moment.utc(dateTime).format("DD.MM.YYYY, dddd, HH:mm");
	}

	formatClassrooms(classrooms: Classroom[]): string {
		return classrooms
			? classrooms.reduce((a, c) => `${a}, ${c.name}`, "").substring(2)
			: "";
	}

	formatLecturers(lecturers: Lecturer[]): string {
		return lecturers
			? lecturers.reduce(
				(a, l) => `${a}, ${l.userLastName} ${l.userFirstName[0]}. ` +
					`${l.userMiddleName[0]}.`,
				"").substring(2)
			: "";
	}
}
