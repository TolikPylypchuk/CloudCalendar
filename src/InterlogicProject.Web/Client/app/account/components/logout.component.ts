import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AccountService } from "../services/account.service";

@Component({
	selector: "ip-logout",
	template: ""
})
export class LogoutComponent implements OnInit {
	private router: Router;
	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	ngOnInit(): void {
		this.accountService.logout();
		this.router.navigate([ "" ]);
	}
}
