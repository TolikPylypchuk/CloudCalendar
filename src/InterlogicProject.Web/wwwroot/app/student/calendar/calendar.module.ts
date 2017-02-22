import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { NgbModalModule } from "@ng-bootstrap/ng-bootstrap";

import { CalendarComponent as FullcalendarComponent } from
	"angular2-fullcalendar/src/calendar/calendar";

import { CommonModule } from "../common/common";

import CalendarComponent from "./calendar.component";
import ModalContentComponent from "./modal/modal-content.component";
import ModalCommentsComponent from "./modal/modal-comments.component";

@NgModule({
	declarations: [
		FullcalendarComponent,
		CalendarComponent,
		ModalContentComponent,
		ModalCommentsComponent
	],
	entryComponents: [
		ModalContentComponent
	],
	imports: [
		BrowserModule,
		HttpModule,
		FormsModule,
		NgbModalModule,
		CommonModule
	],
	exports: [
		CalendarComponent,
		ModalContentComponent,
		ModalCommentsComponent
	]
})
export default class CalendarModule { }
