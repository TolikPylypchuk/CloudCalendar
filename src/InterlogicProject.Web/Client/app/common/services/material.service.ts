import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Material } from "../models";

@Injectable()
export class HomeworkService {
	private materials = "/api/materials";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getMaterials(): Observable<Material[]> {
		return this.http.get(
			this.materials,
			{ headers: getHeaders() })
			.map(response => response.json() as Material[])
			.first();
	}

	getMaterial(id: number): Observable<Material> {
		return this.http.get(
			`${this.materials}/${id}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Material)
			.first();
	}

	getMaterialsByClass(classId: number): Observable<Material[]> {
		return this.http.get(
			`${this.materials}/classId/${classId}`,
			{ headers: getHeaders() })
			.map(response => response.json() as Material[])
			.first();
	}
	
	updateMaterial(material: Material): ConnectableObservable<Response> {
		return this.http.put(
			`${this.materials}/${material.id }`,
			JSON.stringify(material),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteMaterial(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.materials}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
