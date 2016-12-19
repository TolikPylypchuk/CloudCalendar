declare var $: any;

$(document).ready(() => {
	$("#calendar").fullCalendar({
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: () => {
			$.get(
				"http://localhost:8000/api/users/current",
				(data: any) => {
					let displayData = "";

					for (let key in data) {
						if (data.hasOwnProperty(key)) {
							displayData += `${key}: ${data[key]}\n`;
						}
					}

					alert(`Current user:\n${displayData}`);
				});
        },
		eventColor: "#0275d8",
		eventLimit: 5,
		events: [
			{
				title: "Подія 1",
				start: "2016-12-19T10:00",
				end: "2016-12-19T12:00"
			},
			{
				title: "Подія 2",
				start: "2016-12-20T12:00",
				end: "2016-12-20T13:30"
			},
			{
				title: "Подія 3",
				start: "2016-12-20T15:00",
				end: "2016-12-20T17:00"
			}
		],
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
	});
});
