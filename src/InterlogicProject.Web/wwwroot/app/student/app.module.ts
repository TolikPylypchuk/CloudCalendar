import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CalendarModule } from "./calendar/calendar";
import { CommonModule } from "./common/common";

import AppComponent from "./app.component";

@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		CommonModule,
		NgbModule.forRoot(),
		CalendarModule
	],
	bootstrap: [
		AppComponent
	]
})
export default class AppModule { }
