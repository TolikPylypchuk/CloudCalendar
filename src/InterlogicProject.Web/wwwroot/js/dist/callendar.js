require(["../lib/moment/moment"], function () {
    if (document.readyState !== "complete") {
        $(document).ready(function () {
            initCallendar();
        });
    }
    else {
        initCallendar();
    }
});
var moment = require("../lib/moment/moment");
function initCallendar() {
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
function getEvents(start, end, timezone, callback) {
    "use strict";
    $.get({
        url: "http://localhost:8000/api/classes/range/" +
            (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")),
        success: function (data) {
            callback(data.map(classToEvent));
        }
    });
}
function eventClicked(event) {
    "use strict";
    $.get({
        url: "http://localhost:8000/api/classes/id/" + event.id,
        success: function (data) {
            $("#classTitle").text(data.subjectName);
            $("#classType").text(data.type);
            $("#classTime").text(moment.utc(data.dateTime)
                .format("DD.MM.YYYY, dddd, HH:mm"));
            $("#classInfoModal").modal("show");
        }
    });
}
function classToEvent(classInfo) {
    "use strict";
    return {
        id: classInfo.id,
        title: classInfo.subjectName + ": " + classInfo.type,
        start: moment.utc(classInfo.dateTime).format(),
        end: moment.utc(classInfo.dateTime)
            .add(1, "hours")
            .add(20, "minutes")
            .format()
    };
}
//# sourceMappingURL=callendar.js.map