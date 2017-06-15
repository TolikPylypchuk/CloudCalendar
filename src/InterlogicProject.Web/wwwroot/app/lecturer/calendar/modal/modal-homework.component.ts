﻿import { Component, Input, OnInit } from "@angular/core";

import {
	ClassService, HomeworkService, LecturerService, StudentService
} from "../../../common/common";
import { Class, Homework, Student } from "../../../common/models";

@Component({
	selector: "ip-lecturer-modal-homework",
	templateUrl: "/templates/lecturer/calendarModalHomework",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class ModalHomeworkComponent implements OnInit {
	@Input() classId: number;

	currentClass: Class;
	homeworks: Homework[] = [];
	students = new Map<number, Student>();

	text: string;

	private classService: ClassService;
	private homeworkService: HomeworkService;
	private lecturerService: LecturerService;
	private studentService; StudentService;

	private allowText = "Дозволити надсилання домашніх завдань";
	private forbidText = "Обмежити надсилання домашніх завдань";

	constructor(
		classService: ClassService,
		homeworkService: HomeworkService,
		lecturerService: LecturerService,
		studentService: StudentService) {
		this.classService = classService;
		this.homeworkService = homeworkService;
		this.lecturerService = lecturerService;
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

		this.classService.updateClass(this.currentClass)
			.subscribe(response => {
				if (response.status === 204) {
					this.text = this.currentClass.homeworkEnabled
						? this.forbidText
						: this.allowText;
				}
			});
	}

	acceptHomework(homework: Homework, accepted: boolean): void {
		const h: Homework = {
			id: homework.id,
			accepted: accepted
		};

		this.homeworkService.updateHomework(h)
			.subscribe(response => {
				if (response.status === 204) {
					homework.accepted = accepted;
				}
			});
	}

	deleteHomework(homework: Homework): void {
		this.homeworkService.deleteHomework(homework.id)
			.subscribe(response => {
				if (response.status === 204) {
					this.homeworks = this.homeworks.filter(
						h => h.id !== homework.id);
				}
			});
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
}
