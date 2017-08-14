import { Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/Observable/ErrorObservable";

import { NameableEntity, GenericUser } from "./models";

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

export function compareByName(a: NameableEntity, b: NameableEntity): number {
	return a.name.localeCompare(b.name);
}

export function compareByFirstName<T>(
	a: GenericUser<T>,
	b: GenericUser<T>): number {
	let result = 0;

	result = a.firstName.localeCompare(b.firstName);

	if (result == 0) {
		result = a.middleName.localeCompare(b.middleName);

		if (result == 0) {
			result = a.lastName.localeCompare(b.lastName);
		}
	}

	return result;
}

export function compareByLastName<T>(
	a: GenericUser<T>,
	b: GenericUser<T>): number {
	let result = 0;

	result = a.lastName.localeCompare(b.lastName);

	if (result == 0) {
		result = a.firstName.localeCompare(b.firstName);

		if (result == 0) {
			result = a.middleName.localeCompare(b.middleName);
		}
	}

	return result;
}
