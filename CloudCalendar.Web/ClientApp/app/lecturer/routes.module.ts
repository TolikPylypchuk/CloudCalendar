import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard, LecturerGuard } from "../account/account";

import { LecturerComponent } from "./lecturer.component";
import { CalendarComponent } from "./calendar/calendar";

const routes: Routes = [
	{
		path: "lecturer",
		component: LecturerComponent,
		children: [
			{ path: "calendar", component: CalendarComponent },
			{ path: "calendar/:date", component: CalendarComponent },
			{ path: "calendar/:date/:time", component: CalendarComponent }
		],
		canActivate: [ AuthGuard, LecturerGuard ],
		canActivateChild: [ AuthGuard, LecturerGuard ]
	}
];

@NgModule({
	imports: [
		RouterModule.forChild(routes)
	],
	exports: [
		RouterModule
	]
})
export class RoutesModule { }
