import  { Injectable } from "@angular/core";
import {
	CanActivate, CanActivateChild, Router,
	ActivatedRouteSnapshot, RouterStateSnapshot
} from "@angular/router";

import AccountService from "../services/account.service";

@Injectable()
export default class NotAuthGuard implements CanActivate, CanActivateChild {
	private router: Router;
	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): boolean {
		if (!this.accountService.isLoggedIn()) {
			return true;
		}

		this.router.navigate([ "" ]);

		return false;
	}

	canActivateChild(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): boolean {
		return this.canActivate(route, state);
	}
}
