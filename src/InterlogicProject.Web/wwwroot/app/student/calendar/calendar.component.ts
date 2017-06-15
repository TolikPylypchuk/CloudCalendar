import { Component } from "@angular/core";
import { Http } from "@angular/http";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs/Subscription";

import * as moment from "moment";

import {
	ClassService, GroupService, StudentService
} from "../../common/common";
import { Class } from "../../common/models";

import ModalContentComponent from "./modal/modal-content.component";

@Component({
	selector: "ip-student-calendar",
	templateUrl: "templates/student/calendar",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class CalendarComponent {
	options: FC.Options;

	private modalService: NgbModal;

	private classService: ClassService;
	private groupService: GroupService;
	private studentService: StudentService;
	
	constructor(
		modalService: NgbModal,
		classService: ClassService,
		groupService: GroupService,
		studentService: StudentService) {
		this.modalService = modalService;
		this.classService = classService;
		this.groupService = groupService;
		this.studentService = studentService;

		this.options = {
			allDaySlot: false,
			columnFormat: "dd, DD.MM",
			defaultView: "agendaWeek",
			eventClick: this.eventClicked.bind(this),
			eventBackgroundColor: "#0275D8",
			eventBorderColor: "#0275D8",
			eventDurationEditable: false,
			eventRender: (event: FC.EventObject, element: JQuery) => {
				element.css("cursor", "pointer");
			},
			events: this.getEvents.bind(this),
			header: {
				left: "title",
				center: "agendaWeek,listWeek",
				right: "today prev,next"
			},
			height: "auto",
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

		this.studentService.getCurrentStudent()
			.subscribe(student =>
				this.groupService.getGroup(student.groupId)
					.subscribe(group => {
						if (group) {
							this.classService.getClassesForGroupInRange(
								group.id, start, end)
								.subscribe(data => {
									const classes = data as Class[];
									callback(classes.map(this.classToEvent));
								});
						}
					}));
	}

	private eventClicked(event: FC.EventObject): void {
		const modalRef = this.modalService.open(ModalContentComponent);
		const modal = modalRef.componentInstance as ModalContentComponent;
		modal.classId = event.id;
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
