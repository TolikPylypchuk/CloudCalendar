import { Component, Input, OnInit } from "@angular/core";
import { Http } from "@angular/http";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Student, Lecturer, Class } from "../../common/models";

@Component({
	selector: "student-modal-content",
	templateUrl: "app/student/calendar/modal-content.component.html"
})
export default class ModalContentComponent implements OnInit {
	activeModal: NgbActiveModal;
	http: Http;

	classId: number;

	currentStudent: Student;
	currentLecturers: Lecturer[];
	currentClass: Class;

	constructor(activeModal: NgbActiveModal, http: Http) {
		this.activeModal = activeModal;
		this.http = http;
	}

	ngOnInit(): void {
		const classRequest = this.http.get(`/api/classes/id/${this.classId}`);

		classRequest.map(response => response.json())
			.subscribe(data => {
				this.currentClass = data as Class;
			});

		const lecturersRequest = this.http.get(
			`/api/lecturers/classId/${this.classId}`);

		lecturersRequest.map(response => response.json())
			.subscribe(data => {
				this.currentLecturers = data as Lecturer[];
			});
	}
}
