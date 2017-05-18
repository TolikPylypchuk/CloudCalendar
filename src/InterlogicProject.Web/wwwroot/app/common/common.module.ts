import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";

import { FileSelectDirective, FileDropDirective } from "ng2-file-upload";

import FullcalendarComponent from "./components/fullcalendar.component";

import ClassService from "./services/class.service";
import LecturerService from "./services/lecturer.service";
import StudentService from "./services/student.service";

@NgModule({
	declarations: [
		FileSelectDirective,
		FileDropDirective,

		FullcalendarComponent
	],
	providers: [
		LecturerService,
		ClassService,
		StudentService
	],
	imports: [
		HttpModule
	]
})
export default class CommonModule { }
