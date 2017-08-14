import { Component, OnInit, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";

import * as $ from "jquery";

import { AccountService } from "../services/account.service";
import { LoginModel } from '../models/models';

@Component({
	selector: "ip-login",
	templateUrl: "/templates/account/login"
})
export class LoginComponent implements OnInit, OnDestroy {
	model: LoginModel = {
		username: "",
		password: ""
	};

	loginError = false;

	private router: Router;
	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	ngOnInit(): void {
		this.accountService.setLoggingIn(true);
	}

	ngOnDestroy(): void {
		this.accountService.setLoggingIn(false);
	}

	returnUrl(): string {
		const result = this.accountService.getReturnUrl();
		return result ? result : "";
	}

	change(): void {
		this.loginError = false;
	}

	submit(): void {
		const modelToSubmit: LoginModel = {
			username: this.model.username + $("#emailDomain").text().trim(),
			password: this.model.password
		};

		this.accountService.login(modelToSubmit)
			.subscribe(
				(success: boolean) => {
					if (!success) {
						this.loginError = true;
						this.model.password = "";
					}
				},
				() => {
					this.loginError = true;
					this.model.password = "";
				});
	}

	cancel(): void {
		this.router.navigate([ this.returnUrl() ]);
	}
}
