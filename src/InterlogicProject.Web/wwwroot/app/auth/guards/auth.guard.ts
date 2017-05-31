import  { Injectable } from "@angular/core";
import {
	CanActivate, CanActivateChild, Router,
	ActivatedRouteSnapshot, RouterStateSnapshot
} from "@angular/router";

import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {
	private router: Router;
	private authService: AuthService;

	constructor(router: Router, authService: AuthService) {
		this.router = router;
		this.authService = authService;
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): boolean {
		if (this.authService.isLoggedIn()) {
			return true;
		}

		this.authService.setReturnUrl(state.url);
		this.router.navigate([ "/login" ]);

		return false;
	}

	canActivateChild(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): boolean {
		return this.canActivate(route, state);
	}
}
