/// <amd-module name="callendarLecturer" />
define("callendarLecturer", ["require", "exports", "../lib/moment/moment"], function (require, exports, moment) {
    "use strict";
    var currentLecturerId = $("#callendarScript").data("lecturer-id");
    var currentLecturer;
    if (document.readyState !== "complete") {
        $(document).ready(function () {
            init();
        });
    }
    else {
        init();
    }
    function init() {
        $.get({
            url: "http://localhost:8000/api/lecturers/id/" + currentLecturerId,
            success: function (lecturer) {
                currentLecturer = lecturer;
                initCallendar();
            }
        });
    }
    function initCallendar() {
        $("#calendar").fullCalendar({
            allDaySlot: false,
            defaultView: "agendaWeek",
            eventClick: eventClicked,
            eventColor: "#0275D8",
            events: getEvents,
            header: {
                right: "today,prev,next",
                left: "title",
                center: ""
            },
            minTime: moment.duration("08:00:00"),
            maxTime: moment.duration("21:00:00"),
            weekends: false,
            weekNumbers: true
        });
        $("#calendar").fullCalendar("option", "height", "auto");
    }
    function getEvents(start, end, timezone, callback) {
        $.get({
            url: "http://localhost:8000/api/classes/lecturerId/" +
                (currentLecturerId + "/range/") +
                (start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")),
            success: function (data) {
                callback(data.map(classToEvent));
            }
        });
    }
    function eventClicked(event) {
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
});
//# sourceMappingURL=callendarLecturer.js.map