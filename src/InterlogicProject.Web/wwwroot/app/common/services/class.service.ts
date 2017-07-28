import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import * as moment from "moment";

import { getHeaders } from "../functions";

import { Class } from "../models";

@Injectable()
export default class ClassService {
	private classes = "/api/classes";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}
	
	getClasses(): Observable<Class[]> {
		return this.http.get(
			this.classes,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Class[]
					: null)
			.first();
	}
	
	getClass(id: number): Observable<Class> {
		return this.http.get(
			`${this.classes}/${id}`,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Class
					: null)
			.first();
	}

	getClassesInRange(
		start: moment.Moment,
		end: moment.Moment): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/range/${start.format("YYYY-MM-DD")}/` +
			`${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassesForGroup(groupId: number): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/groupId/${groupId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassesForGroupInRange(
		groupId: number,
		start: moment.Moment,
		end: moment.Moment): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/groupId/${groupId}/range/` +
			`${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassForGroupByDateTime(
		groupId: number,
		dateTime: moment.Moment): Observable<Class> {
		return this.http.get(
			`${this.classes}/groupId/${groupId}/` +
			`dateTime/${dateTime.format("YYYY-MM-DDTHH:mm") }`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class)
			.first();
	}

	getClassesForLecturer(lecturerId: number): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/lecturerId/${lecturerId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassesForLecturerInRange(
		lecturerId: number,
		start: moment.Moment,
		end: moment.Moment): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/lecturerId/${lecturerId}/range/` +
			`${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassForLecturerByDateTime(
		lecturerId: number,
		dateTime: moment.Moment): Observable<Class> {
		return this.http.get(
			`${this.classes}/lecturerId/${lecturerId}/` +
			`dateTime/${dateTime.format("YYYY-MM-DDTHH:mm")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class)
			.first();
	}

	getClassesForGroupAndLecturer(
		groupId: number,
		lecturerId: number): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/groupId/${groupId}/lecturerId/${lecturerId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassesForGroupAndLecturerInRange(
		groupId: number,
		lecturerId: number,
		start: moment.Moment,
		end: moment.Moment): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/groupId/${groupId}/lecturerId/${lecturerId}/range/` +
			`${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassesForClassroom(classroomId: number): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/classroomId/${classroomId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	getClassesForClassroomInRange(
		classroomId: number,
		start: moment.Moment,
		end: moment.Moment): Observable<Class[]> {
		return this.http.get(
			`${this.classes}/classroomId/${classroomId}/range/` +
			`${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Class[])
			.first();
	}

	addClass(c: Class): ConnectableObservable<Response> {
		const action = this.http.post(
			this.classes,
			JSON.stringify(c),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				c.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateClass(c: Class): ConnectableObservable<Response> {
		return this.http.put(
			`${this.classes}/${c.id}`,
			JSON.stringify(c),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteClass(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.classes}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
