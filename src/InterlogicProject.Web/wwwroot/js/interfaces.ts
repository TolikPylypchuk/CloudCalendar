interface Entity {
	id: number;
}

interface EntityWithUser extends Entity {
	userId: number;
	userFirstName: string;
	userMiddleName: string;
	userLastName: string;
}

interface Class extends Entity {
	subjectId: number;
	subjectName: string;
	dateTime: string;
	type: string;
}

interface ClassPlace extends Entity {
	classId: number;
	building: string;
	classroom: string;
}

interface Department extends Entity {
	name: string;
	facultyId: number;
}

interface Group extends Entity {
	name: string;
	year: number;
	curatorId: number;
}

interface Lecturer extends EntityWithUser {
	departmentId: number;
	departmentName: string;
	isDean: boolean;
	isHead: boolean;
	isAdmin: boolean;
}

interface LecturerClass extends Entity {
	lecturerId: number;
	classId: number;
}

interface Student extends EntityWithUser {
	groupId: number;
	groupName: string;
	isGroupLeader: boolean;
	transcriptNumber: string;
}

interface User extends Entity {
	email: string;
	firstName: string;
	middleName: string;
	lastName: string;
}
