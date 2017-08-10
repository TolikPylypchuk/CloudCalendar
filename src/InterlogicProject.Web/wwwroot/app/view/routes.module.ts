import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "../account/account";

import ViewComponent from "./view.component";
import GroupsComponent from "./components/groups.component";
import GroupComponent from "./components/group.component";
import NotificationsComponent from "./components/notifications.component";

const routes: Routes = [
	{
		path: "view",
		component: ViewComponent,
		children: [
			{ path: "groups", component: GroupsComponent },
			{ path: "group/:id", component: GroupComponent },
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
export default class RoutesModule { }
