import { Injectable } from "@angular/core";
import { Http, Headers } from "@angular/http";
import { Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { ReplaySubject } from "rxjs/ReplaySubject";

import { User } from "../../common/models";

import { LoginModel } from "../models/models";

@Injectable()
export class AccountService {
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

		return this.getEmailDomain()
			.concatMap(domain =>
				this.http.post(
					this.authUrl,
					JSON.stringify({
						username: `${model.username}@${domain}`,
						password: model.password
					}),
					{ headers: this.getHeaders() })
					.map(response => {
						const token = response.json() && response.json().token;

						if (token) {
							if (localStorage) {
								localStorage.setItem("ipAuthToken", token);
							}
							this.loggedIn = true;

							this.http.get(
								this.currentUserUrl,
								{
									headers: this.getHeaders()
								})
								.map(r =>
									r.status === 200
										? r.json() as User
										: null)
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
					}))
			.first();
	}

	logout(): void {
		if (localStorage) {
			localStorage.removeItem("ipAuthToken");
		}

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

	isStudent(): Observable<boolean> {
		return this.isCurrentUserInRole("student");
	}

	isLecturer(): Observable<boolean> {
		return this.isCurrentUserInRole("lecturer");
	}

	isAdmin(): Observable<boolean> {
		return this.isCurrentUserInRole("admin");
	}

	getEmailDomain(): Observable<string> {
		return this.http.get(
			"api/config/email-domain",
			{ headers: this.getHeaders() })
			.map(response => response.text())
			.first();
	}

	getToken(): string {
		const token = localStorage ? localStorage.getItem("ipAuthToken") : null;
		return token ? token : null;
	}

	getHeaders(): Headers {
		return this.isLoggedIn()
			? new Headers({
				"Content-Type": "application/json",
				"Authorization": `Bearer ${this.getToken()}`
			})
			: new Headers({ "Content-Type": "application/json" });
	}

	private isCurrentUserInRole(role: string): Observable<boolean> {
		return this.currentUserSource.asObservable()
			.map(user => user.roles && !!user.roles.find(
				r => r.toLowerCase() === role.toLowerCase()));
	}
}
