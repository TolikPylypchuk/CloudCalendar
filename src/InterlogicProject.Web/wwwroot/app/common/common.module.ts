import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";

import { FileSelectDirective, FileDropDirective } from "ng2-file-upload";

import FullcalendarComponent from "./components/fullcalendar.component";

import BuildingService from "./services/building.service";
import ClassService from "./services/class.service";
import ClassroomService from "./services/classroom.service";
import CommentService from "./services/comment.service";
import DepartmentService from "./services/department.service";
import FacultyService from "./services/faculty.service";
import LecturerService from "./services/lecturer.service";
import StudentService from "./services/student.service";

@NgModule({
	declarations: [
		FileSelectDirective,
		FileDropDirective,

		FullcalendarComponent
	],
	providers: [
		BuildingService,
		LecturerService,
		ClassService,
		ClassroomService,
		CommentService,
		DepartmentService,
		FacultyService,
		StudentService
	],
	imports: [
		HttpModule
	]
})
export default class CommonModule { }
