import { Component, Input, OnInit } from "@angular/core";

import * as moment from "moment";

import {
	ClassService, CommentService, NotificationService, StudentService
} from "../../../common/common";
import { Class, Comment, Notification, Student } from "../../../common/models";

@Component({
	selector: "ip-student-modal-comments",
	templateUrl: "/templates/student/calendar/modal-comments",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class ModalCommentsComponent implements OnInit {
	@Input() classId: number;

	comments: Comment[] = [];

	currentStudent: Student;
	currentClass: Class;
	currentComment: Comment = {
		text: ""
	};

	editedCommentId = 0;
	editedCommentOriginalText = "";

	private classService: ClassService;
	private commentService: CommentService;
	private notificationService: NotificationService;
	private studentService: StudentService;

	constructor(
		classService: ClassService,
		commentService: CommentService,
		notificationService: NotificationService,
		studentService: StudentService) {
		this.classService = classService;
		this.commentService = commentService;
		this.notificationService = notificationService;
		this.studentService = studentService;
	}

	ngOnInit(): void {
		this.studentService.getCurrentStudent()
			.subscribe(student => {
				this.currentStudent = student;

				this.currentComment.userId = student.userId;
				this.currentComment.userFirstName = student.firstName;
				this.currentComment.userMiddleName = student.middleName;
				this.currentComment.userLastName = student.lastName;
				this.currentComment.userFullName = student.fullName;
				this.currentComment.classId = this.classId;

				this.classService.getClass(this.classId)
					.subscribe(c => {
						this.currentClass = c;
					});
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

				const notification: Notification = {
					dateTime: moment().toISOString(),
					text: this.getNotificationText(),
					userId: this.currentStudent.userId,
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
			};
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
		return `${this.currentStudent.firstName} ${this.currentStudent.lastName} ` +
			`додав коментар до пари '${this.currentClass.subjectName}' ` +
			`${moment(this.currentClass.dateTime).format("DD.MM.YYYY")}.`;
	}
}
