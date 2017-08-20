import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { LoginComponent } from "./components/login.component";
import { LogoutComponent } from "./components/logout.component";

import { NotAuthGuard } from "./guards/not-auth.guard";

const routes: Routes = [
	{ path: "login", component: LoginComponent, canActivate: [ NotAuthGuard ] },
	{ path: "logout", component: LogoutComponent }
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
