import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { CalendarComponent } from "./student/calendar/calendar";

import { AuthGuard } from "./account/account";

const routes: Routes = [
	{ path: "", component: CalendarComponent, canActivate: [ AuthGuard ] }
]

@NgModule({
	imports: [
		RouterModule.forRoot(routes)
	],
	exports: [
		RouterModule
	]
})
export default class RoutesModule { }
