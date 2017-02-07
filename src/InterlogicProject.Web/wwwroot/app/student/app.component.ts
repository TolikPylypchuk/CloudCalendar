import { Component, Input } from "@angular/core";

@Component({
	selector: "student-app",
	template: `
		<student-calendar [studentId]="studentId">
		<student-calendar>
	`
})
export default class AppComponent {
	@Input() studentId: number;
}
