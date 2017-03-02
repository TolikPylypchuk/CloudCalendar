import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

import {
	Class, Classroom, Group, Lecturer, Comment
} from "../../../common/models";

@Injectable()
export default class ClassService {
	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getClass(id: number): Observable<Class> {
		return this.http.get(`api/classes/${id}`)
						.map(response => response.json() as Class)
						.catch(this.handleError);
	}

	getPlaces(classId: number): Observable<Classroom[]> {
		return this.http.get(`api/classrooms/classId/${classId}`)
						.map(response => response.json() as Classroom[])
						.catch(this.handleError);
	}

	getGroups(classId: number): Observable<Group[]> {
		return this.http.get(`api/groups/classId/${classId}`)
						.map(response => response.json() as Group[])
						.catch(this.handleError);
	}

	getLecturers(classId: number): Observable<Lecturer[]> {
		return this.http.get(`api/lecturers/classId/${classId}`)
						.map(response => response.json() as Lecturer[])
						.catch(this.handleError);
	}

	getComments(classId: number): Observable<Comment[]> {
		return this.http.get(`api/comments/classId/${classId}`)
						.map(response => response.json() as Comment[])
						.catch(this.handleError);
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
