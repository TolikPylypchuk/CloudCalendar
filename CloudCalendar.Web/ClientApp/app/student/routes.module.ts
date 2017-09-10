import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard, StudentGuard } from "../account/account";

import { StudentComponent } from "./student.component";
import { CalendarComponent } from "./calendar/calendar";

const routes: Routes = [
	{
		path: "student",
		component: StudentComponent,
		children: [
			{ path: "calendar", component: CalendarComponent },
			{ path: "calendar/:date", component: CalendarComponent },
			{ path: "calendar/:date/:time", component: CalendarComponent }
		],
		canActivate: [ AuthGuard, StudentGuard ],
		canActivateChild: [ AuthGuard, StudentGuard ]
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
