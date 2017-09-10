import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { NgbModalModule } from "@ng-bootstrap/ng-bootstrap";

import { RoutesModule } from "./routes.module";
import { AccountModule } from "../account/account";

import { AdminComponent } from "./admin.component";

import { UsersComponent } from "./components/users.component";
import { SettingsComponent } from "./components/settings.component";
import { BuildingModalComponent } from "./components/building-modal.component";
import { ClassroomModalComponent } from "./components/classroom-modal.component";
import { DepartmentModalComponent } from "./components/department-modal.component";
import { FacultyModalComponent } from "./components/faculty-modal.component";
import { StudentModalComponent } from "./components/student-modal.component";
import { SubjectModalComponent } from "./components/subject-modal.component";

@NgModule({
	declarations: [
		AdminComponent,
		UsersComponent,
		SettingsComponent,
		BuildingModalComponent,
		ClassroomModalComponent,
		DepartmentModalComponent,
		FacultyModalComponent,
		StudentModalComponent,
		SubjectModalComponent
	],
	imports: [
		BrowserModule,
		FormsModule,
		HttpModule,
		NgbModalModule,
		AccountModule,
		RoutesModule
	],
	entryComponents: [
		BuildingModalComponent,
		ClassroomModalComponent,
		DepartmentModalComponent,
		FacultyModalComponent,
		StudentModalComponent,
		SubjectModalComponent
	]
})
export class AdminModule { }
