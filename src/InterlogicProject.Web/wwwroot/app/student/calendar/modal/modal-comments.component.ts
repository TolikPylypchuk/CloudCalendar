import { Component, Input, OnInit } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs";

import { Student, Class, Comment } from "../../../common/models";

import { ClassService } from "../../common/common";

@Component({
	selector: "student-modal-comments",
	templateUrl: "app/student/calendar/modal-comments.component.html"
})
export default class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	private http: Http;
	private classService: ClassService;

	constructor(http: Http, classService: ClassService) {
		this.http = http;
		this.classService = classService;
	}

	ngOnInit(): void {
		this.classService.getComments(this.classId)
			.subscribe(data => this.comments = data);
	}
}
