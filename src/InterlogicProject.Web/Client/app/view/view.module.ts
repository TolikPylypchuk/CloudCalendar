import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "../common/common";

import { GroupsComponent } from "./components/groups.component";
import { GroupComponent } from "./components/group.component";
import { LecturersComponent } from "./components/lecturers.component";
import { LecturerComponent } from "./components/lecturer.component";
import { NotificationsComponent } from "./components/notifications.component";
import { ViewComponent } from "./view.component";

import { RoutesModule } from "./routes.module";

@NgModule({
	declarations: [
		GroupsComponent,
		GroupComponent,
		LecturersComponent,
		LecturerComponent,
		NotificationsComponent,
		ViewComponent
	],
	imports: [
		BrowserModule,
		NgbModule,
		CommonModule,
		RoutesModule
	],
	exports: [
		GroupsComponent,
		GroupComponent,
		LecturersComponent,
		LecturerComponent,
		NotificationsComponent,
		ViewComponent
	]
})
export class ViewModule { }
