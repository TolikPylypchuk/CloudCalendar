import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

import { handleError } from "../functions";
import { Lecturer, Student, User } from "../models";

@Injectable()
export default class LecturerService {
	private currentUserSource = new BehaviorSubject<User>(null);
	private currentLecturerSource = new BehaviorSubject<Lecturer>(null);

	private http: Http;

	constructor(http: Http) {
		this.http = http;

		this.http.get("/api/users/current")
			.map(response => response.json())
			.catch(handleError)
			.first()
			.subscribe(data => this.initUser(data as User));
	}
	
	getCurrentUser(): Observable<User> {
		return this.currentUserSource.asObservable();
	}

	getCurrentLecturer(): Observable<Lecturer> {
		return this.currentLecturerSource.asObservable();
	}

	getLecturer(id: number): Observable<Lecturer> {
		return this.http.get(`api/lecturers/${id}`)
			.map(response => response.json() as Lecturer)
			.catch(handleError)
			.first();
	}

	getStudent(id: number): Observable<Student> {
		return this.http.get(`api/students/${id}`)
			.map(response => response.json() as Student)
			.catch(handleError)
			.first();
	}
	
	private initUser(user: User): void {
		this.currentUserSource.next(user);

		this.http.get(`/api/lecturers/userId/${user.id}`)
			.map(response => response.json())
			.catch(handleError)
			.first()
			.subscribe(data => this.initLecturer(data as Lecturer));
	}

	private initLecturer(lecturer: Lecturer) {
		this.currentLecturerSource.next(lecturer);
	}
}
