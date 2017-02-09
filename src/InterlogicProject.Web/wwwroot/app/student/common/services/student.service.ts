import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

import { Student, Group, User } from "../../../common/models";

@Injectable()
export default class StudentService {
	private currentUserSource = new BehaviorSubject<User>(null);
	private currentStudentSource = new BehaviorSubject<Student>(null);
	private currentGroupSource = new BehaviorSubject<Group>(null);

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

	getCurrentStudent(): Observable<Student> {
		return this.currentStudentSource.asObservable();
	}

	getCurrentGroup(): Observable<Group> {
		return this.currentGroupSource.asObservable();
	}

	private initUser(user: User): void {
		this.currentUserSource.next(user);

		this.http.get(`/api/students/userId/${user.id}`)
			.map(response => response.json())
			.catch(this.handleError)
			.subscribe(data => this.initStudent(data as Student));
	}

	private initStudent(student: Student) {
		this.currentStudentSource.next(student);

		this.http.get(`/api/groups/id/${student.groupId}`)
			.map(response => response.json())
			.catch(this.handleError)
			.subscribe(data => this.initGroup(data as Group));
	}

	private initGroup(group: Group) {
		this.currentGroupSource.next(group);
	}

	private handleError(error: Response | any): ErrorObservable<string> {
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
