import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { NgbModalModule, NgbTooltipModule } from "@ng-bootstrap/ng-bootstrap";

import { CalendarComponent as FullcalendarComponent } from
	"angular2-fullcalendar/src/calendar/calendar";

import { CommonModule } from "../common/common";

import CalendarComponent from "./calendar.component";
import ModalContentComponent from "./modal/modal-content.component";
import ModalCommentsComponent from "./modal/modal-comments.component";
import ModalMaterialsComponent from "./modal/modal-materials.component";

@NgModule({
	declarations: [
		FullcalendarComponent,
		CalendarComponent,
		ModalContentComponent,
		ModalCommentsComponent,
		ModalMaterialsComponent
	],
	entryComponents: [
		ModalContentComponent
	],
	imports: [
		BrowserModule,
		HttpModule,
		FormsModule,
		NgbModalModule,
		NgbTooltipModule,
		CommonModule
	],
	exports: [
		CalendarComponent,
		ModalContentComponent,
		ModalCommentsComponent,
		ModalMaterialsComponent
	]
})
export default class CalendarModule { }
