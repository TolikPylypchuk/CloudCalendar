/// <binding BeforeBuild='compile-sass' />

var gulp = require("gulp");
var gulpSass = require("gulp-sass");

gulp.task("compile-scss", () => {
	return gulp.src("./wwwroot/scss/*")
			   .pipe(gulpSass())
			   .pipe(gulp.dest("./wwwroot/dist/css"));
});

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

gulp.task("default", [ "compile-scss" ]);
