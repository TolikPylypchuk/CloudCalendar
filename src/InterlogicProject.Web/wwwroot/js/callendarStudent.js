$(document).ready(function () {
	$("#calendar").fullCalendar({
		defaultView: "agendaWeek",
		eventLimit: 5,
		events: [
			{
				title: "Hello world!",
				start: "2016-11-25T10:10",
				end: "2016-11-25T11:30"
			}
		],
		header: {
			left: "title",
			right: "today,month,agendaDay,agendaWeek prev,next"
		},
		height: "auto",
		minTime: "08:00:00",
		maxTime: "21:00:00",
		weekends: false,
		weekNumbers: true
	});
});