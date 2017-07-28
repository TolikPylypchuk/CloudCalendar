import { Component, Input, OnInit } from "@angular/core"
import { FileUploader, FileItem } from "ng2-file-upload";

import * as moment from "moment";

import {
	ClassService, HomeworkService, NotificationService, StudentService
} from "../../../common/common";
import { getAuthToken } from "../../../common/functions";
import { Class, Homework, Student } from "../../../common/models";

@Component({
	selector: "ip-student-modal-homework",
	templateUrl: "/templates/student/calendar/modal-homework",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class ModalHomeworkComponent implements OnInit {
	@Input() classId: number;

	currentClass: Class;
	currentStudentId: number;
	homework: Homework;

	uploader: FileUploader;

	private classService: ClassService;
	private homeworkService: HomeworkService;
	private notificationService: NotificationService;
	private studentService: StudentService;

	constructor(
		classService: ClassService,
		homeworkService: HomeworkService,
		notificationService: NotificationService,
		studentService: StudentService) {
		this.classService = classService;
		this.homeworkService = homeworkService;
		this.notificationService = notificationService;
		this.studentService = studentService;
	}

	ngOnInit(): void {
		this.studentService.getCurrentStudent()
			.subscribe(student => {
				this.currentStudentId = student.id;

				this.uploader = new FileUploader(
					{
						url: `/api/homeworks/classId/${this.classId}` +
							`/studentId/${student.id}`,
						authToken: `Bearer ${getAuthToken()}`,
						removeAfterUpload: true
					});

				this.uploader.onCompleteItem = (item: FileItem) => {
					this.homeworkService.getHomeworkByClassAndStudent(
						this.classId, this.currentStudentId)
						.subscribe(homework => this.homework = homework);
				};

				this.uploader.onCompleteAll = () => {
					this.notificationService.addNotificationForLecturersInClass(
						{
							dateTime: moment().toISOString(),
							text: this.getNotificationText(student),
							userId: student.userId
						},
						this.currentClass.id)
						.connect();
				};

				this.homeworkService.getHomeworkByClassAndStudent(
					this.classId, this.currentStudentId)
					.subscribe(homework => this.homework = homework);
			});

		this.classService.getClass(this.classId)
			.subscribe(c => this.currentClass = c);
	}
	
	deleteHomework(): void {
		const action = this.homeworkService.deleteHomework(this.homework.id);

		action.subscribe(response => {
			if (response.status === 204) {
				this.homework = null;
			}
		});

		action.connect();
	}

	getCheckClass(): string {
		return this.homework.accepted === null
			? "fa fa-circle-o text-primary"
			: this.homework.accepted
				? "fa fa-check text-success"
				: "fa fa-ban text-danger";
	}

	getCheckTooltip(): string {
		return this.homework.accepted === null
			? "Не перевірено"
			: this.homework.accepted
				? "Прийнято"
				: "Відхилено";
	}

	private getNotificationText(student: Student): string {
		return `${student.firstName} ${student.lastName} додав домашню роботу ` +
			`до пари '${this.currentClass.subjectName}' ` +
			`${moment(this.currentClass.dateTime).format("DD.MM.YYYY")}.`;
	}
}
