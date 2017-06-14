import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { AccountModule } from "./account/account";
import { CommonModule } from "./common/common";

import RoutesModule from "./routes.module";

import {
	CalendarModule as LecturerCalendarModule
} from "./lecturer/calendar/calendar";
 
import {
	CalendarModule as StudentCalendarModule
} from "./student/calendar/calendar";

import AppComponent from "./app.component";
import NavigationComponent from "./navigation.component";

@NgModule({
	declarations: [
		AppComponent,
		NavigationComponent
	],
	imports: [
		NgbModule.forRoot(),
		CommonModule,
		AccountModule,
		LecturerCalendarModule,
		StudentCalendarModule,
		RoutesModule
	],
	bootstrap: [
		AppComponent
	]
})
export default class AppModule { }
