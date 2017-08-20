import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";
import { ReplaySubject } from "rxjs/ReplaySubject";

import { getHeaders } from "../functions";
import { Student } from "../models";

import { AccountService } from "../../account/account";

@Injectable()
export class StudentService {
	private students = "/api/students";

	private currentStudentSource = new ReplaySubject<Student>();
	private currentUserId: string = null;

	private http: Http;

	private accountService: AccountService;

	constructor(http: Http, accountService: AccountService) {
		this.http = http;
		this.accountService = accountService;
	}
	
	getCurrentStudent(): Observable<Student> {
		return this.accountService.getCurrentUser()
			.map(user =>
				user.id === this.currentUserId
					? this.currentStudentSource.asObservable()
					: this.http.get(
						`${this.students}/userId/${user.id}`,
						{ headers: getHeaders() })
						.map(response => {
							const student = response.json() as Student;
							this.currentUserId = student.userId;
							this.currentStudentSource.next(student);
							return student;
						}))
			.switch() as any;
	}

	getStudents(): Observable<Student[]> {
		return this.http.get(
			this.students,
			{ headers: getHeaders() })
			.map(response => response.json() as Student[])
			.first();
	}

	getStudent(id: number): Observable<Student> {
		return this.http.get(
			`${this.students}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Student)
			.first();
	}

	getStudentByUserId(userId: number): Observable<Student> {
		return this.http.get(
			`${this.students}/userId/${userId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Student)
			.first();
	}

	getStudentByEmail(email: number): Observable<Student> {
		return this.http.get(
			`${this.students}/email/${email}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Student)
			.first();
	}

	getStudentByTranscript(transcript: string): Observable<Student> {
		return this.http.get(
			`${this.students}/transcript/${transcript}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Student)
			.first();
	}

	getStudentsByGroup(groupId: number): Observable<Student[]> {
		return this.http.get(
			`${this.students}/groupId/${groupId}`,
			{ headers: getHeaders() })
			.map(response =>
				response.status === 200
					? response.json() as Student[]
					: null)
			.first();
	}

	addStudent(student: Student): ConnectableObservable<Response> {
		const action = this.http.post(
			this.students,
			JSON.stringify(student),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				student.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateStudent(student: Student): ConnectableObservable<Response> {
		return this.http.put(
			`${this.students}/${student.id}`,
			JSON.stringify(student),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteStudent(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.students}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
