import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";

import StudentService from "./services/student.service";

@NgModule({
	providers: [
		StudentService
	],
	imports: [
		HttpModule
	]
})
class CommonModule { }

export {
	StudentService,

	CommonModule
}
