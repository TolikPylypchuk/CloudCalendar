import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import * as moment from "moment";

import { getHeaders } from "../functions";
import { Notification } from "../models";

@Injectable()
export default class NotificationService {
	private notifications = "/api/notifications";

	private http: Http;

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
			.map(response => response.json() as Notification)
			.first();
	}

	getNotificationsForUser(userId: string): Observable<Notification[]> {
		return this.http.get(
			`${this.notifications}/userId/${userId}`,
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

	updateNotification(notification: Notification): ConnectableObservable<Response> {
		return this.http.put(
			`${this.notifications}/${notification.id}`,
			JSON.stringify(notification),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteNotification(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.notifications}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
