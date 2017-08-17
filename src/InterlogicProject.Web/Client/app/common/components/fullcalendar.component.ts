import { Component, Input, AfterViewInit, ElementRef } from "@angular/core";

import * as $ from "jquery";

import "fullcalendar";

@Component({
	selector: "ip-fullcalendar",
	template: ""
})
export class FullcalendarComponent implements AfterViewInit {
	@Input() options: FC.Options;
	text: string;
	calendarInitiated: boolean;

	private element: ElementRef;

	constructor(element: ElementRef) {
		this.element = element;
	}
	
	ngAfterViewInit() {
		setTimeout(() => ($ as any)("ip-fullcalendar").fullCalendar(this.options), 100);
	}
	
	fullCalendar(...args: any[]): void {
		if (!args) {
			return;
		}

		switch (args.length) {
			case 1:
				($ as any)(this.element.nativeElement)
					.fullCalendar(args[0]);
				break;
			case 2:
				($ as any)(this.element.nativeElement)
					.fullCalendar(args[0], args[1]);
				break;
			case 3:
				($ as any)(this.element.nativeElement)
					.fullCalendar(args[0], args[1], args[2]);
				break;
		}
	}

	updateEvent(event) {
		return ($ as any)(this.element.nativeElement)
			.fullCalendar("updateEvent", event);
	}

	clientEvents(idOrFilter) {
		return ($ as any)(this.element.nativeElement)
			.fullCalendar("clientEvents", idOrFilter);
	}
}
