import { Component, Input, AfterViewInit, ElementRef } from "@angular/core";

import * as $ from "jquery";

import "fullcalendar";

@Component({
	selector: "fullcalendar",
	template: "",
	styleUrls: [ "../../../css/style.css" ]
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
		setTimeout(() => {
			const element = ($ as any)("fullcalendar");

			if (element) {
				element.fullCalendar(this.options);
			}
		}, 100);
	}
	
	fullCalendar(...args: any[]): void {
		if (!args) {
			return;
		}

		const element = ($ as any)(this.element.nativeElement);

		if (element) {
			switch (args.length) {
			case 1:
				element.fullCalendar(args[0]);
				break;
			case 2:
				element.fullCalendar(args[0], args[1]);
				break;
			case 3:
				element.fullCalendar(args[0], args[1], args[2]);
				break;
			}
		}
	}

	updateEvent(event: FC.EventObject): any {
		const element = ($ as any)(this.element.nativeElement);

		return element
			? element.fullCalendar("updateEvent", event)
			: null;
	}

	clientEvents(idOrFilter: any): any {
		const element = ($ as any)(this.element.nativeElement);

		return element
			? element.fullCalendar("clientEvents", idOrFilter)
			: null;
	}
}
