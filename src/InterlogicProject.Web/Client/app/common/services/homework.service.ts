import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Homework } from "../models";

@Injectable()
export class HomeworkService {
	private homeworks = "/api/homeworks";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getHomeworks(): Observable<Homework[]> {
		return this.http.get(
			this.homeworks,
			{ headers: getHeaders() })
			.map(response => response.json() as Homework[])
			.first();
	}

	getHomework(id: number): Observable<Homework> {
		return this.http.get(
			`${this.homeworks}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Homework)
			.first();
	}

	getHomeworksByClass(classId: number): Observable<Homework[]> {
		return this.http.get(
			`${this.homeworks}/classId/${classId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Homework[])
			.first();
	}

	getHomeworksByStudent(studentId: number): Observable<Homework[]> {
		return this.http.get(
			`${this.homeworks}/studentId/${studentId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Homework[])
			.first();
	}

	getHomeworkByClassAndStudent(
		classId: number,
		studentId: number): Observable<Homework> {
		return this.http.get(
			`${this.homeworks}/classId/${classId}/studentId/${studentId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Homework)
			.first();
	}

	updateHomework(homework: Homework): ConnectableObservable<Response> {
		return this.http.put(
			`${this.homeworks}/${homework.id }`,
			JSON.stringify(homework),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteHomework(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.homeworks}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
