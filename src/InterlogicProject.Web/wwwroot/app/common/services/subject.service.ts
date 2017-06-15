import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Subject } from "../models";

@Injectable()
export default class SubjectService {
	private subjects = "/api/subjects";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getSubjects(): Observable<Subject[]> {
		return this.http.get(
			this.subjects,
			{ headers: getHeaders() })
			.map(response => response.json() as Subject[])
			.first();
	}

	getSubject(id: number): Observable<Subject> {
		return this.http.get(
			`${this.subjects}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Subject)
			.first();
	}

	addSubject(subject: Subject): ConnectableObservable<Response> {
		const action = this.http.post(
			this.subjects,
			JSON.stringify(subject),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				subject.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateSubject(subject: Subject): ConnectableObservable<Response> {
		return this.http.put(
			`${this.subjects}/${subject.id}`,
			JSON.stringify(subject),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteSubject(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.subjects}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
