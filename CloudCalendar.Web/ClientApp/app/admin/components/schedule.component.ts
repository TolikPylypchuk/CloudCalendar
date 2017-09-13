import { Component } from "@angular/core";
import { NgbDatepickerConfig, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

import * as moment from "moment";

import { ScheduleService } from "../../common/common";

@Component({
	selector: "admin-schedule",
	templateUrl: "./schedule.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class ScheduleComponent {

	private scheduleSerivce: ScheduleService;

	success = null as boolean;

	model = {
		start: null as NgbDateStruct,
		end: null as NgbDateStruct
	};

	dateStart: { year: number, month: number };
	dateEnd: { year: number, month: number };

	constructor(
		config: NgbDatepickerConfig,
		scheduleSerivce: ScheduleService) {
		this.scheduleSerivce = scheduleSerivce;

		config.minDate = { year: 1900, month: 1, day: 1 };
		config.maxDate = { year: 2099, month: 12, day: 31 };

		config.markDisabled = (date: NgbDateStruct) => {
			const m = moment(new Date(date.year, date.month - 1, date.day));
			return m.day() === 0 || m.day() === 6;
		};
	}

	submit(): void {
		if (this.model.start === null || this.model.end === null) {
			return;
		}

		const start = moment(new Date(
			this.model.start.year,
			this.model.start.month - 1,
			this.model.start.day));

		const end = moment(new Date(
			this.model.end.year,
			this.model.end.month - 1,
			this.model.end.day));

		const action = this.scheduleSerivce.createSchedule(start, end);

		action.subscribe(
			response => {
				if (response.status === 201) {
					this.success = true;
				}
			},
			() => this.success = false);

		action.connect();
	}

	closeAlert(): void {
		this.success = null;
	}
}
