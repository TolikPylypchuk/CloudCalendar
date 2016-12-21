declare var moment: any;

$(document).ready(() => {
	$("#calendar").fullCalendar({
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275D8",
		events: getEvents,
		weekends: false,
		weekNumbers: true
	});

	$("#calendar").fullCalendar("option", "height", "auto");
	$("#calendar").fullCalendar("option", "minTime", "08:00:00");
	$("#calendar").fullCalendar("option", "maxTime", "21:00:00");
});

function getEvents(start: any, end: any, timezone: any, callback: any): void {
	"use strict";

	$.get({
		url: "http://localhost:8000/api/classes/range/" +
			 `${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
		success: (data: any) => {
			const classes: any[] = [];

			for (let item of data) {
				classes.push(item);
			}

			callback(classes.map(classToEvent));
		}
	});
}

function eventClicked(event: any): void {
	"use strict";

	$.get({
		url: `http://localhost:8000/api/classes/id/${event.id}`,
		async: true,
		success: (data: any) => {
			let displayData = "";

			for (let key in data) {
				if (data.hasOwnProperty(key)) {
					displayData += `${key}: ${data[key]}\n`;
				}
			}

			alert(displayData);
		}});
}

function classToEvent(classDto: any): any {
	"use strict";

	return {
		id: classDto.id,
		title: `${classDto.subjectName}: ${classDto.type}`,
		start: moment.utc(classDto.dateTime),
		end: moment.utc(classDto.dateTime)
				   .add(1, "hours")
				   .add(20, "minutes")
				   .format()
	};
}
