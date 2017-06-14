import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Group } from "../models";

@Injectable()
export default class GroupService {
	private groups = "api/groups";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getGroups(): Observable<Group[]> {
		return this.http.get(this.groups)
			.map(response => response.json() as Group[])
			.first();
	}

	getGroup(id: number): Observable<Group> {
		return this.http.get(`${this.groups}/${id}`)
			.map(response => response.json() as Group)
			.first();
	}

	getGroupsByDepartment(departmentId: number): Observable<Group[]> {
		return this.http.get(this.groups)
			.map(response => response.json() as Group[])
			.first();
	}

	addGroup(group: Group): ConnectableObservable<Response> {
		const action = this.http.post(
			this.groups,
			JSON.stringify(group),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				group.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateGroup(group: Group): ConnectableObservable<Response> {
		return this.http.put(
			`${this.groups}/${group.id}`,
			JSON.stringify(group),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteGroup(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.groups}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
