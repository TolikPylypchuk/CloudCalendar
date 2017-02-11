System.register(["./services/student.service", "./services/class.service", "./common.module"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var student_service_1, class_service_1, common_module_1;
    return {
        setters: [
            function (student_service_1_1) {
                student_service_1 = student_service_1_1;
            },
            function (class_service_1_1) {
                class_service_1 = class_service_1_1;
            },
            function (common_module_1_1) {
                common_module_1 = common_module_1_1;
            }
        ],
        execute: function () {
            exports_1("StudentService", student_service_1.default);
            exports_1("ClassService", class_service_1.default);
            exports_1("CommonModule", common_module_1.default);
        }
    };
});
//# sourceMappingURL=common.js.map