import { Component, Input, OnInit } from "@angular/core";

import * as moment from "moment";

import { StudentService, ClassService } from "../../../common/common";
import { Comment } from "../../../common/models";

@Component({
	selector: "student-modal-comments",
	templateUrl: "app/student/calendar/modal/modal-comments.component.html"
})
export default class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	currentComment: Comment = {
		text: ""
	};

	editedCommentId = 0;
	editedCommentOriginalText = "";

	private studentService: StudentService;
	private classService: ClassService;

	constructor(
		studentService: StudentService,
		classService: ClassService) {
		this.studentService = studentService;
		this.classService = classService;
	}

	ngOnInit(): void {
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

		this.classService.addComment(this.currentComment)
			.subscribe(response => {
				if (response.status === 201) {
					this.comments.push(response.json() as Comment);

					this.currentComment = {
						userId: this.currentComment.userId,
						userFirstName: this.currentComment.userFirstName,
						userMiddleName: this.currentComment.userMiddleName,
						userLastName: this.currentComment.userLastName,
						userFullName: this.currentComment.userFullName,
						userEmail: this.currentComment.userEmail,
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
		this.classService.updateComment(comment)
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
		this.classService.deleteComment(comment.id)
			.subscribe(response => {
				if (response.status === 204) {
					this.comments = this.comments.filter(
						c => c.id !== comment.id);
				}
			});
	}
}
