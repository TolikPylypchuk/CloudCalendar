import { Component, Input } from "@angular/core";

@Component({
	selector: "student-app",
	template: `
		<student-calendar [studentId]="getStudentId()"
		                  [groupId]="getGroupId()">
		<student-calendar>
	`
})
export default class AppComponent {
	getStudentId(): number {
		return $("student-app").data("student-id") as number;
	}

	getGroupId(): number {
		return $("student-app").data("group-id") as number;
	}
}
