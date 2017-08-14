import "./imports";

import { enableProdMode } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { environment } from "./environments/environment";

import * as moment from "moment";

import { AppModule } from "./app/app.module";

declare var module: any;

moment.locale("uk");

if (environment.production) {
	enableProdMode();
}

const bootApplication = () => {
	platformBrowserDynamic().bootstrapModule(AppModule);
};

if (module["hot"]) {
	module["hot"].accept();
	module["hot"].dispose(() => {
		const oldRootElem = document.querySelector("ip-app");
		const newRootElem = document.createElement("ip-app");
		oldRootElem.parentNode.insertBefore(newRootElem, oldRootElem);
		platformBrowserDynamic().destroy();
	});
}

if (document.readyState === "complete") {
	bootApplication();
} else {
	document.addEventListener("DOMContentLoaded", bootApplication);
}
