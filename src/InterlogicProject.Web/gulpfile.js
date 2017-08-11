var gulp = require("gulp");

gulp.task("copy-angular-bundles", () => {
	return gulp.src("./node_modules/@angular/**/bundles/**")
			   .pipe(gulp.dest("./wwwroot/lib/angular/"));
});

gulp.task("copy-ng-bootstrap", () => {
	return gulp.src("./node_modules/@ng-bootstrap/**")
			   .pipe(gulp.dest("./wwwroot/lib/ng-bootstrap/"));
});

gulp.task("copy-ng2-file-upload", () => {
	return gulp.src("./node_modules/ng2-file-upload/bundles/**")
			   .pipe(gulp.dest("./wwwroot/lib/ng2-file-upload/"));
});

gulp.task("copy-popper", () => {
	return gulp.src("./node_modules/popper.js/**")
		.pipe(gulp.dest("./wwwroot/lib/popper.js/"));
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
		"copy-ng2-file-upload",
		"copy-rxjs"
	]);
