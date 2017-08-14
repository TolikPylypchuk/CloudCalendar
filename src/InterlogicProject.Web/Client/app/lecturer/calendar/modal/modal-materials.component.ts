import { Component, Input, OnInit } from "@angular/core";
import { FileUploader } from "ng2-file-upload";

import * as moment from "moment";

import {
	ClassService, LecturerService, MaterialService, NotificationService
} from "../../../common/common";
import { getAuthToken } from "../../../common/functions";
import { Class, Lecturer, Material } from "../../../common/models";

@Component({
	selector: "ip-lecturer-modal-materials",
	templateUrl: "/templates/lecturer/calendar/modal-materials",
	styleUrls: [ "/dist/css/style.min.css" ]
})
export class ModalMaterialsComponent implements OnInit {
	@Input() classId: number;

	currentClass: Class;
	currentLecturer: Lecturer;
	materials: Material[] = [];

	uploader: FileUploader;
	hasDropZoneOver = false;

	private classService: ClassService;
	private lecturerService: LecturerService;
	private materialService: MaterialService;
	private notificationService: NotificationService;

	constructor(
		classService: ClassService,
		lecturerService: LecturerService,
		materialService: MaterialService,
		notificationService: NotificationService) {
		this.classService = classService;
		this.lecturerService = lecturerService;
		this.materialService = materialService;
		this.notificationService = notificationService;
	}

	ngOnInit(): void {
		this.uploader = new FileUploader(
			{
				url: `/api/materials/classId/${this.classId}`,
				authToken: `Bearer ${getAuthToken()}`,
				removeAfterUpload: true
			});

		this.classService.getClass(this.classId)
			.subscribe(c => this.currentClass = c);

		this.lecturerService.getCurrentLecturer()
			.subscribe(lecturer => this.currentLecturer = lecturer);

		this.materialService.getMaterialsByClass(this.classId)
			.subscribe(materials => this.materials = materials);

		this.uploader.onCompleteAll = () => {
			this.materialService.getMaterialsByClass(this.classId)
				.subscribe(materials => this.materials = materials);

			this.notificationService.addNotificationForGroupsInClass(
				{
					dateTime: moment().toISOString(),
					text: this.getNotificationText(),
					classId: this.classId
				},
				this.classId)
				.connect();
		};
	}

	deleteMaterial(material: Material): void {
		const action = this.materialService.deleteMaterial(material.id);

		action.subscribe(response => {
			if (response.status === 204) {
				this.materials = this.materials.filter(
					m => m.id !== material.id);
			}
		});

		action.connect();
	}
	
	fileOverDropZone(e: any): void {
		this.hasDropZoneOver = e;
	}

	private getNotificationText(): string {
		return `${this.currentLecturer.firstName} ` +
			`${this.currentLecturer.lastName } додав матеріали` +
			`до пари '${this.currentClass.subjectName}' ` +
			`${moment(this.currentClass.dateTime).format("DD.MM.YYYY")}.`;
	}
}
