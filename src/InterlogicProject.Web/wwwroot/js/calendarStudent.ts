/// <amd-module name="calendarStudent" />

"use strict";

import * as models from "./models";
import * as moment from "moment";

const currentStudentId = $("#calendarScript").data("student-id") as number;
let currentStudent: models.Student;

if (document.readyState !== "complete") {
	$(document).ready(init);
} else {
	init();
}

function init(): void {
	moment.locale("uk");

	$.get({
		url: `http://localhost:8000/api/students/id/${currentStudentId}`,
		success: (student: models.Student) => {
			currentStudent = student;
			initCallendar();
		}
	});
}

function initCallendar(): void {
	$("#calendar").fullCalendar({
		allDaySlot: false,
		columnFormat: "dd, DD.MM",
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275D8",
		events: getEvents,
		header: {
			left: "title",
			center: "agendaWeek,listWeek",
			right: "today,prev,next"
		},
		minTime: moment.duration("08:00:00"),
		maxTime: moment.duration("21:00:00"),
		slotDuration: moment.duration("00:30:00"),
		slotLabelFormat: "HH:mm",
		slotLabelInterval: moment.duration("01:00:00"),
		titleFormat: "DD MMM YYYY",
		weekends: false,
		weekNumbers: true,
		weekNumberTitle: "Тиж "
	});

	$("#calendar").fullCalendar("option", "height", "auto");
}

function getEvents(
	start: moment.Moment,
	end: moment.Moment,
	timezone: string | boolean,
	callback: (data: FC.EventObject[]) => void): void {

	$.get({
		url: "http://localhost:8000/api/classes/" +
			 `groupId/${currentStudent.groupId}/` +
			 `range/${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
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
			const time = moment.utc(data.dateTime).format("DD.MM.YYYY, dddd, HH:mm");
			$("#classTime").text(time);

			$("#classInfoModal").modal("show");
		}});
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
