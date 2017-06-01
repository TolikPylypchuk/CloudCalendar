import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";

import { handleError } from "../functions";

import {
	Class, Classroom, Group, Lecturer, Comment, Material, Homework
} from "../models";

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
			.catch(handleError)
			.first();
	}

	getPlaces(classId: number): Observable<Classroom[]> {
		return this.http.get(`api/classrooms/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Classroom[]
					: null)
			.catch(handleError)
			.first();
	}

	getGroups(classId: number): Observable<Group[]> {
		return this.http.get(`api/groups/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Group[]
					: null)
			.catch(handleError)
			.first();
	}

	getLecturers(classId: number): Observable<Lecturer[]> {
		return this.http.get(`api/lecturers/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Lecturer[]
					: null)
			.catch(handleError)
			.first();
	}

	getComments(classId: number): Observable<Comment[]> {
		return this.http.get(`api/comments/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Comment[]
					: null)
			.catch(handleError)
			.first();
	}

	getMaterials(classId: number): Observable<Material[]> {
		return this.http.get(`api/materials/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Material[]
					: null)
			.catch(handleError)
			.first();
	}
	
	getHomeworks(classId: number): Observable<Homework[]> {
		return this.http.get(`api/homeworks/classId/${classId}`)
			.map(response =>
				response.status === 200
					? response.json() as Homework[]
					: null)
			.catch(handleError)
			.first();
	}

	getHomework(classId: number, studentId: number): Observable<Homework> {
		return this.http.get(`api/homeworks/classId/${classId}/studentId/${studentId}`)
			.map(response =>
				response.status === 200
					? response.json() as Homework
					: null)
			.catch(handleError)
			.first();
	}

	updateClass(c: Class): Observable<Response> {
		return this.http.put(
			`api/classes/${c.id}`,
			JSON.stringify(c),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.catch(handleError)
			.first();
	}

	addComment(comment: Comment): Observable<Response> {
		return this.http.post(
			"api/comments",
			JSON.stringify(comment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.catch(handleError)
			.first();
	}

	updateComment(comment: Comment): Observable<Response> {
		return this.http.put(
			`api/comments/${comment.id}`,
			JSON.stringify(comment),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.catch(handleError)
			.first();
	}

	deleteComment(id: number): Observable<Response> {
		return this.http.delete(`api/comments/${id}`)
			.catch(handleError)
			.first();
	}

	deleteMaterial(id: number): Observable<Response> {
		return this.http.delete(`api/materials/${id}`)
			.catch(handleError)
			.first();
	}

	updateHomework(homework: Homework): Observable<Response> {
		return this.http.put(
			`api/homeworks/${homework.id}`,
			JSON.stringify(homework),
			{
				headers: new Headers({ "Content-Type": "application/json" })
			})
			.catch(handleError)
			.first();
	}
	
	deleteHomework(id: number): Observable<Response> {
		return this.http.delete(`api/homeworks/${id}`)
			.catch(handleError)
			.first();
	}
}
