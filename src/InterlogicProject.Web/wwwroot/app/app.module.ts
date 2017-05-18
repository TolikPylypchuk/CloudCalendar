import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import {
	CalendarModule as LecturerCalendarModule
} from "./lecturer/calendar/calendar";
 
import {
	CalendarModule as StudentCalendarModule
} from "./student/calendar/calendar";

import AppComponent from "./app.component";

@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		NgbModule.forRoot(),
		LecturerCalendarModule,
		StudentCalendarModule
	],
	bootstrap: [
		AppComponent
	]
})
export default class AppModule { }
