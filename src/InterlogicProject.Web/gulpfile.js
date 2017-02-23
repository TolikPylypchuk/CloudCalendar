var gulp = require("gulp");

gulp.task("copy-angular-bundles", () => {
	return gulp.src("./node_modules/@angular/**/bundles/**")
			   .pipe(gulp.dest("./wwwroot/lib/angular/"));
});

gulp.task("copy-ng-bootstrap", () => {
	return gulp.src("./node_modules/@ng-bootstrap/**")
		.pipe(gulp.dest("./wwwroot/lib/ng-bootstrap/"));
});

gulp.task("copy-angular2-fullcalendar", () => {
	return gulp.src("./node_modules/angular2-fullcalendar/**")
		.pipe(gulp.dest("./wwwroot/lib/angular2-fullcalendar/"));
});

gulp.task("copy-rxjs", () => {
	return gulp.src("./node_modules/rxjs/**")
		.pipe(gulp.dest("./wwwroot/lib/rxjs/"));
});

gulp.task(
	"default",
	[
		"copy-angular-bundles",
		"copy-ng-bootstrap",
		"copy-angular2-fullcalendar",
		"copy-rxjs"
	]);
