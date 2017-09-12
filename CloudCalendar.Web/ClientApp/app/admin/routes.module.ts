import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AdminComponent } from "./admin.component";

import { UsersComponent } from "./components/users.component";
import { SettingsComponent } from "./components/settings.component";
import { ScheduleComponent } from "./components/schedule.component";

import { AuthGuard, AdminGuard } from "../account/account";

const routes: Routes = [
	{
		path: "admin",
		component: AdminComponent,
		children: [
			{ path: "settings", component: SettingsComponent },
			{ path: "users", component: UsersComponent },
			{ path: "schedule", component: ScheduleComponent }
		],
		canActivate: [ AuthGuard, AdminGuard ],
		canActivateChild: [ AuthGuard, AdminGuard ]
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
