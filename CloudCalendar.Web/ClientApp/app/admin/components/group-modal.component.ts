import { Component, OnInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { Group, Lecturer } from "../../common/models";
import { getUserInitials } from "../../common/functions";
import { GroupService, LecturerService } from "../../common/common";

@Component({
	selector: "admin-group-modal",
	templateUrl: "./group-modal.component.html",
	styleUrls: [ "../../../css/style.css" ]
})
export class GroupModalComponent implements OnInit {
	private activeModal: NgbActiveModal;

	private groupService:  GroupService;
	private lecturerService: LecturerService;

	group: Group = {
		name: null,
		year: 0,
		curatorId: 0
	};

	departmentId: number;
	curator = null as Lecturer;
	lecturers: Lecturer[] = [];

	isEditing = false;
	error = false;
	errorText = "";

	constructor(
		activeModal: NgbActiveModal,
		buildingService: GroupService,
		lecturerService: LecturerService) {
		this.activeModal = activeModal;
		this.groupService = buildingService;
		this.lecturerService = lecturerService;
	}

	ngOnInit(): void {
		if (this.isEditing) {
			if (this.isEditing) {
				this.curator = this.lecturers.find(
					l => l.id === this.group.curatorId);
			}
		}
	}

	change(): void {
		this.error = false;
		this.errorText = "";
	}

	submit(): void {
		if (!this.group.name || this.group.name.length === 0 ||
			this.curator == null) {
			this.error = true;
			this.errorText = "Заповніть усі поля.";
			return;
		}

		this.group.curatorId = this.curator.id;
		
		const action = this.isEditing
			? this.groupService.updateGroup(this.group)
			: this.groupService.addGroup(this.group);

		action.subscribe(
			() => this.activeModal.close(this.group),
			() => {
				this.error = true;
				this.errorText = "Не вдалося зберегти.";
			});

		action.connect();
	}

	getUserInitials = getUserInitials;
}
