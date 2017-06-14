import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "./common/common";
import { AccountModule } from "./account/account";

import { StudentModule, CalendarModule } from "./student/student";

import RoutesModule from "./routes.module";

import {
	CalendarModule as LecturerCalendarModule
} from "./lecturer/calendar/calendar";

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
		StudentModule,
		CalendarModule,
		RoutesModule
	],
	bootstrap: [
		AppComponent
	]
})
export default class AppModule { }
