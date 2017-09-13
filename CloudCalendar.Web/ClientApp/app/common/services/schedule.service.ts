import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { ConnectableObservable } from "rxjs/Observable/ConnectableObservable";

import * as moment from "moment";

import { getHeaders } from "../functions";

@Injectable()
export class ScheduleService {
	private schedule = "/api/schedule";

	private http: Http;
	
	constructor(http: Http) {
		this.http = http;
	}

	createSchedule(
		start: moment.Moment, end: moment.Moment): ConnectableObservable<Response> {
		const formatString = "YYYY-MM-DDT00:00:00";
		return this.http.post(
			`${this.schedule}/create/range/${start.format(formatString)}/${end.format(formatString)}`,
			{ },
			{ headers: getHeaders() })
			.first()
			.publish();
	}
}
