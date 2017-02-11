System.register(["./calendar.component", "./modal/modal-content.component", "./modal/modal-comments.component", "./calendar.module"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var calendar_component_1, modal_content_component_1, modal_comments_component_1, calendar_module_1;
    return {
        setters: [
            function (calendar_component_1_1) {
                calendar_component_1 = calendar_component_1_1;
            },
            function (modal_content_component_1_1) {
                modal_content_component_1 = modal_content_component_1_1;
            },
            function (modal_comments_component_1_1) {
                modal_comments_component_1 = modal_comments_component_1_1;
            },
            function (calendar_module_1_1) {
                calendar_module_1 = calendar_module_1_1;
            }
        ],
        execute: function () {
            exports_1("CalendarComponent", calendar_component_1.default);
            exports_1("ModalContentComponent", modal_content_component_1.default);
            exports_1("ModalCommentsComponent", modal_comments_component_1.default);
            exports_1("CalendarModule", calendar_module_1.default);
        }
    };
});
//# sourceMappingURL=calendar.js.map