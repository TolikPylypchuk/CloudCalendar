import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs/Observable";

import { AccountService } from "./account/account";
import { NotificationService } from "./common/common";

@Component({
	selector: "ip-navigation",
	templateUrl: "templates/navigation"
})
export default class NavigationComponent implements OnInit {
    private accoutService: AccountService;
	private notificationService: NotificationService;

	notificationCount: Observable<number>;

    constructor(
        accoutService: AccountService,
        notificationService: NotificationService) {
        this.accoutService = accoutService;
        this.notificationService = notificationService;
	}

	ngOnInit(): void {
		this.notificationCount =
			this.notificationService.getNotificationsForCurrentUser()
				.map(notifications =>
					notifications.filter(n => !n.isSeen).length);
	}

	isLoggedIn(): boolean {
		return this.accoutService.isLoggedIn();
	}

	getUserName(): Observable<string> {
		return this.accoutService.getCurrentUser()
			.map(user => `${user.firstName} ${user.lastName}`)
			.first();
    }
}
