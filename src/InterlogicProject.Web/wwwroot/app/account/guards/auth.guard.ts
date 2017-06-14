import  { Injectable } from "@angular/core";
import {
	CanActivate, CanActivateChild, Router,
	ActivatedRouteSnapshot, RouterStateSnapshot
} from "@angular/router";

import { AccountService } from "../services/account.service";

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {
	private router: Router;
	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): boolean {
		if (this.accountService.isLoggedIn()) {
			return true;
		}

		this.accountService.setReturnUrl(state.url);
		this.router.navigate([ "/login" ]);

		return false;
	}

	canActivateChild(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): boolean {
		return this.canActivate(route, state);
	}
}
