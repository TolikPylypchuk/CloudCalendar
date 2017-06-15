import { Component, Input, OnInit } from "@angular/core";

import * as moment from "moment";

import { AccountService } from "../../../account/account";
import { CommentService } from "../../../common/common";
import { Comment } from "../../../common/models";

@Component({
	selector: "ip-student-modal-comments",
	templateUrl: "/templates/student/calendar/modal-comments",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	currentComment: Comment = {
		text: ""
	};

	editedCommentId = 0;
	editedCommentOriginalText = "";

	private accountService: AccountService;
	private commentService: CommentService;

	constructor(
		accountService: AccountService,
		commentService: CommentService) {
		this.accountService = accountService;
		this.commentService = commentService;
	}

	ngOnInit(): void {
		this.accountService.getCurrentUser()
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

		this.commentService.getCommentsByClass(this.classId)
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

		const action = this.commentService.addComment(this.currentComment);

		action.subscribe(response => {
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

		action.connect();
	}

	editComment(comment: Comment): void {
		this.editedCommentId = comment.id;
		this.editedCommentOriginalText = comment.text;
	}

	updateComment(comment: Comment): void {
		const action = this.commentService.updateComment(comment);

		action.subscribe(response => {
			if (response.status === 204) {
				this.editedCommentId = 0;
			}
		});

		action.connect();
	}

	cancelEditing(comment: Comment): void {
		comment.text = this.editedCommentOriginalText;

		this.editedCommentId = 0;
		this.editedCommentOriginalText = "";
	}

	deleteComment(comment: Comment): void {
		const action = this.commentService.deleteComment(comment.id);

		action.subscribe(response => {
			if (response.status === 204) {
				this.comments = this.comments.filter(
					c => c.id !== comment.id);
			}
		});

		action.connect();
	}
}
