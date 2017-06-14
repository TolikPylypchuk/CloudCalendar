import AccountModule from "./account.module";
import RoutesModule from "./routes.module";

import LoginComponent from "./components/login.component";
import LogoutComponent from "./components/logout.component";

import AccountService from "./services/account.service";

import AuthGuard from "./guards/auth.guard";
import NotAuthGuard from "./guards/not-auth.guard";

export {
	AccountModule,
	RoutesModule,

	LoginComponent,
	LogoutComponent,

	AccountService,

	AuthGuard,
	NotAuthGuard
}
