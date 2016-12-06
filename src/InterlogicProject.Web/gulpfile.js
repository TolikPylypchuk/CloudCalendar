/// <binding BeforeBuild='compile-sass' AfterBuild='compile-ts' />

var gulp = require("gulp");
var gulpSass = require("gulp-sass");

gulp.task("compile-sass", function () {
	gulp.src("./wwwroot/css/style.scss")
		.pipe(gulpSass())
		.pipe(gulp.dest("./wwwroot/css/dist"));
});
