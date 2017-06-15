"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("rxjs/add/observable/of");
require("rxjs/add/observable/throw");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/first");
require("rxjs/add/operator/map");
require("rxjs/add/operator/publish");
require("rxjs/add/operator/switch");
var moment = require("moment");
require("fullcalendar");
require("fullcalendar/dist/locale/uk");
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var app_module_1 = require("./app.module");
moment.locale("uk");
platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.default);
//# sourceMappingURL=main.js.map