import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import * as moment from "moment";

import { AccountService } from "../../account/account";

import { Notification } from "../../common/models";
import { ClassService, NotificationService } from "../../common/common";

@Component({
	selector: "ip-view-notifications",
	templateUrl: "/templates/view/notifications"
})
export class NotificationsComponent implements OnInit {
	private router: Router;

	private accountService: AccountService;
	private classService: ClassService;
	private notificationService: NotificationService;

	notifications: Notification[] = [];

	constructor(
		router: Router,
		accountService: AccountService,
		classService: ClassService,
		notificationService: NotificationService) {
		this.router = router;
		this.accountService = accountService;
		this.classService = classService;
		this.notificationService = notificationService;
	}

	ngOnInit(): void {
		this.notificationService.getNotificationsForCurrentUser()
			.subscribe(notifications =>
				this.notifications = notifications.sort(
					this.compareByTimeDescending));
	}

	formatDateTime(dateTime: string): string {
		return moment(dateTime).format("DD.MM.YYYY HH:mm");
	}

	markAsSeen(notification: Notification): void {
		this.notificationService
			.markNotificationAsSeen(notification)
			.connect();
	}

	markAsNotSeen(notification: Notification): void {
		this.notificationService
			.markNotificationAsNotSeen(notification)
			.connect();
	}

	goToClass(notification: Notification): void {
		this.classService.getClass(notification.classId)
			.subscribe(c => {
				if (c) {
					this.accountService.isStudent()
						.subscribe(isStudent => {
							const path = isStudent ? "student" : "lecturer";
							const dateTime = moment(c.dateTime);

							this.router.navigate([
								`${path}/calendar`,
								dateTime.format("YYYY-MM-DD"),
								dateTime.format("HH:mm")
							]);
						});
				}
			});
	}

	private compareByTimeDescending(
		a: Notification,
		b: Notification): number {
		const moment1 = moment(a.dateTime);
		const moment2 = moment(b.dateTime);

		return moment1.isBefore(moment2)
			? 1
			: moment1.isAfter(moment2)
				? -1
				: 0;
	}
}
