"use strict";
require("rxjs/add/observable/throw");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
var moment = require("moment");
require("fullcalendar");
require("fullcalendar/dist/locale/uk");
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var app_module_1 = require("./app.module");
moment.locale("uk");
platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.default);
//# sourceMappingURL=main.js.map