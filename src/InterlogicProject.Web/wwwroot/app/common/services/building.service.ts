import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import { getHeaders } from "../functions";
import { Building } from "../models";

@Injectable()
export default class BuildingService {
	private buildings = "api/buildings";

	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getBuildings(): Observable<Building[]> {
		return this.http.get(this.buildings)
			.map(response => response.json() as Building[])
			.first();
	}

	getBuilding(id: number): Observable<Building> {
		return this.http.get(`${this.buildings}/${id}`)
			.map(response => response.json() as Building)
			.first();
	}

	addBuilding(building: Building): ConnectableObservable<Response> {
		const action = this.http.post(
			this.buildings,
			JSON.stringify(building),
			{ headers: getHeaders() })
			.first()
			.publish();

		action.subscribe(
			response => {
				const location = response.headers.get("Location");
				building.id = +location.substr(location.lastIndexOf("/") + 1);
			});

		return action;
	}

	updateBuilding(building: Building): ConnectableObservable<Response> {
		return this.http.put(
			`${this.buildings}/${building.id}`,
			JSON.stringify(building),
			{ headers: getHeaders() })
			.first()
			.publish();
	}

	deleteBuilding(id: number): ConnectableObservable<Response> {
		return this.http.delete(
			`${this.buildings}/${id}`,
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
