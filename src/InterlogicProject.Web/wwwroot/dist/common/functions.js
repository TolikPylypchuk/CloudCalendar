"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
function handleError(error) {
    var message;
    if (error instanceof http_1.Response) {
        var body = error.json() || "";
        var err = body.error || JSON.stringify(body);
        message = error.status + " - " + (error.statusText || "") + " " + err;
    }
    else {
        message = error.message ? error.message : error.toString();
    }
    console.error(message);
    return Observable_1.Observable.throw(message);
}
exports.handleError = handleError;
//# sourceMappingURL=functions.js.map