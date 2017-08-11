import "rxjs/add/observable/of";
import "rxjs/add/observable/throw";

import "rxjs/add/operator/catch";
import "rxjs/add/operator/first";
import "rxjs/add/operator/map";
import "rxjs/add/operator/publish";
import "rxjs/add/operator/switch";

import * as moment from "moment";

import "fullcalendar";
import "fullcalendar/dist/locale/uk";

import "popper.js";

import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import AppModule from "./app.module";

moment.locale("uk");

platformBrowserDynamic().bootstrapModule(AppModule);
