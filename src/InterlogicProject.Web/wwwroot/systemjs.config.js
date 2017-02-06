(function() {

var pathMappings = {
	"@angular/core": "lib/angular/core/bundles/core.umd.js",
	"@angular/common": "lib/angular/common/bundles/common.umd.js",
	"@angular/compiler": "lib/angular/compiler/bundles/compiler.umd.js",
	"@angular/platform-browser": "lib/angular/platform-browser/bundles/platform-browser.umd.js",
	"@angular/platform-browser-dynamic": "lib/angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js",
	"@angular/http": "lib/angular/http/bundles/http.umd.js",
	"@angular/router": "lib/angular/router/bundles/router.umd.js",
	"@angular/router/index": "lib/angular/router/bundles/router.umd.js",
	"@ng-bootstrap/ng-bootstrap": "lib/ng-bootstrap/ng-bootstrap/index.js",
	"fullcalendar": "lib/fullcalendar/dist/fullcalendar.js",
	"jquery": "lib/jquery/dist/jquery.js",
	"moment": "lib/moment/moment.js",
	"rxjs": "lib/rxjs",
	"uk": "lib/fullcalendar/dist/locale/uk.js"
};

var packages = [
	"rxjs",
	"dist"
];

var packagesConfig = {};

packages.forEach(function(packageName) {
	packagesConfig[packageName] = {
		main: "index.js",
		defaultExtension: "js"
	};
});

System.config({
	map: pathMappings,
	packages: packagesConfig
});

})();
