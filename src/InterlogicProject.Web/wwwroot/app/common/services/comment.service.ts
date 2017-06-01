import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Comment } from "../models";

@Injectable()
export default class CommentService {
	private comments = "api/comments";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getComments(): Observable<Comment[]> {
		return this.http.get(this.comments)
			.map(response => response.json() as Comment[])
			.first();
	}

	getComment(id: number): Observable<Comment> {
		return this.http.get(`${this.comments}/${id}`)
			.map(response => response.json() as Comment)
			.first();
	}

	getByClass(classId: number): Observable<Comment[]> {
		return this.http.get(`${this.comments}/classId/${classId}`)
			.map(response => response.json() as Comment[])
			.first();
	}

	getSomeByClass(classId: number, take: number): Observable<Comment[]> {
		return this.http.get(`${this.comments}/classId/${classId}/take/${take}`)
			.map(response => response.json() as Comment[])
			.first();
	}

	getByUser(userId: number): Observable<Comment[]> {
		return this.http.get(`${this.comments}/userId/${userId}`)
			.map(response => response.json() as Comment[])
			.first();
	}

	getByClassAndUser(classId: number, userId: number): Observable<Comment[]> {
		return this.http.get(
			`${this.comments}/classId/${classId}/userId/${userId}`)
			.map(response => response.json() as Comment[])
			.first();
	}

	getSomeByClassAndUser(
		classId: number,
		userId: number,
		take: number): Observable<Comment[]> {
		return this.http.get(
			`${this.comments}/classId/${classId}/userId/${userId}/take/${take}`)
			.map(response => response.json() as Comment[])
			.first();
	}

	addComment(comment: Comment): ConnectableObservable<Response> {
		const action = this.http.post(
			this.comments,
			JSON.stringify(comment),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				comment.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateComment(comment: Comment): ConnectableObservable<Response> {
		return this.http.put(
			`${this.comments}/${comment.id}`,
			JSON.stringify(comment),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteComment(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.comments}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
