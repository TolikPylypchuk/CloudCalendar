﻿/// <amd-module name="callendarLecturer" />

"use strict";

import * as models from "./models";
import * as moment from "../lib/moment/moment";

var currentLecturerId = $("#callendarScript").data("lecturer-id") as number;
var currentLecturer: models.Lecturer;

if (document.readyState !== "complete") {
	$(document).ready(() => {
		init();
	});
} else {
	init();
}

function init(): void {
	$.get({
		url: `http://localhost:8000/api/lecturers/id/${currentLecturerId}`,
		success: (lecturer: models.Lecturer) => {
			currentLecturer = lecturer;
			initCallendar();
		}
	});
}

function initCallendar(): void {
	$("#calendar").fullCalendar({
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275D8",
		events: getEvents,
		header: {
			right: "today,prev,next",
			left: "title",
			center: ""
		},
		minTime: moment.duration("08:00:00"),
		maxTime: moment.duration("21:00:00"),
		weekends: false,
		weekNumbers: true
	});

	$("#calendar").fullCalendar("option", "height", "auto");
}

function getEvents(
	start: moment.Moment,
	end: moment.Moment,
	timezone: string | boolean,
	callback: (data: FC.EventObject[]) => void): void {

	$.get({
		url: "http://localhost:8000/api/classes/lecturerId/" +
			 `${currentLecturerId}/range/` +
			 `${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
		success: (data: models.Class[]) => {
			callback(data.map(classToEvent));
		}
	});
}

function eventClicked(event: FC.EventObject): void {
	$.get({
		url: `http://localhost:8000/api/classes/id/${event.id}`,
		success: (data: models.Class) => {
			$("#classTitle").text(data.subjectName);
			$("#classType").text(data.type);
			$("#classTime").text(moment.utc(data.dateTime)
				.format("DD.MM.YYYY, dddd, HH:mm"));

			$("#classInfoModal").modal("show");
		}
	});
}

function classToEvent(classInfo: models.Class): FC.EventObject {
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
