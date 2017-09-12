import { Headers } from "@angular/http";

import { NameableEntity, GenericUser } from "./models";

export function getAuthToken(): string {
	const token = localStorage ? localStorage.getItem("ipAuthToken") : null;
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
	let result: number;

	result = a.firstName.localeCompare(b.firstName);

	if (result === 0) {
		result = a.middleName.localeCompare(b.middleName);

		if (result === 0) {
			result = a.lastName.localeCompare(b.lastName);
		}
	}

	return result;
}

export function compareByLastName<T>(
	a: GenericUser<T>,
	b: GenericUser<T>): number {
	let result: number;

	result = a.lastName.localeCompare(b.lastName);

	if (result === 0) {
		result = a.firstName.localeCompare(b.firstName);

		if (result === 0) {
			result = a.middleName.localeCompare(b.middleName);
		}
	}

	return result;
}

export function getUserInitials<T>(user: GenericUser<T>): string {
	return `${user.lastName} ${user.firstName[0]}. ${user.lastName[0]}.`;
}
