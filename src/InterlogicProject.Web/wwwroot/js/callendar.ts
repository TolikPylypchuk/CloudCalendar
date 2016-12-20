declare var moment: any;

$(document).ready(() => {
	const classes = getClasses(
		moment().startOf("week"),
		moment().endOf("week"));

	const events = classes.map(classToEvent);

	$("#calendar").fullCalendar({
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275D8",
		events: events,
		weekends: false,
		weekNumbers: true
	});
	/*{
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275d8",
		eventLimit: 5,
		events: events,
		header: {
			left: "title",
			right: "today,month,agendaDay,agendaWeek prev,next"
		},
		height: "auto",
		minTime: "08:00:00",
		maxTime: "21:00:00",
		selectable: true,
		weekends: false,
		weekNumbers: true
	});*/
});

function getClasses(start: any, end: any): any[] {
	"use strict";

	const result: any[] = [];

	$.get({
		url: "http://localhost:8000/api/classes/range/" +
			 `${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
		async: false,
		success: (data: any) => {
			for (let item of data) {
				result.push(item);
			}
		},
		error: () => { return true; }
	});

	return result;
}

function eventClicked(): void {
	"use strict";

	$.get({
		url: "http://localhost:8000/api/users/current",
		async: true,
		success: (data: any) => {
			let displayData = "";

			for (let key in data) {
				if (data.hasOwnProperty(key)) {
					displayData += `${key}: ${data[key]}\n`;
				}
			}

			alert(`Current user:\n${displayData}`);
		}});
}

function classToEvent(classInfo: any): any {
	"use strict";

	return {
		title: `${classInfo.subjectName}: ${classInfo.type}`,
		start: classInfo.dateTime,
		end: moment.utc(classInfo.dateTime).add(1, "hours").add(20, "minutes").format()
	};
}
