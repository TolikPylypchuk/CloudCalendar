import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "./common/common";
import { AccountModule } from "./account/account";
import { StudentModule } from "./student/student";
import { LecturerModule } from "./lecturer/lecturer";

import RoutesModule from "./routes.module";

import AppComponent from "./app.component";
import NavigationComponent from "./navigation.component";
import StartPageComponent from "./start-page.component";

@NgModule({
	declarations: [
		AppComponent,
		NavigationComponent,
		StartPageComponent
	],
	imports: [
		NgbModule.forRoot(),
		CommonModule,
		AccountModule,
		LecturerModule,
		StudentModule,
		RoutesModule
	],
	bootstrap: [
		AppComponent
	]
})
export default class AppModule { }
