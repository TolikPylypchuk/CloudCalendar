$(document).ready(function () {
    var classes = getClasses(moment().startOf("week"), moment().endOf("week"));
    var events = classes.map(classToEvent);
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
function getClasses(start, end) {
    "use strict";
    var result = [];
    $.get({
        url: "http://localhost:8000/api/classes/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")),
        async: false,
        success: function (data) {
            for (var _i = 0, data_1 = data; _i < data_1.length; _i++) {
                var item = data_1[_i];
                result.push(item);
            }
        },
        error: function () { return true; }
    });
    return result;
}
function eventClicked() {
    "use strict";
    $.get({
        url: "http://localhost:8000/api/users/current",
        async: true,
        success: function (data) {
            var displayData = "";
            for (var key in data) {
                if (data.hasOwnProperty(key)) {
                    displayData += key + ": " + data[key] + "\n";
                }
            }
            alert("Current user:\n" + displayData);
        }
    });
}
function classToEvent(classInfo) {
    "use strict";
    return {
        title: classInfo.subjectName + ": " + classInfo.type,
        start: classInfo.dateTime,
        end: moment.utc(classInfo.dateTime).add(1, "hours").add(20, "minutes").format()
    };
}
//# sourceMappingURL=callendar.js.map