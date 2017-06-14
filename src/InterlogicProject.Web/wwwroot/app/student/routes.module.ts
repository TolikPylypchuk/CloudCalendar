import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "../account/account";

import StudentComponent from "./student.component";
import { CalendarComponent } from "./calendar/calendar";

const routes: Routes = [
	{
		path: "student",
		component: StudentComponent,
		children: [
			{ path: "calendar", component: CalendarComponent }
		],
		canActivate: [ AuthGuard ],
		canActivateChild: [ AuthGuard ]
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
export default class RoutesModule { }
