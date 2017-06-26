import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "../account/account";

import ViewComponent from "./view.component";
import NotificationsComponent from "./components/notifications.component";

const routes: Routes = [
	{
		path: "view",
		component: ViewComponent,
		children: [
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
