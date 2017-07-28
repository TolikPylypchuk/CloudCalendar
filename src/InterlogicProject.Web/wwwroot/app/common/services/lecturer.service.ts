import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";
import { ReplaySubject } from "rxjs/ReplaySubject";

import { getHeaders } from "../functions";
import { Lecturer, User } from "../models";

import { AccountService } from "../../account/account";

@Injectable()
export default class LecturerService {
	private lecturers = "/api/lecturers";

	private currentLecturerSource = new ReplaySubject<Lecturer>(null);
	private currentUserId: string = null;

	private http: Http;

	private accountService: AccountService;

	constructor(http: Http, accountService: AccountService) {
		this.http = http;
		this.accountService = accountService;
	}

	getCurrentLecturer(): Observable<Lecturer> {
		return this.accountService.getCurrentUser()
			.map(user =>
				user.id === this.currentUserId
					? this.currentLecturerSource.asObservable()
					: this.http.get(
						`${this.lecturers}/userId/${user.id}`,
						{ headers: getHeaders() })
						.map(response => {
							const lecturer = response.json() as Lecturer;
							this.currentUserId = lecturer.userId;
							this.currentLecturerSource.next(lecturer);
							return lecturer;
						}))
			.switch();
	}

	getLecturers(): Observable<Lecturer[]> {
		return this.http.get(
			this.lecturers,
			{ headers: getHeaders() })
			.map(response => response.json() as Lecturer[])
			.first();
	}

	getLecturer(id: number): Observable<Lecturer> {
		return this.http.get(
			`${this.lecturers}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Lecturer)
			.first();
	}

	getLecturerByUserId(userId: number): Observable<Lecturer> {
		return this.http.get(
			`${this.lecturers}/userId/${userId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Lecturer)
			.first();
	}

	getLecturerByEmail(email: number): Observable<Lecturer> {
		return this.http.get(
			`${this.lecturers}/email/${email}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Lecturer)
			.first();
	}

	getLecturersByDepartment(departmentId: number): Observable<Lecturer[]> {
		return this.http.get(
			`${this.lecturers}/departmentId/${departmentId}`,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Lecturer[]
					: null)
			.first();
	}

	getLecturersByFaculty(facultyId: number): Observable<Lecturer[]> {
		return this.http.get(
			`${this.lecturers}/facultyId/${facultyId}`,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Lecturer[]
					: null)
			.first();
	}

	getLecturersByClass(classId: number): Observable<Lecturer[]> {
		return this.http.get(
			`${this.lecturers}/classId/${classId}`,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Lecturer[]
					: null)
			.first();
	}

	addLecturer(lecturer: Lecturer): ConnectableObservable<Response> {
		const action = this.http.post(
			this.lecturers,
			JSON.stringify(lecturer),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				lecturer.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateLecturer(lecturer: Lecturer): ConnectableObservable<Response> {
		return this.http.put(
			`${this.lecturers}/${lecturer.id}`,
			JSON.stringify(lecturer),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteLecturer(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.lecturers}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
