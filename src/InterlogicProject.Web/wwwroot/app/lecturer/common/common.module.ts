import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";

import LecturerService from "./services/lecturer.service";
import ClassService from "./services/class.service";

@NgModule({
	providers: [
		LecturerService,
		ClassService
	],
	imports: [
		HttpModule
	]
})
export default class CommonModule { }
