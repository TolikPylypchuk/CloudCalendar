import { Component, Input, OnInit } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs";

import { Student, Class, Comment } from "../../common/models";

@Component({
	selector: "student-modal-comments",
	templateUrl: "app/student/calendar/modal-comments.component.html"
})
export default class ModalCommentsComponent implements OnInit {
	@Input() currentStudent: Student;
	@Input() currentClass: Class;

	comments: Comment[] = [];

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	ngOnInit(): void {
		const request = this.http.get(
			`/api/comments/classId/${this.currentClass.id}`);

		request.map(response => response.json())
			.subscribe(data => {
				this.comments = data as Comment[];
			});
	}
}
