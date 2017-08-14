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

import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { AppModule } from "./app/app.module";

moment.locale("uk");

if (environment.production) {
	enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule);