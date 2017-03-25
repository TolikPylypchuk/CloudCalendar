import { Component, Input, OnInit } from "@angular/core";

import { ClassService } from "../../common/common";

import { Class, Homework } from "../../../common/models";

@Component({
	selector: "lecturer-modal-homework",
	templateUrl: "app/lecturer/calendar/modal/modal-homework.component.html",
	styleUrls: [ "app/lecturer/calendar/modal/modal-homework.component.css" ]
})
export default class ModalHomeworkComponent implements OnInit {
	@Input() classId: number;

	currentClass: Class;
	homeworks: Homework[] = [];

	classService: ClassService;

	constructor(classService: ClassService) {
		this.classService = classService;
	}

	ngOnInit(): void {
		this.classService.getClass(this.classId)
			.subscribe(c => this.currentClass = c);

		this.classService.getHomeworks(this.classId)
			.subscribe(homeworks => this.homeworks = homeworks);
	}
}
