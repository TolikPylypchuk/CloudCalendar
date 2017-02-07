import "rxjs/add/operator/map";

import * as moment from "moment";
import "fullcalendar";
import "fullcalendar/dist/locale/uk";

import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import AppModule from "./app.module";

moment.locale("uk");

platformBrowserDynamic().bootstrapModule(AppModule);
