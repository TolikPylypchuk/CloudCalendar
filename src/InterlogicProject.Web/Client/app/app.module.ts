import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "./common/common";
import { AccountModule } from "./account/account";
import { LecturerModule } from "./lecturer/lecturer";
import { StudentModule } from "./student/student";
import { ViewModule } from "./view/view";

import { RoutesModule } from "./routes.module";

import { AppComponent } from "./app.component";
import { NavigationComponent } from "./navigation.component";
import { StartPageComponent } from "./start-page.component";

@NgModule({
	declarations: [
		AppComponent,
		NavigationComponent,
		StartPageComponent
	],
	imports: [
		BrowserModule,
		NgbModule.forRoot(),
		CommonModule,
		AccountModule,
		LecturerModule,
		StudentModule,
		ViewModule,
		RoutesModule
	],
	bootstrap: [
		AppComponent
	]
})
export class AppModule { }
