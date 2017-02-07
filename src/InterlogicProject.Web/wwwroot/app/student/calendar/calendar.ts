import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModalModule } from "@ng-bootstrap/ng-bootstrap";

import { CalendarComponent as ImportedCalendarComponent } from
	"angular2-fullcalendar/src/calendar/calendar";

import CalendarComponent from "./calendar.component";
import ModalContentComponent from "./modal-content.component";
import ModalCommentsComponent from "./modal-comments.component";

@NgModule({
	declarations: [
		ImportedCalendarComponent,
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
		NgbModalModule.forRoot()
	],
	exports: [
		CalendarComponent,
		ModalContentComponent,
		ModalCommentsComponent
	]
})
class CalendarModule { }

export {
	CalendarComponent,
	ModalContentComponent,
	ModalCommentsComponent,

	CalendarModule
}
