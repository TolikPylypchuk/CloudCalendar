import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";

import LoginComponent from "./components/login.component";
import LogoutComponent from "./components/logout.component";

import AccountService from "./services/account.service";

import AuthGuard from "./guards/auth.guard";
import NotAuthGuard from "./guards/not-auth.guard";

import RoutesModule from "./routes.module";

@NgModule({
	declarations: [
		LoginComponent,
		LogoutComponent
	],
	imports: [
		BrowserModule,
		FormsModule,
		HttpModule,
		RoutesModule
	],
	providers: [
		AccountService,

		AuthGuard,
		NotAuthGuard
	]
})
export default class AccountModule { }
