import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Department } from "../models";

@Injectable()
export class DepartmentService {
	private departments = "/api/departments";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getDepartments(): Observable<Department[]> {
		return this.http.get(
			this.departments,
			{ headers: getHeaders() })
			.map(response => response.json() as Department[])
			.first();
	}

	getDepartment(id: number): Observable<Department> {
		return this.http.get(
			`${this.departments}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Department)
			.first();
	}

	getDepartmentsByFaculty(facultyId: number): Observable<Department[]> {
		return this.http.get(
			`${this.departments}/facultyId/${facultyId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Department[])
			.first();
	}
	
	addDepartment(department: Department): ConnectableObservable<Response> {
		const action = this.http.post(
			this.departments,
			JSON.stringify(department),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				department.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateDepartment(department: Department): ConnectableObservable<Response> {
		return this.http.put(
			`${this.departments}/${department.id}`,
			JSON.stringify(department),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteDepartment(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.departments}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
