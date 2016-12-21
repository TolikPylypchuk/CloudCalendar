$(document).ready(function () {
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
function getEvents(start, end, timezone, callback) {
    "use strict";
    $.get({
        url: "http://localhost:8000/api/classes/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")),
        success: function (data) {
            var classes = [];
            for (var _i = 0, data_1 = data; _i < data_1.length; _i++) {
                var item = data_1[_i];
                classes.push(item);
            }
            callback(classes.map(classToEvent));
        }
    });
}
function eventClicked(event) {
    "use strict";
    $.get({
        url: "http://localhost:8000/api/classes/id/" + event.id,
        async: true,
        success: function (data) {
            var displayData = "";
            for (var key in data) {
                if (data.hasOwnProperty(key)) {
                    displayData += key + ": " + data[key] + "\n";
                }
            }
            alert(displayData);
        }
    });
}
function classToEvent(classDto) {
    "use strict";
    return {
        id: classDto.id,
        title: classDto.subjectName + ": " + classDto.type,
        start: moment.utc(classDto.dateTime),
        end: moment.utc(classDto.dateTime)
            .add(1, "hours")
            .add(20, "minutes")
            .format()
    };
}
//# sourceMappingURL=callendar.js.map