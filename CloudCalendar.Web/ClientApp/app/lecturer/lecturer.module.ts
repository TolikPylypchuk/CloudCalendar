import { NgModule } from "@angular/core";

import { LecturerComponent } from "./lecturer.component";

import { CalendarModule } from "./calendar/calendar";
import { RoutesModule } from "./routes.module";

@NgModule({
	declarations: [
		LecturerComponent
	],
	imports: [
		CalendarModule,
		RoutesModule
	],
	exports: [
		CalendarModule,
		LecturerComponent
	]
})
export class LecturerModule { }
