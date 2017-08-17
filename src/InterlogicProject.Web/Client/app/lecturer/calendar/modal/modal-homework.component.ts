import { Component, Input, OnInit } from "@angular/core";

import * as moment from "moment";

import {
	ClassService, HomeworkService, LecturerService,
	NotificationService, StudentService
} from "../../../common/common";
import { Class, Homework, Lecturer, Student } from "../../../common/models";

@Component({
	selector: "ip-lecturer-modal-homework",
	templateUrl: "./modal-homework.component.html"
})
export class ModalHomeworkComponent implements OnInit {
	@Input() classId: number;

	currentClass: Class;
	currentLecturer: Lecturer;
	homeworks: Homework[] = [];
	students = new Map<number, Student>();

	text: string;

	private classService: ClassService;
	private homeworkService: HomeworkService;
	private lecturerService: LecturerService;
	private notificationService: NotificationService;
	private studentService: StudentService;

	private allowText = "Дозволити надсилання домашніх завдань";
	private forbidText = "Обмежити надсилання домашніх завдань";

	constructor(
		classService: ClassService,
		homeworkService: HomeworkService,
		lecturerService: LecturerService,
		notificationService: NotificationService,
		studentService: StudentService) {
		this.classService = classService;
		this.homeworkService = homeworkService;
		this.lecturerService = lecturerService;
		this.notificationService = notificationService;
		this.studentService = studentService;
	}

	ngOnInit(): void {
		this.classService.getClass(this.classId)
			.subscribe(c => {
				this.currentClass = c;

				this.text = this.currentClass.homeworkEnabled
					? this.forbidText
					: this.allowText;
			});

		this.lecturerService.getCurrentLecturer()
			.subscribe(lecturer => this.currentLecturer = lecturer);

		this.homeworkService.getHomeworksByClass(this.classId)
			.subscribe(homeworks => {
				this.homeworks = homeworks;

				for (let homework of homeworks) {
					this.studentService.getStudent(homework.studentId)
						.subscribe(student =>
							this.students.set(student.id, student));
				}
			});
	}

	homeworkEnabledClick(): void {
		this.currentClass.homeworkEnabled = !this.currentClass.homeworkEnabled;

		const action = this.classService.updateClass(this.currentClass);

		action.subscribe(response => {
			if (response.status === 204) {
				this.text = this.currentClass.homeworkEnabled
					? this.forbidText
					: this.allowText;

				this.notificationService.addNotificationForGroupsInClass(
					{
						dateTime: moment().toISOString(),
						text: this.getHomeworkNotificationText(
							this.currentClass.homeworkEnabled)
					},
					this.classId)
					.connect();
			}
		});

		action.connect();
	}

	acceptHomework(homework: Homework, accepted: boolean): void {
		const h: Homework = {
			id: homework.id,
			accepted: accepted
		};

		const action = this.homeworkService.updateHomework(h);

		action.subscribe(response => {
			if (response.status === 204) {
				homework.accepted = accepted;
			}
		});

		action.connect();
	}

	deleteHomework(homework: Homework): void {
		const action = this.homeworkService.deleteHomework(homework.id);

		action.subscribe(response => {
			if (response.status === 204) {
				this.homeworks = this.homeworks.filter(
					h => h.id !== homework.id);
			}
		});

		action.connect();
	}

	getStudentName(id: number): string {
		const student = this.students.get(id);
		return student
			? `${student.lastName} ${student.firstName}`
			: "";
	}

	getAcceptedText(accepted: boolean): string {
		return accepted === null
			? "Не перевірено"
			: accepted
				? "Прийнято"
				: "Відхилено";
	}

	getAcceptedClass(accepted: boolean): string {
		return accepted === null
			? "text-primary float-right"
			: accepted
				? "text-success float-right"
				: "text-danger float-right";
	}

	private getHomeworkNotificationText(enabled: boolean): string {
		return `${this.currentLecturer.firstName} ` +
			`${this.currentLecturer.lastName} ${enabled ? "увімкнув" : "вимкнув"} ` +
			`додавання домашнього завдання до пари '${this.currentClass.subjectName}' ` +
			`${moment(this.currentClass.dateTime).format("DD.MM.YYYY")}.`;
	}

	private getCheckNotificationText(approved: boolean | null): string {
		const classInfo = `'${this.currentClass.subjectName}' ` +
			`${moment(this.currentClass.dateTime).format("DD.MM.YYYY")}`;

		return approved === null
			? `Результат перевірки вашого завдання до пари ${classInfo} скасовано.`
			: `Ваше домашнє завдання до пари ${classInfo}` +
				`${approved ? "прийнято" : "відхилено"}.`;
	}
}
