export interface Entity {
	id?: number;
}

export interface EntityWithUser extends Entity {
	userId?: number;
	userFirstName?: string;
	userMiddleName?: string;
	userLastName?: string;
	userFullName?: string;
	userEmail?: string;
}

export interface User extends Entity {
	email?: string;
	firstName?: string;
	middleName?: string;
	lastName?: string;
	fullName?: string;
	roles?: string[];
}

export interface Building extends Entity {
	name?: string;
	address?: string;
}

export interface Class extends Entity {
	subjectId?: number;
	subjectName?: string;
	dateTime?: string;
	type?: string;
	homeworkEnabled?: boolean;
}

export interface Classroom extends Entity {
	name?: string;
	buildingId?: number;
	buildingName?: string;
	buildingAddress?: string;
}

export interface Comment extends EntityWithUser {
	classId?: number;
	text?: string;
	dateTime?: string;
}

export interface Department extends Entity {
	name?: string;
	facultyId?: number;
}

export interface Faculty extends Entity {
	name?: string;
	buildingId?: number;
	buildingName?: string;
	buildingAddress?: string;
}

export interface Group extends Entity {
	name?: string;
	year?: number;
	curatorId?: number;
}

export interface Homework extends Entity {
	fileName?: string;
	studentId?: number;
	classId?: number;
	dateTime?: string;
	accepted?: boolean;
}

export interface Lecturer extends User {
	departmentId?: number;
	departmentName?: string;
	userId?: number;
	isDean?: boolean;
	isHead?: boolean;
	isAdmin?: boolean;
}

export interface Material extends Entity {
	classId?: number;
	fileName?: string;
}

export interface Notification extends EntityWithUser {
	text?: string;
	dateTime?: string;
	isSeen?: boolean;
}

export interface Student extends User {
	groupId?: number;
	groupName?: string;
	isGroupLeader?: boolean;
	transcriptNumber?: string;
	userId?: number;
}

export interface Subject extends Entity {
	name?: string;
}
