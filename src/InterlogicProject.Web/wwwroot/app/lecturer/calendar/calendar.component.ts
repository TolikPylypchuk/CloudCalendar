import { Component, OnInit, AfterViewInit } from "@angular/core";
import { Http } from "@angular/http";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import * as moment from "moment";

import ModalContentComponent from "./modal/modal-content.component";

import { ClassService, LecturerService } from "../../common/common";
import { Class } from "../../common/models";

@Component({
	selector: "ip-lecturer-calendar",
	templateUrl: "/templates/lecturer/calendar",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export default class CalendarComponent implements OnInit, AfterViewInit {
	options: FC.Options;

	private router: Router;
	private route: ActivatedRoute;
	private modalService: NgbModal;

	private classService: ClassService;
	private lecturerService: LecturerService;
	
	constructor(
		router: Router,
		route: ActivatedRoute,
		modalService: NgbModal,
		classService: ClassService,
		lecturerService: LecturerService) {
		this.router = router;
		this.route = route;
		this.modalService = modalService;

		this.classService = classService;
		this.lecturerService = lecturerService;
	}

	ngOnInit(): void {
		this.route.params.subscribe((params: Params) => {
			const paramDate = params["date"];

			let date: moment.Moment;

			if (paramDate) {
				date = moment(params["date"], "YYYY-MM-DD", true);

				if (!date.isValid()) {
					date = moment();
					this.router.navigate([ "lecturer/calendar" ]);
				}
			} else {
				date = moment();
			}

			this.options = {
				allDaySlot: false,
				columnFormat: "dd, DD.MM",
				defaultDate: date,
				defaultView: "agendaWeek",
				eventClick: this.eventClicked.bind(this),
				eventDurationEditable: false,
				eventRender: (event: FC.EventObject, element: JQuery) => {
					element.css("border-style", "hidden");
					element.css("cursor", "pointer");
					element.addClass("bg-primary");
					element.addClass("text-white");
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
		});
	}

	ngAfterViewInit(): void {
		this.route.params.subscribe((params: Params) => {
			const date = moment(params["date"], "YYYY-MM-DD", true);

			const paramTime = params["time"];

			if (paramTime) {
				const time = moment(
					paramTime,
					["HH:mm", "H:mm", "HH:m", "H:m"],
					true);

				const dateTime =
					moment(date.format("YYYY-MM-DD") + "T" +
						time.format("HH:mm"));

				if (time.isValid()) {
					this.lecturerService.getCurrentLecturer()
						.subscribe(lecturer =>
							this.classService.getClassForLecturerByDateTime(
								lecturer.id,
								dateTime)
								.subscribe(c => {
									if (c) {
										this.createModal(c.id);
									}
								}));
				}
			}
		});
	}
	
	private getEvents(
		start: moment.Moment,
		end: moment.Moment,
		timezone: string | boolean,
		callback: (data: FC.EventObject[]) => void): void {
		this.lecturerService.getCurrentLecturer()
			.subscribe(lecturer => {
				if (lecturer) {
					this.classService.getClassesForLecturerInRange(
						lecturer.id, start, end)
						.subscribe(classes =>
							callback(classes.map(this.classToEvent)));
				}
			});
	}

	private eventClicked(event: FC.EventObject): void {
		this.createModal(event.id);
	}

	private createModal(classId: number): void {
		const modalRef = this.modalService.open(ModalContentComponent);
		const modal = modalRef.componentInstance as ModalContentComponent;
		modal.classId = classId;
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
