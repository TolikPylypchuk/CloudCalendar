import { Injectable } from "@angular/core";
import {
	CanActivate, CanActivateChild, Router,
	ActivatedRouteSnapshot, RouterStateSnapshot
	} from "@angular/router";
import { Observable } from "rxjs/Observable";

import { AccountService } from "../services/account.service";

import { User } from "../../common/models";

@Injectable()
export class StudentGuard implements CanActivate, CanActivateChild {
	private router: Router;
	private accountService: AccountService;

	constructor(router: Router, accountService: AccountService) {
		this.router = router;
		this.accountService = accountService;
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean> {
		return this.accountService.getCurrentUser()
			.map((currentUser: User) => {
				if (currentUser && currentUser.roles.find(
					role => role.toLocaleLowerCase() === "student")) {
					return true;
				}

				this.accountService.setReturnUrl(state.url);
				this.router.navigate(["/login"]);

				return false;
			});
	}

	canActivateChild(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean> {
		return this.canActivate(route, state);
	}
}
