import { NgModule } from "@angular/core";

import { CalendarModule } from "./calendar/calendar";
import { CommonModule } from "./common/common";

import AppComponent from "./app.component";

@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		CommonModule,
		CalendarModule
	],
	bootstrap: [
		AppComponent
	]
})
export default class AppModule { }
