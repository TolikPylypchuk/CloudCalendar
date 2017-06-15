import { Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

export function getAuthToken(): string {
	const token = localStorage.getItem("ipAuthToken");
	return token ? token : null;
}

export function getHeaders(): Headers {
	return getAuthToken()
		? new Headers({
			"Content-Type": "application/json",
			"Authorization": `Bearer ${getAuthToken()}`
		})
		: new Headers({
			"Content-Type": "application/json"
		});
}

export function handleError(error: Response | any): ErrorObservable {
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
