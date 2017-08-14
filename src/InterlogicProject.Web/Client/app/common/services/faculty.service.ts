import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Faculty } from "../models";

@Injectable()
export class FacultyService {
	private faculties = "/api/faculties";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getFaculties(): Observable<Faculty[]> {
		return this.http.get(
			this.faculties,
			{ headers: getHeaders() })
			.map(response => response.json() as Faculty[])
			.first();
	}

	getFaculty(id: number): Observable<Faculty> {
		return this.http.get(
			`${this.faculties}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Faculty)
			.first();
	}

	getFacultiesByBuilding(buildingId: number): Observable<Faculty[]> {
		return this.http.get(
			`${this.faculties}/buildingId/${buildingId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Faculty[])
			.first();
	}

	addFaculty(faculty: Faculty): ConnectableObservable<Response> {
		const action = this.http.post(
			this.faculties,
			JSON.stringify(faculty),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				faculty.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateFaculty(faculty: Faculty): ConnectableObservable<Response> {
		return this.http.put(
			`${this.faculties}/${faculty.id}`,
			JSON.stringify(faculty),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteFaculty(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.faculties}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
