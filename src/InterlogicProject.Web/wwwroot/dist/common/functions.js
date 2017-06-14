"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
function getAuthToken() {
    var token = localStorage.getItem("ipAuthToken");
    return token ? JSON.parse(token).token : null;
}
exports.getAuthToken = getAuthToken;
function getHeaders() {
    return getAuthToken()
        ? new http_1.Headers({
            "Content-Type": "application/json",
            "Authorization": "Bearer " + getAuthToken()
        })
        : new http_1.Headers({
            "Content-Type": "application/json"
        });
}
exports.getHeaders = getHeaders;
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