import { Component, Input, OnInit } from "@angular/core"

import { FileUploader, FileItem } from "ng2-file-upload";

import { ClassService, StudentService } from "../../common/common";

import { Class, Homework } from "../../../common/models";

@Component({
	selector: "student-modal-homework",
	templateUrl: "app/student/calendar/modal/modal-homework.component.html",
	styleUrls: [ "app/student/calendar/modal/modal-homework.component.css" ]
})
export default class ModalHomeworkComponent implements OnInit {
	@Input() classId: number;

	currentClass: Class;
	currentStudentId: number;
	homework: Homework;

	uploader: FileUploader;

	private classService: ClassService;
	private studentService: StudentService;

	constructor(classService: ClassService, studentService: StudentService) {
		this.classService = classService;
		this.studentService = studentService;
	}

	ngOnInit(): void {
		this.studentService.getCurrentStudent()
			.subscribe(student => {
				this.currentStudentId = student.id;

				this.uploader = new FileUploader(
				{
					url: `api/homeworks/classId/${this.classId}` +
							`/studentId/${student.id}`
				});
				
				this.uploader.onCompleteItem = (item: FileItem) => {
					this.uploader.queue = [];

					this.classService.getHomework(this.classId, this.currentStudentId)
						.subscribe(homework => this.homework = homework);
				};

				this.classService.getHomework(this.classId, this.currentStudentId)
					.subscribe(homework => this.homework = homework);
			});

		this.classService.getClass(this.classId)
			.subscribe(c => this.currentClass = c);
	}
	
	deleteHomework(): void {
		this.classService.deleteHomework(this.homework.id)
			.subscribe(response => {
				if (response.status === 204) {
					this.homework = null;
				}
			});
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
				: "Не прийнято";
	}
}
