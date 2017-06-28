"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/http");
function getAuthToken() {
    var token = localStorage.getItem("ipAuthToken");
    return token ? token : null;
}
exports.getAuthToken = getAuthToken;
function getHeaders() {
    var token = getAuthToken();
    return token
        ? new http_1.Headers({
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        })
        : new http_1.Headers({
            "Content-Type": "application/json"
        });
}
exports.getHeaders = getHeaders;
//# sourceMappingURL=functions.js.map