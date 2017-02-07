import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModalModule } from "@ng-bootstrap/ng-bootstrap";

import { CalendarComponent as ImportedCalendarComponent } from
	"angular2-fullcalendar/src/calendar/calendar";

import CalendarComponent from "./calendar.component";
import ModalContentComponent from "./modal-content.component";

@NgModule({
	declarations: [
		ImportedCalendarComponent,
		CalendarComponent,
		ModalContentComponent
	],
	entryComponents: [
		ModalContentComponent
	],
	imports: [
		HttpModule,
		BrowserModule,
		NgbModalModule.forRoot()
	],
	exports: [
		CalendarComponent,
		ModalContentComponent
	]
})
class CalendarModule { }

export {
	CalendarComponent,
	ModalContentComponent,

	CalendarModule
}
