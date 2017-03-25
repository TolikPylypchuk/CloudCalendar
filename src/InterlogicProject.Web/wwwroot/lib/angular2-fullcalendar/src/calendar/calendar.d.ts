import { OnInit, AfterViewInit, AfterContentChecked, AfterViewChecked, ElementRef } from '@angular/core';
import 'fullcalendar';
import { Options } from "fullcalendar";
export declare class CalendarComponent implements OnInit, AfterViewInit, AfterContentChecked, AfterViewChecked {
    private element;
    options: Options;
    text: string;
    calendarInitiated: boolean;
    constructor(element: ElementRef);
    ngOnInit(): void;
    ngAfterViewInit(): void;
    ngAfterContentChecked(): void;
    ngAfterViewChecked(): void;
    fullCalendar(...args: any[]): void;
    updateEvent(event: any): void;
    clientEvents(idOrFilter: any): FC.EventObject[];
}
