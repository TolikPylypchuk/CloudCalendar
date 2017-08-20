import { NgModule } from "@angular/core";
import { CommonModule as NgCommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { CommonModule } from "./common/common";
import { AccountModule } from "./account/account";
import { LecturerModule } from "./lecturer/lecturer";
import { StudentModule } from "./student/student";
import { ViewModule } from "./view/view";

import { RoutesModule } from "./routes.module";

import { AppComponent } from "./app.component";
import { NavigationComponent } from "./navigation.component";
import { StartPageComponent } from "./start-page.component";

@NgModule({
	declarations: [
		AppComponent,
		NavigationComponent,
		StartPageComponent
	],
	imports: [
		NgCommonModule,
        HttpModule,
		FormsModule,
	    NgbModule.forRoot(),
	    CommonModule,
	    AccountModule,
	    LecturerModule,
	    StudentModule,
	    ViewModule,
	    RoutesModule
    ]
})
export class AppModuleShared {
}
