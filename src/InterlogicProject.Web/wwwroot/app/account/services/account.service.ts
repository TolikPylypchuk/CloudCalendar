import { Injectable } from "@angular/core";
import { Http, Headers, Response } from "@angular/http";
import { Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { ReplaySubject } from "rxjs/ReplaySubject";

import { handleError } from "../../common/functions";
import { User } from "../../common/models";

import { LoginModel } from "../models/models";

declare const localStorage;

@Injectable()
export default class AccountService {
	private authUrl = "/api/token";
	private currentUserUrl = "/api/users/current";

	private http: Http;
	private router: Router;

	private currentUserSource = new ReplaySubject<User>();

	private loggedIn = false;
	private loggingIn = false;
	private returnUrl: string = null;

	constructor(http: Http, router: Router) {
		this.http = http;
		this.router = router;

		const token = this.getToken();

		if (token) {
			this.loggedIn = true;
			this.http.get(
				this.currentUserUrl,
				{
					headers: this.getHeaders()
				})
				.map(response =>
					response.status === 200
						? response.json() as User
						: null)
				.catch(handleError)
				.first()
				.subscribe((user: User) => this.currentUserSource.next(user));
		}
	}

	getCurrentUser(): Observable<User> {
		return this.currentUserSource.asObservable().first();
	}

	getReturnUrl(): string {
		return this.returnUrl;
	}

	setReturnUrl(returnUrl: string): void {
		this.returnUrl = returnUrl;
	}

	login(model: LoginModel): Observable<boolean> {
		if (this.loggedIn) {
			return Observable.of(null);
		}

		return this.http.post(
			this.authUrl,
			JSON.stringify(model),
			{ headers: this.getHeaders() })
			.map((response: Response) => {
				const token = response.json() && response.json().token;

				if (token) {
					localStorage.setItem("ipAuthToken", token);
					this.loggedIn = true;

					this.http.get(
						this.currentUserUrl,
						{
							headers: this.getHeaders()
						})
						.map(response =>
							response.status === 200
								? response.json() as User
								: null)
						.catch(handleError)
						.subscribe((user: User) => {
							this.currentUserSource.next(user);
							this.router.navigate(
								[ this.returnUrl ? this.returnUrl : "" ]);
							this.setReturnUrl("");
						});

					this.loggingIn = false;
					return true;
				}

				return false;
			})
			.catch(handleError)
			.first();
	}

	logout(): void {
		localStorage.removeItem("ipAuthToken");
		this.currentUserSource = new ReplaySubject<User>();
		this.loggedIn = false;
		this.router.navigate([ "" ]);
	}

	isLoggedIn(): boolean {
		return this.loggedIn;
	}

	isLoggingIn(): boolean {
		return this.loggingIn;
	}

	setLoggingIn(value: boolean): void {
		this.loggingIn = value;
	}
	
	getToken(): string {
		const token = localStorage.getItem("ipAuthToken");
		return token ? JSON.parse(token).token : null;
	}

	getHeaders(): Headers {
		return this.isLoggedIn()
			? new Headers({
				"Content-Type": "application/json",
				"Authorization": `Bearer ${this.getToken()}`
			})
			: new Headers({ "Content-Type": "application/json" });
	}
}
