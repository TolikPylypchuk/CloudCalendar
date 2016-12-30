/// <amd-module name="calendarModels" />

export interface Entity {
	id: number;
}

export interface User extends Entity {
	email: string;
	firstName: string;
	middleName: string;
	lastName: string;
}

export interface EntityWithUser extends Entity {
	userId: number;
	userFirstName: string;
	userMiddleName: string;
	userLastName: string;
}

export interface Class extends Entity {
	subjectId: number;
	subjectName: string;
	groupId: number;
	groupName: string;
	dateTime: string;
	type: string;
}

export interface ClassPlace extends Entity {
	classId: number;
	building: string;
	classroom: string;
}

export interface Department extends Entity {
	name: string;
	facultyId: number;
}

export interface Group extends Entity {
	name: string;
	year: number;
	curatorId: number;
}

export interface Lecturer extends EntityWithUser {
	departmentId: number;
	departmentName: string;
	isDean: boolean;
	isHead: boolean;
	isAdmin: boolean;
}

export interface LecturerClass extends Entity {
	lecturerId: number;
	classId: number;
}

export interface Student extends EntityWithUser {
	groupId: number;
	groupName: string;
	isGroupLeader: boolean;
	transcriptNumber: string;
}
