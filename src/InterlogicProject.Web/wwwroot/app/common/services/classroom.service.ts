import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Classroom } from "../models";

@Injectable()
export default class ClassroomService {
	private classrooms = "api/classrooms";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getClassrooms(): Observable<Classroom[]> {
		return this.http.get(this.classrooms)
			.map(response => response.json() as Classroom[])
			.first();
	}

	getClassroom(id: number): Observable<Classroom> {
		return this.http.get(`${this.classrooms}/${id}`)
			.map(response => response.json() as Classroom)
			.first();
	}

	getByBuilding(buildingId: number): Observable<Classroom[]> {
		return this.http.get(`${this.classrooms}/buildingId/${buildingId}`)
			.map(response => response.json() as Classroom[])
			.first();
	}

	getByClass(classId: number): Observable<Classroom[]> {
		return this.http.get(`${this.classrooms}/classId/${classId}`)
			.map(response => response.json() as Classroom[])
			.first();
	}

	addClassroom(classroom: Classroom): ConnectableObservable<Response> {
		const action = this.http.post(
			this.classrooms,
			JSON.stringify(classroom),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				classroom.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateClassroom(classroom: Classroom): ConnectableObservable<Response> {
		return this.http.put(
			`${this.classrooms}/${classroom.id}`,
			JSON.stringify(classroom),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteClassroom(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.classrooms}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
