import { NgModule } from "@angular/core";

import { CalendarModule } from "./calendar/calendar";
import RoutesModule from "./routes.module";

import StudentComponent from "./student.component";

@NgModule({
	declarations: [
		StudentComponent
	],
	imports: [
		CalendarModule,
		RoutesModule
	],
	exports: [
		CalendarModule,
		StudentComponent
	]
})
export default class StudentModule { }
