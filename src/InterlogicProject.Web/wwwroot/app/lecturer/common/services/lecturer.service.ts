import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

import { Lecturer, Group, User } from "../../../common/models";

@Injectable()
export default class LecturerService {
	private currentUserSource = new BehaviorSubject<User>(null);
	private currentLecturerSource = new BehaviorSubject<Lecturer>(null);

	private http: Http;

	constructor(http: Http) {
		this.http = http;

		this.http.get("/api/users/current")
			.map(response => response.json())
			.catch(this.handleError)
			.subscribe(data => this.initUser(data as User));
	}
	
	getCurrentUser(): Observable<User> {
		return this.currentUserSource.asObservable();
	}

	getCurrentLecturer(): Observable<Lecturer> {
		return this.currentLecturerSource.asObservable();
	}
	
	private initUser(user: User): void {
		this.currentUserSource.next(user);

		this.http.get(`/api/lecturers/userId/${user.id}`)
			.map(response => response.json())
			.catch(this.handleError)
			.subscribe(data => this.initLecturer(data as Lecturer));
	}

	private initLecturer(lecturer: Lecturer) {
		this.currentLecturerSource.next(lecturer);
	}

	private handleError(error: Response | any): ErrorObservable {
		let message: string;

		if (error instanceof Response) {
			const body = error.json() || "";
			const err = body.error || JSON.stringify(body);
			message = `${error.status} - ${error.statusText || ""} ${err}`;
		} else {
			message = error.message ? error.message : error.toString();
		}

		console.error(message);

		return Observable.throw(message);
	}
}
