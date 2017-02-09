import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";

import StudentService from "./services/student.service";
import ClassService from "./services/class.service";

@NgModule({
	providers: [
		StudentService,
		ClassService
	],
	imports: [
		HttpModule
	]
})
class CommonModule { }

export {
	StudentService,
	ClassService,

	CommonModule
}
