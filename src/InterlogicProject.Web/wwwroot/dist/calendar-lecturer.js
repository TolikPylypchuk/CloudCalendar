System.register(["moment", "fullcalendar", "fullcalendar/dist/locale/uk"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    function init() {
        moment.locale("uk");
        $.get({
            url: "http://localhost:8000/api/lecturers/id/" + currentLecturerId,
            success: function (lecturer) {
                currentLecturer = lecturer;
                initCalendar();
            }
        });
    }
    function initCalendar() {
        $("#calendar").fullCalendar({
            allDaySlot: false,
            columnFormat: "dd, DD.MM",
            defaultView: "agendaWeek",
            eventClick: eventClicked,
            eventColor: "#0275D8",
            events: getEvents,
            header: {
                left: "title",
                center: "agendaWeek,listWeek",
                right: "today,prev,next"
            },
            minTime: moment.duration("08:00:00"),
            maxTime: moment.duration("21:00:00"),
            slotDuration: moment.duration("00:30:00"),
            slotLabelFormat: "HH:mm",
            slotLabelInterval: moment.duration("01:00:00"),
            titleFormat: "DD MMM YYYY",
            weekends: false,
            weekNumbers: true,
            weekNumberTitle: "Тиж "
        });
        $("#calendar").fullCalendar("option", "height", "auto");
    }
    function getEvents(start, end, timezone, callback) {
        $.get({
            url: "http://localhost:8000/api/classes/" +
                ("lecturerId/" + currentLecturerId + "/") +
                ("range/" + start.format("YYYY-MM-DD") + "/" + end.format("YYYY-MM-DD")),
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
                $("#classGroup").text("\u0413\u0440\u0443\u043F\u0430: " + data.groupName);
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
    var moment, currentLecturerId, currentLecturer;
    return {
        setters: [
            function (moment_1) {
                moment = moment_1;
            },
            function (_1) {
            },
            function (_2) {
            }
        ],
        execute: function () {
            currentLecturerId = 1;
            if (document.readyState !== "complete") {
                $(document).ready(init);
            }
            else {
                init();
            }
        }
    };
});
//# sourceMappingURL=calendar-lecturer.js.map