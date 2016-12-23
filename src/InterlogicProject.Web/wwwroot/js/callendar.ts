require(["../lib/moment/moment"], () => {
	if (document.readyState !== "complete") {
		$(document).ready(() => {
			initCallendar();
		});
	} else {
		initCallendar();
	}
});

import * as models from "./models";
import * as moment from "../lib/moment/moment";

function initCallendar(): void {
	"use strict";

	$("#calendar").fullCalendar({
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275D8",
		events: getEvents,
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
	"use strict";

	$.get({
		url: "http://localhost:8000/api/classes/range/" +
		`${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
		success: (data: models.Class[]) => {
			callback(data.map(classToEvent));
		}
	});
}

function eventClicked(event: FC.EventObject): void {
	"use strict";

	$.get({
		url: `http://localhost:8000/api/classes/id/${event.id}`,
		success: (data: models.Class) => {
			$("#classTitle").text(data.subjectName);
			$("#classType").text(data.type);
			$("#classTime").text(moment.utc(data.dateTime)
				.format("DD.MM.YYYY, dddd, HH:mm"));

			$("#classInfoModal").modal("show");
		}});
}

function classToEvent(classInfo: models.Class): FC.EventObject {
	"use strict";

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
