import { Component, OnInit, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";

import { AuthService } from "../services/auth.service";
import { LoginModel } from '../models/models';

@Component({
	selector: "schedule-login",
	templateUrl: "./login.component.html"
})
export class LoginComponent implements OnInit, OnDestroy {
	model: LoginModel = {
		username: "",
		password: ""
	};

	loginError = false;

	private router: Router;
	private authService: AuthService;

	constructor(router: Router, authService: AuthService) {
		this.router = router;
		this.authService = authService;
	}

	ngOnInit(): void {
		this.authService.setLoggingIn(true);
	}

	ngOnDestroy(): void {
		this.authService.setLoggingIn(false);
	}

	returnUrl(): string {
		const result = this.authService.getReturnUrl();
		return result ? result : "";
	}

	change(): void {
		this.loginError = false;
	}

	submit(): void {
		this.authService.login(this.model)
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
