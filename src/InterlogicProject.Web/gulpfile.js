var gulp = require("gulp");
var del = require("del");
var runSequence = require("run-sequence");

gulp.task("clean-all", () =>
	del(["./wwwroot/lib/**"]));

gulp.task("copy-angular", () =>
	gulp.src("./node_modules/@angular/**/bundles/**")
		.pipe(gulp.dest("./wwwroot/lib/@angular/")));

gulp.task("copy-bootstrap", () =>
	gulp.src("./node_modules/bootstrap/**")
		.pipe(gulp.dest("./wwwroot/lib/bootstrap")));

gulp.task("copy-es6-shim", () =>
	gulp.src("./node_modules/es6-shim/**")
		.pipe(gulp.dest("./wwwroot/lib/es6-shim/")));

gulp.task("copy-fullcalendar", () =>
	gulp.src("./node_modules/fullcalendar/**")
		.pipe(gulp.dest("./wwwroot/lib/fullcalendar/")));

gulp.task("copy-jquery", () =>
	gulp.src("./node_modules/jquery/**")
		.pipe(gulp.dest("./wwwroot/lib/jquery/")));

gulp.task("copy-moment", () =>
	gulp.src("./node_modules/moment/**")
		.pipe(gulp.dest("./wwwroot/lib/moment/")));

gulp.task("copy-ng-bootstrap", () =>
	gulp.src("./node_modules/@ng-bootstrap/**")
		.pipe(gulp.dest("./wwwroot/lib/@ng-bootstrap/")));

gulp.task("copy-ng2-file-upload", () =>
	gulp.src("./node_modules/ng2-file-upload/bundles/**")
		.pipe(gulp.dest("./wwwroot/lib/ng2-file-upload/")));

gulp.task("copy-popper", () =>
	gulp.src("./node_modules/popper.js/**")
		.pipe(gulp.dest("./wwwroot/lib/popper.js/")));

gulp.task("copy-reflect-metadata", () =>
	gulp.src("./node_modules/reflect-metadata/**")
		.pipe(gulp.dest("./wwwroot/lib/reflect-metadata/")));

gulp.task("copy-rxjs", () =>
	gulp.src("./node_modules/rxjs/**")
		.pipe(gulp.dest("./wwwroot/lib/rxjs/")));

gulp.task("copy-systemjs", () =>
	gulp.src("./node_modules/systemjs/**")
		.pipe(gulp.dest("./wwwroot/lib/systemjs/")));

gulp.task("copy-zone", () =>
	gulp.src("./node_modules/zone.js/**")
		.pipe(gulp.dest("./wwwroot/lib/zone.js/")));

gulp.task(
	"copy-all",
	[
		"copy-angular",
		"copy-bootstrap",
		"copy-es6-shim",
		"copy-fullcalendar",
		"copy-jquery",
		"copy-moment",
		"copy-ng-bootstrap",
		"copy-ng2-file-upload",
		"copy-popper",
		"copy-reflect-metadata",
		"copy-rxjs",
		"copy-systemjs",
		"copy-zone"
	]);

gulp.task("default", () => runSequence("clean-all", "copy-all"));
