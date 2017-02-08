﻿import { Component, Input, OnInit } from "@angular/core";
import { Http } from "@angular/http";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs/Subscription";
import * as moment from "moment";

import ModalContentComponent from "./modal-content.component";

import { Student, Class } from "../../common/models";

import { StudentService } from "../common/common";

@Component({
	selector: "student-calendar",
	template: `
		<div class="m-3 pb-3">
			<angular2-fullcalendar [options]="options">
			</angular2-fullcalendar>
		
			<template ngbModalContainer></template>
		</div>
	`
})
export default class CalendarComponent {
	options: FC.Options;

	private http: Http;
	private modalService: NgbModal;
	private studentService: StudentService;

	private currentSubscription: Subscription = null;
	
	constructor(
		http: Http,
		modalService: NgbModal,
		studentService: StudentService) {
		this.http = http;
		this.modalService = modalService;
		this.studentService = studentService;

		this.options = {
			allDaySlot: false,
			columnFormat: "dd, DD.MM",
			defaultView: "agendaWeek",
			eventClick: this.eventClicked.bind(this),
			eventColor: "#0275D8",
			eventDurationEditable: false,
			events: this.getEvents.bind(this),
			header: {
				left: "title",
				center: "agendaWeek,listWeek",
				right: "today,prev,next"
			},
			height: "auto" as any,
			minTime: moment.duration("08:00:00"),
			maxTime: moment.duration("21:00:00"),
			slotDuration: moment.duration("00:30:00"),
			slotLabelFormat: "HH:mm",
			slotLabelInterval: moment.duration("01:00:00"),
			titleFormat: "DD MMM YYYY",
			weekends: false,
			weekNumbers: true,
			weekNumberTitle: "Тиж "
		};
	}
	
	private getEvents(
		start: moment.Moment,
		end: moment.Moment,
		timezone: string | boolean,
		callback: (data: FC.EventObject[]) => void): void {

		if (this.currentSubscription !== null) {
			this.currentSubscription.unsubscribe();
		}
		
		this.currentSubscription = this.studentService.getCurrentGroup()
			.subscribe(group => {
				if (group) {
					const request = this.http.get(
						`/api/classes/groupId/${group.id}` +
						`/range/${start.format("YYYY-MM-DD")}` +
						`/${end.format("YYYY-MM-DD")}`);

					request.map(response => response.json())
						.subscribe(data => {
							const classes = data as Class[];
							callback(classes.map(this.classToEvent));
						});
				}
			});
	}

	private eventClicked(event: FC.EventObject): void {
		this.modalService.open(ModalContentComponent);
	}

	private classToEvent(classInfo: Class): FC.EventObject {
		return {
			id: classInfo.id,
			title: `${classInfo.subjectName}: ${classInfo.type}`,
			start: moment.utc(classInfo.dateTime).format(),
			end: moment.utc(classInfo.dateTime)
					   .add(1, "hours")
					   .add(20, "minutes")
					   .format()
		};
	}
}