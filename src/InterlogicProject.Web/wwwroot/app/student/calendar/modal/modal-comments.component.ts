import { Component, Input, OnInit } from "@angular/core";
import { Http, Headers } from "@angular/http";
import { Observable } from "rxjs";
import * as moment from "moment";

import { Student, Class, Comment } from "../../../common/models";

import { StudentService, ClassService } from "../../common/common";

@Component({
	selector: "student-modal-comments",
	templateUrl: "app/student/calendar/modal/modal-comments.component.html"
})
export default class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	currentComment: Comment = {};

	private http: Http;
	private studentService: StudentService;
	private classService: ClassService;

	constructor(
		http: Http,
		studentService: StudentService,
		classService: ClassService) {
		this.http = http;
		this.studentService = studentService;
		this.classService = classService;
	}

	ngOnInit(): void {
		this.classService.getComments(this.classId)
			.subscribe(data => this.comments = data);

		this.studentService.getCurrentUser()
			.subscribe(user => {
				if (user) {
					this.currentComment.userId = user.id;
					this.currentComment.userFirstName = user.firstName;
					this.currentComment.userMiddleName = user.middleName;
					this.currentComment.userLastName = user.lastName;
					this.currentComment.userFullName = user.fullName;
					this.currentComment.classId = this.classId;
				}
			});
	}

	formatDateTime(dateTime: string, format: string): string {
		return moment.utc(dateTime).format(format);
	}

	addComment(): void {
		this.currentComment.dateTime = moment().utc().add(2, "hours").toISOString();

		this.http.post(
			"api/comments",
			JSON.stringify(this.currentComment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.subscribe(response => {
				this.comments.push(response.json() as Comment);
				
				this.currentComment = {
					userId: this.currentComment.userId,
					userFirstName: this.currentComment.userFirstName,
					userMiddleName: this.currentComment.userMiddleName,
					userLastName: this.currentComment.userLastName,
					userFullName: this.currentComment.userFullName,
					classId: this.currentComment.classId
				};
			});
	}
}
