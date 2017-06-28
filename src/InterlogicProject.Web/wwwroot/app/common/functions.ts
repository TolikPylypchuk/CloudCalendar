import { Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

export function getAuthToken(): string {
	const token = localStorage.getItem("ipAuthToken");
	return token ? token : null;
}

export function getHeaders(): Headers {
	const token = getAuthToken();

	return token
		? new Headers({
			"Content-Type": "application/json",
			"Authorization": `Bearer ${token}`
		})
		: new Headers({
			"Content-Type": "application/json"
		});
}
