import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";
import { Subject } from "rxjs/Subject";

import * as moment from "moment";

import { getHeaders } from "../functions";
import { Notification } from "../models";

@Injectable()
export class NotificationService {
	private notifications = "/api/notifications";

	private http: Http;

	private notificationsSubject = new Subject<boolean>();

	constructor(http: Http) {
		this.http = http;
	}

	getNotifications(): Observable<Notification[]> {
		return this.http.get(
			this.notifications,
			{ headers: getHeaders() })
			.map(response => response.json() as Notification[])
			.first();
	}

	getNotification(id: number): Observable<Notification> {
		return this.http.get(
			`${this.notifications}/${id}`,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Notification
					: null)
			.first();
	}

	getNotificationsForUser(userId: string): Observable<Notification[]> {
		return this.http.get(
			`${this.notifications}/userId/${userId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Notification[])
			.first();
	}

	getNotificationsForCurrentUser(): Observable<Notification[]> {
		return this.http.get(
			`${this.notifications}/user/current`,
			{ headers: getHeaders() })
			.map(response => response.json() as Notification[])
			.first();
	}

	getNotificationsInRange(
		start: moment.Moment,
		end: moment.Moment): Observable<Notification[]> {
		return this.http.get(
			`${this.notifications}/range/${start.format("YYYY-MM-DD")}/` +
			`${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Notification[])
			.first();
	}

	getNotificationsForUserInRange(
		userId: string,
		start: moment.Moment,
		end: moment.Moment): Observable<Notification[]> {
		return this.http.get(
			`${this.notifications}/userId/${userId}/range/` +
			`${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Notification[])
			.first();
	}
	
	addNotification(
		notification: Notification): ConnectableObservable<Response> {
		const action = this.http.post(
			this.notifications,
			JSON.stringify(notification),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				notification.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	addNotificationForGroupsInClass(
		notification: Notification,
		classId: number): ConnectableObservable<Response> {
		const action = this.http.post(
			`${this.notifications}/groups/classId/${classId}`,
			JSON.stringify(notification),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				notification.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	addNotificationForLecturersInClass(
		notification: Notification,
		classId: number): ConnectableObservable<Response> {
		const action = this.http.post(
			`${this.notifications}/lecturers/classId/${classId}`,
			JSON.stringify(notification),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				notification.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	markNotificationAsSeen(
		notification: Notification): ConnectableObservable<Response> {
		const action = this.http.put(
			`${this.notifications}/${notification.id}/mark/seen`,
			"",
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(response => notification.isSeen = true);

		this.notificationsSubject.next(true);

		return action;
	}

	markNotificationAsNotSeen(
		notification: Notification): ConnectableObservable<Response> {
		const action = this.http.put(
			`${this.notifications}/${notification.id}/mark/notSeen`,
			"",
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(response => notification.isSeen = false);

		this.notificationsSubject.next(false);

		return action;
	}

	deleteNotification(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.notifications}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	notificationsMarkObservable(): Observable<boolean> {
		return this.notificationsSubject;
	}
}
