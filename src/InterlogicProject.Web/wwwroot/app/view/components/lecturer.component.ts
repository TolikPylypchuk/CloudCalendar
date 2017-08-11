import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";

import * as moment from "moment";

import { ClassService, LecturerService } from "../../common/common";
import { Class, Lecturer } from "../../common/models";

@Component({
	selector: "ip-view-lecturer",
	templateUrl: "/templates/view/lecturer"
})
export default class LecturerComponent implements OnInit {
	options: FC.Options;
	lecturerId: number;
	lecturer: Lecturer;

	private router: Router;
	private route: ActivatedRoute;

	private classService: ClassService;
	private lecturerService: LecturerService;

	constructor(
		router: Router,
		route: ActivatedRoute,
		classService: ClassService,
		lecturerService: LecturerService) {
		this.router = router;
		this.route = route;
		this.classService = classService;
		this.lecturerService = lecturerService;
	}

	ngOnInit(): void {
		this.route.params.subscribe((params: Params) => {
			this.lecturerId = +params["id"];

			if (!this.lecturerId) {
				this.router.navigate([ "view/lecturers" ]);
			}

			this.lecturerService.getLecturer(this.lecturerId)
				.subscribe(lecturer => this.lecturer = lecturer);

			const paramDate = params["date"];

			let date: moment.Moment;

			if (paramDate) {
				date = moment(params["date"], "YYYY-MM-DD", true);

				if (!date.isValid()) {
					date = moment();
					this.router.navigate([ "view/lecturer", this.lecturerId ]);
				}
			} else {
				date = moment();
			}

			this.options = {
				allDaySlot: false,
				columnFormat: "dd, DD.MM",
				defaultDate: date,
				defaultView: "agendaWeek",
				eventDurationEditable: false,
				eventRender: (event: FC.EventObject, element: JQuery) => {
					element.css("border-style", "hidden");
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

	private getEvents(
		start: moment.Moment,
		end: moment.Moment,
		timezone: string | boolean,
		callback: (data: FC.EventObject[]) => void): void {

		this.classService.getClassesForLecturerInRange(
			this.lecturerId, start, end)
			.subscribe(classes =>
				callback(classes.map(this.classToEvent)));
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
