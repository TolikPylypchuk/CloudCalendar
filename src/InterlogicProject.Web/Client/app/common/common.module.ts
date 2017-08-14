import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";

import { FileSelectDirective, FileDropDirective } from "ng2-file-upload";

import { FullcalendarComponent } from "./components/fullcalendar.component";

import { BuildingService } from "./services/building.service";
import { ClassService } from "./services/class.service";
import { ClassroomService } from "./services/classroom.service";
import { CommentService } from "./services/comment.service";
import { DepartmentService } from "./services/department.service";
import { FacultyService } from "./services/faculty.service";
import { GroupService } from "./services/group.service";
import { HomeworkService } from "./services/homework.service";
import { LecturerService } from "./services/lecturer.service";
import { MaterialService } from "./services/material.service";
import { NotificationService } from "./services/notification.service";
import { StudentService } from "./services/student.service";
import { SubjectService } from "./services/subject.service";

@NgModule({
	declarations: [
		FileSelectDirective,
		FileDropDirective,

		FullcalendarComponent
	],
	providers: [
		BuildingService,
		ClassService,
		ClassroomService,
		CommentService,
		DepartmentService,
		FacultyService,
		GroupService,
		HomeworkService,
		LecturerService,
		MaterialService,
		NotificationService,
		StudentService,
		SubjectService
	],
	imports: [
		HttpModule
	],
	exports: [
		FileSelectDirective,
		FileDropDirective,

		FullcalendarComponent
	]
})
export class CommonModule { }
