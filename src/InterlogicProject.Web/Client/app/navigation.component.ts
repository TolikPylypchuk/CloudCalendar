import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs/Observable";

import { AccountService } from "./account/account";
import { NotificationService } from "./common/common";

@Component({
	selector: "ip-navigation",
	templateUrl: "templates/navigation"
})
export class NavigationComponent implements OnInit {
    private accoutService: AccountService;
	private notificationService: NotificationService;

	notificationCount = 0;

    constructor(
        accoutService: AccountService,
        notificationService: NotificationService) {
        this.accoutService = accoutService;
        this.notificationService = notificationService;
	}

	ngOnInit(): void {
		if (this.isLoggedIn()) {
			this.notificationService.getNotificationsForCurrentUser()
				.map(notifications =>
					notifications.filter(n => !n.isSeen).length)
				.subscribe(count => this.notificationCount = count);
		}
		
		this.notificationService.notificationsMarkObservable()
			.subscribe(seen =>
				this.notificationCount == 0 ? this.notificationCount :
					seen ? this.notificationCount-- : this.notificationCount++);
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
