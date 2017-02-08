import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Observable } from "rxjs/Observable";

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
			.subscribe(data => this.initStudent(data as Student));
	}

	private initStudent(student: Student) {
		this.currentStudentSource.next(student);

		this.http.get(`/api/groups/id/${student.groupId}`)
			.map(response => response.json())
			.subscribe(data => this.initGroup(data as Group));
	}

	private initGroup(group: Group) {
		this.currentGroupSource.next(group);
	}
}
