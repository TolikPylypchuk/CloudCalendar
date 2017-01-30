/// <binding BeforeBuild='compile-sass' />

var gulp = require("gulp");
var gulpSass = require("gulp-sass");

gulp.task("compile-sass", () => {
	return gulp.src("./wwwroot/css/style.scss")
			   .pipe(gulpSass())
			   .pipe(gulp.dest("./wwwroot/css/dist"));
});

gulp.task("copy-angular-bundles", () => {
	return gulp.src("./node_modules/@angular/**/bundles/**")
			   .pipe(gulp.dest("./wwwroot/lib/angular2/"));
});

gulp.task("default", [ "compile-sass" ]);
