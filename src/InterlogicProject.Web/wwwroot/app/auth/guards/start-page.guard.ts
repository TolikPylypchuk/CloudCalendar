import  { Injectable } from "@angular/core";
import {
	CanActivate, ActivatedRouteSnapshot,
	Router, RouterStateSnapshot
} from "@angular/router";
import { Observable } from "rxjs/Observable";

import { AuthService } from "../services/auth.service";

import { User } from "../../common/models";

@Injectable()
export class StartPageGuard implements CanActivate {
	private router: Router;
	private authService: AuthService;

	constructor(router: Router, authService: AuthService) {
		this.router = router;
		this.authService = authService;
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean> {
		if (!this.authService.isLoggedIn()) {
			this.router.navigate([ "/schedule" ]);
			return Observable.of(false);
		}

		return this.authService.getCurrentUser()
			.map((user: User) => {
				if (!user) {
					this.router.navigate([ "/schedule" ]);
					return false;
				}

				const navigateUrl = "/schedule";

				if (navigateUrl) {
					this.router.navigate([ navigateUrl ]);
				}

				return false;
			});
	}
}
