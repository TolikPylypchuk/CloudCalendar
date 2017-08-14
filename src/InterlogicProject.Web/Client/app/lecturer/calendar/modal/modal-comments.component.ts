import { Component, Input, OnInit } from "@angular/core";

import * as moment from "moment";

import { AccountService } from "../../../account/account";
import {
	ClassService, CommentService, LecturerService, NotificationService
} from "../../../common/common";
import { Class, Comment, Lecturer, Notification } from "../../../common/models";

@Component({
	selector: "ip-lecturer-modal-comments",
	templateUrl: "./modal-comments.component.html"
})
export class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	currentClass: Class;
	currentLecturer: Lecturer;
	currentComment: Comment = {
		text: ""
	};

	editedCommentId = 0;
	editedCommentOriginalText = "";

	private classService: ClassService;
	private commentService: CommentService;
	private lecturerService: LecturerService;
	private notificationService: NotificationService;

	constructor(
		classService: ClassService,
		commentService: CommentService,
		lecturerService: LecturerService,
		notificationService: NotificationService) {
		this.classService = classService;
		this.commentService = commentService;
		this.lecturerService = lecturerService;
		this.notificationService = notificationService;
	}

	ngOnInit(): void {
		this.lecturerService.getCurrentLecturer()
			.subscribe(lecturer => {
				this.currentComment.userId = lecturer.userId;
				this.currentComment.userFirstName = lecturer.firstName;
				this.currentComment.userMiddleName = lecturer.middleName;
				this.currentComment.userLastName = lecturer.lastName;
				this.currentComment.userFullName = lecturer.fullName;
				this.currentComment.classId = this.classId;
				this.currentLecturer = lecturer;
			});

		this.commentService.getCommentsByClass(this.classId)
			.subscribe(data => this.comments = data);

		this.classService.getClass(this.classId)
			.subscribe(c => this.currentClass = c);
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
					classId: this.currentComment.classId,
					text: ""
				};

				const notification: Notification = {
					dateTime: moment().toISOString(),
					text: this.getNotificationText(),
					userId: this.currentLecturer.userId,
					classId: this.classId
				};

				const action =
					this.notificationService.addNotificationForGroupsInClass(
						notification, this.classId);

				action.subscribe(() =>
					this.notificationService.addNotificationForLecturersInClass(
						notification, this.classId)
						.connect());

				action.connect();
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

	private getNotificationText(): string {
		return `${this.currentLecturer.firstName} ${this.currentLecturer.lastName} ` +
			`додав коментар до пари '${this.currentClass.subjectName}' ` +
			`${moment(this.currentClass.dateTime).format("DD.MM.YYYY")}.`;
	}
}
