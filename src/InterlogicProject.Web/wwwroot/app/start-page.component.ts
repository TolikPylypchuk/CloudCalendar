import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AccountService } from "./account/account";

@Component({
	selector: "ip-start-page",
	template: ""
})
export default class StartPageComponent implements OnInit {
	private router: Router;

	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	ngOnInit(): void {
		this.accountService.isStudent()
			.subscribe(isStudent =>
				this.router.navigate(
					[`${isStudent ? "student" : "lecturer"}/calendar`]));
	}
}
