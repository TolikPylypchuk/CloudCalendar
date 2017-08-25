import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";

import { getHeaders } from "../functions";
import { ScheduleOptions } from "../models";

@Injectable()
export class ConfigService {
	private http: Http;

	constructor(http: Http) {
		this.http = http;
	}

	getScheduleOptions(): Observable<ScheduleOptions> {
		return this.http.get(
			"api/config/schedule",
			{ headers: getHeaders() })
			.map(response => response.json() as ScheduleOptions)
			.first();
	}
}
