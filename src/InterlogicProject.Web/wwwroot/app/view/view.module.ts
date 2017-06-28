import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "../common/common";

import NotificationsComponent from "./components/notifications.component";
import ViewComponent from "./view.component";

import RoutesModule from "./routes.module";

@NgModule({
	declarations: [
		NotificationsComponent,
		ViewComponent
	],
	imports: [
		BrowserModule,
		NgbModule,
		RoutesModule
	],
	exports: [
		NotificationsComponent,
		ViewComponent
	]
})
export default class ViewModule { }
