import { Component } from "@angular/core";
import { Observable } from "rxjs/Observable";

import { AccountService } from "./account/account";

@Component({
	selector: "ip-navigation",
	templateUrl: "templates/app/navigation"
})
export default class NavigationComponent {
	private accoutService: AccountService;

	constructor(accoutService: AccountService) {
		this.accoutService = accoutService;
	}

	isLoggedin(): boolean {
		return this.accoutService.isLoggedIn();
	}

	getUserName(): Observable<string> {
		return this.accoutService.getCurrentUser()
			.map(user => `${user.firstName} ${user.lastName}`)
			.first();
	}
}
