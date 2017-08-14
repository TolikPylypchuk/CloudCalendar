import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { NgbModalModule, NgbTooltipModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "../../common/common";

import { CalendarComponent } from "./calendar.component";
import { ModalContentComponent } from "./modal/modal-content.component";
import { ModalCommentsComponent } from "./modal/modal-comments.component";
import { ModalHomeworkComponent } from "./modal/modal-homework.component";
import { ModalMaterialsComponent } from "./modal/modal-materials.component";

@NgModule({
	declarations: [
		CalendarComponent,
		ModalContentComponent,
		ModalCommentsComponent,
		ModalHomeworkComponent,
		ModalMaterialsComponent
	],
	entryComponents: [
		ModalContentComponent
	],
	imports: [
		BrowserModule,
		FormsModule,
		NgbModalModule,
		NgbTooltipModule,
		CommonModule
	],
	exports: [
		CalendarComponent,

		ModalContentComponent,
		ModalCommentsComponent,
		ModalHomeworkComponent,
		ModalMaterialsComponent
	]
})
export class CalendarModule { }
