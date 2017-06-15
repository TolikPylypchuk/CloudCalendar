import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import StartPageComponent from "./start-page.component";

import { AuthGuard } from "./account/account";

const routes: Routes = [
	{ path: "", component: StartPageComponent, canActivate: [ AuthGuard ] }
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes)
	],
	exports: [
		RouterModule
	]
})
export default class RoutesModule { }
