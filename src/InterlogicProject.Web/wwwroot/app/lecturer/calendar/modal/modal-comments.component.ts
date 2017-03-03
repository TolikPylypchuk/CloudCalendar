﻿import { Component, Input, OnInit } from "@angular/core";
import { Http, Headers } from "@angular/http";
import { Observable } from "rxjs";
import * as moment from "moment";

import { Student, Class, Comment } from "../../../common/models";

import { LecturerService, ClassService } from "../../common/common";

@Component({
	selector: "lecturer-modal-comments",
	templateUrl: "app/lecturer/calendar/modal/modal-comments.component.html",
	styleUrls: [ "app/lecturer/calendar/modal/modal-comments.component.css" ]
})
export default class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	currentComment: Comment = {
		text: ""
	};

	editedCommentId = 0;
	editedCommentOriginalText = "";

	private http: Http;
	private lecturerService: LecturerService;
	private classService: ClassService;

	constructor(
		http: Http,
		lecturerService: LecturerService,
		classService: ClassService) {
		this.http = http;
		this.lecturerService = lecturerService;
		this.classService = classService;
	}

	ngOnInit(): void {
		this.lecturerService.getCurrentUser()
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

		this.classService.getComments(this.classId)
			.subscribe(data => this.comments = data);
	}

	formatDateTime(dateTime: string, format: string): string {
		return moment.utc(dateTime).format(format);
	}

	getCommentId(index: number, comment: Comment): number {
		return comment.id;
	}

	addComment(): void {
		this.currentComment.dateTime = moment().utc()
			.add(2, "hours").toISOString();

		this.http.post(
			"api/comments",
			JSON.stringify(this.currentComment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.subscribe(response => {
				if (response.status === 201) {
					this.comments.push(response.json() as Comment);

					this.currentComment = {
						userId: this.currentComment.userId,
						userFirstName: this.currentComment.userFirstName,
						userMiddleName: this.currentComment.userMiddleName,
						userLastName: this.currentComment.userLastName,
						userFullName: this.currentComment.userFullName,
						classId: this.currentComment.classId,
						text: ""
					};
				}
			});
	}

	editComment(comment: Comment): void {
		this.editedCommentId = comment.id;
		this.editedCommentOriginalText = comment.text;
	}

	updateComment(comment: Comment): void {
		this.http.put(
			`api/comments/${comment.id}`,
			JSON.stringify(comment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.subscribe(response => {
				if (response.status === 204) {
					this.editedCommentId = 0;
				}
			});
	}

	cancelEditing(comment: Comment): void {
		comment.text = this.editedCommentOriginalText;

		this.editedCommentId = 0;
		this.editedCommentOriginalText = "";
	}

	deleteComment(comment: Comment): void {
		this.http.delete(
			`api/comments/${comment.id}`)
			.subscribe(response => {
				if (response.status === 204) {
					this.comments = this.comments.filter(
						c => c.id !== comment.id);
				}
			});
	}
}