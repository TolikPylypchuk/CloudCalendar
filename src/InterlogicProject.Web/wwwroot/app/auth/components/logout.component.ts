import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AuthService } from "../services/auth.service";

@Component({
	selector: "schedule-logout",
	template: ""
})
export class LogoutComponent implements OnInit {
	private router: Router;
	private authService: AuthService;

	constructor(router: Router, authService: AuthService) {
		this.router = router;
		this.authService = authService;
	}

	ngOnInit(): void {
		this.authService.logout();
		this.router.navigate([ "" ]);
	}
}
