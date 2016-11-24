$(document).ready(function () {
	$("#calendar").fullCalendar({
		header: {
			left: "title",
			right: "today,month,agendaDay,agendaWeek prev,next"
		},
		weekends: false,
		defaultView: "timelineWeek",
		events: [
			{
				title: "Hello world!",
				start: "2016-11-25"
			}
		]
	});
});
