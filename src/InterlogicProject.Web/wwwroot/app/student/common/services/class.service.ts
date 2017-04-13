﻿import { Injectable } from "@angular/core";
import {
	Http, Response, Headers
} from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

import {
	Class, Classroom, Group, Lecturer, Comment, Material, Homework
} from "../../../common/models";

@Injectable()
export default class ClassService {
	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getClass(id: number): Observable<Class> {
		return this.http.get(`api/classes/${id}`)
			.map(response =>
				response.status === 200
					? response.json() as Class
					: null)
			.catch(this.handleError);
	}

	getPlaces(classId: number): Observable<Classroom[]> {
		return this.http.get(`api/classrooms/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Classroom[]
					: null)
			.catch(this.handleError);
	}

	getGroups(classId: number): Observable<Group[]> {
		return this.http.get(`api/groups/classId/${classId}`)
						.map(response =>
							response.status === 200
								? response.json() as Group[]
								: null)
						.catch(this.handleError);
	}

	getLecturers(classId: number): Observable<Lecturer[]> {
		return this.http.get(`api/lecturers/classId/${classId}`)
						.map(response =>
							response.status === 200
								? response.json() as Lecturer[]
								: null)
						.catch(this.handleError);
	}

	getComments(classId: number): Observable<Comment[]> {
		return this.http.get(`api/comments/classId/${classId}`)
						.map(response =>
							response.status === 200
								? response.json() as Comment[]
								: null)
						.catch(this.handleError);
	}

	getMaterials(classId: number): Observable<Material[]> {
		return this.http.get(`api/materials/classId/${classId}`)
						.map(response =>
							response.status === 200
								? response.json() as Material[]
								: null)
						.catch(this.handleError);
	}
	
	getHomeworks(classId: number): Observable<Homework[]> {
		return this.http.get(`api/homeworks/classId/${classId}`)
						.map(response =>
							response.status === 200
								? response.json() as Homework[]
								: null)
						.catch(this.handleError);
	}

	getHomework(classId: number, studentId: number): Observable<Homework> {
		return this.http.get(`api/homeworks/classId/${classId}/studentId/${studentId}`)
						.map(response =>
							response.status === 200
								? response.json() as Homework
								: null)
			.catch(this.handleError);
	}

	addComment(comment: Comment): Observable<Response> {
		return this.http.post(
			"api/comments",
			JSON.stringify(comment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.catch(this.handleError);
	}

	updateComment(comment: Comment): Observable<Response> {
		return this.http.put(
			`api/comments/${comment.id}`,
			JSON.stringify(comment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.catch(this.handleError);
	}

	deleteComment(id: number): Observable<Response> {
		return this.http.delete(`api/comments/${id}`)
						.catch(this.handleError);
	}
	
	deleteHomework(id: number): Observable<Response> {
		return this.http.delete(`api/homeworks/${id}`)
						.catch(this.handleError);
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
