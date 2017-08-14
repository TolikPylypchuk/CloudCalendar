import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "../account/account";

import { ViewComponent } from "./view.component";
import { GroupsComponent } from "./components/groups.component";
import { GroupComponent } from "./components/group.component";
import { LecturersComponent } from "./components/lecturers.component";
import { LecturerComponent } from "./components/lecturer.component";
import { NotificationsComponent } from "./components/notifications.component";

const routes: Routes = [
	{
		path: "view",
		component: ViewComponent,
		children: [
			{ path: "groups", component: GroupsComponent },
			{ path: "group/:id", component: GroupComponent },
			{ path: "group/:id/:date", component: GroupComponent },
			{ path: "lecturers", component: LecturersComponent },
			{ path: "lecturer/:id", component: LecturerComponent },
			{ path: "lecturer/:id/:date", component: LecturerComponent },
			{ path: "notifications", component: NotificationsComponent }
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
export class RoutesModule { }
