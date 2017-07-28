import { Component, OnInit } from "@angular/core";

import * as moment from "moment";

import { Notification } from "../../common/models";
import { NotificationService } from "../../common/common";

@Component({
	selector: "ip-view-notifications",
	templateUrl: "/templates/view/notifications"
})
export default class NotificationsComponent implements OnInit {
	private notificationService: NotificationService;

	notifications: Notification[] = [];

	constructor(notificationService: NotificationService) {
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
