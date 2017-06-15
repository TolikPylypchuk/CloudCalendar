import { Component, Input, AfterViewInit, ElementRef } from "@angular/core";

import * as $ from "jquery";

import "fullcalendar";
import { Options } from "fullcalendar";

@Component({
	selector: "ip-fullcalendar",
	template: ""
})
export default class FullcalendarComponent implements AfterViewInit {
	@Input() options: Options;
	text: string;
	calendarInitiated: boolean;

	private element: ElementRef;

	constructor(element: ElementRef) {
		this.element = element;
	}
	
	ngAfterViewInit() {
		setTimeout(() => $("ip-fullcalendar").fullCalendar(this.options), 100);
	}
	
	fullCalendar(...args: any[]) {
		if (!args) {
			return;
		}

		switch (args.length) {
			case 0:
				return;
			case 1:
				return $(this.element.nativeElement)
					.fullCalendar(args[0]);
			case 2:
				return $(this.element.nativeElement)
					.fullCalendar(args[0], args[1]);
			case 3:
				return $(this.element.nativeElement)
					.fullCalendar(args[0], args[1], args[2]);
		}
	}

	updateEvent(event) {
		return $(this.element.nativeElement)
			.fullCalendar("updateEvent", event);
	}

	clientEvents(idOrFilter) {
		return $(this.element.nativeElement)
			.fullCalendar("clientEvents", idOrFilter);
	}
}
