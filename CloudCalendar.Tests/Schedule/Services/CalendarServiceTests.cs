using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;
using CloudCalendar.Schedule.Services.Options;

using CalendarBuilding = CloudCalendar.Data.Models.Building;
using CalendarClassroom = CloudCalendar.Data.Models.Classroom;
using CalendarFaculty = CloudCalendar.Data.Models.Faculty;
using CalendarGroup = CloudCalendar.Data.Models.Group;
using CalendarLecturer = CloudCalendar.Data.Models.Lecturer;
using CalendarSubject = CloudCalendar.Data.Models.Subject;

using ScheduleBuilding = CloudCalendar.Schedule.Models.Building;
using ScheduleClass = CloudCalendar.Schedule.Models.Class;
using ScheduleClassroom = CloudCalendar.Schedule.Models.Classroom;
using ScheduleFaculty = CloudCalendar.Schedule.Models.Faculty;
using ScheduleGroup = CloudCalendar.Schedule.Models.Group;
using ScheduleLecturer = CloudCalendar.Schedule.Models.Lecturer;
using ScheduleSubject = CloudCalendar.Schedule.Models.Subject;

namespace CloudCalendar.Schedule.Services
{
	[TestClass]
	public class CalendarServiceTests
	{
		private Mock<IRepository<CalendarClassroom>> mockClassrooms;
		private Mock<IRepository<CalendarGroup>> mockGroups;
		private Mock<IRepository<CalendarLecturer>> mockLecturers;
		private Mock<IRepository<CalendarSubject>> mockSubjects;

		private Mock<IOptionsSnapshot<ScheduleOptions>> mockOptions;

		[TestInitialize]
		public void Initialize()
		{
			this.mockClassrooms = new Mock<IRepository<CalendarClassroom>>();
			this.mockClassrooms.Setup(cl => cl.GetAll())
				.Returns(() => new[]
				{
					new CalendarClassroom
					{
						Id = 1,
						Name = "111",
						Building = new CalendarBuilding
						{
							Id = 1,
							Name = "a"
						}
					}
				}.AsQueryable());

			this.mockGroups = new Mock<IRepository<CalendarGroup>>();
			this.mockGroups.Setup(g => g.GetAll())
				.Returns(() => new[]
				{
					new CalendarGroup
					{
						Id = 1,
						Name = "group2",
						Year = 2016
					}
				}.AsQueryable());

			this.mockLecturers = new Mock<IRepository<CalendarLecturer>>();
			this.mockLecturers.Setup(l => l.GetAll())
				.Returns(() => new[]
				{
					new CalendarLecturer
					{
						Id = 1,
						User = new User
						{
							Id = "1",
							FirstName = "a",
							MiddleName = "b",
							LastName = "c"
						},
						Department = new Department
						{
							Id = 1,
							Faculty = new CalendarFaculty
							{
								Id = 1,
								Name = "f"
							}
						}
					}
				}.AsQueryable());

			this.mockSubjects = new Mock<IRepository<CalendarSubject>>();
			this.mockSubjects.Setup(l => l.GetAll())
				.Returns(() => new[]
				{
					new CalendarSubject
					{
						Id = 1,
						Name = "s"
					}
				}.AsQueryable());

			this.mockOptions = new Mock<IOptionsSnapshot<ScheduleOptions>>();
			this.mockOptions.Setup(o => o.Value)
				.Returns(() => new ScheduleOptions
				{
					ClassStarts = new[]
					{
						"08:30",
						"10:10",
						"11:50",
						"13:30",
						"15:05",
						"16:40",
						"18:10",
						"19:35",
						"21:00"
					},
					ClassEnds = new[]
					{
						"09:50",
						"11:30",
						"13:10",
						"14:50",
						"16:25",
						"18:00",
						"19:30",
						"20:55",
						"22:20"
					},
					ClassDuration = "01:20"
				});
		}

		[TestMethod]
		public void WeeklyClassesShouldBeEveryWeek()
		{
			var calendarService = new CalendarService(
				this.mockClassrooms.Object,
				this.mockGroups.Object,
				this.mockLecturers.Object,
				this.mockSubjects.Object,
				this.mockOptions.Object);

			var calendarClasses = calendarService.CreateCalendar(
				new List<ScheduleClass>
				{
					new ScheduleClass
					{
						Id = 1,
						Number = 2,
						DayOfWeek = "понеділок",
						Frequency = "щотижня",
						Classrooms = new List<ScheduleClassroom>
						{
							new ScheduleClassroom
							{
								Id = 1,
								Number = "111",
								Building = new ScheduleBuilding
								{
									Id = 1,
									Name = "a"
								}
							}
						},
						Groups = new List<ScheduleGroup>
						{
							new ScheduleGroup
							{
								Id = 1,
								Name = "group0",
								Year = 2016
							}
						},
						Lecturers = new List<ScheduleLecturer>
						{
							new ScheduleLecturer
							{
								Id = 1,
								FirstName = "a",
								MiddleName = "b",
								LastName = "c",
								Faculty = new ScheduleFaculty
								{
									Id = 1,
									Name = "f"
								}
							}
						},
						Subject = new ScheduleSubject
						{
							Id = 1,
							Name = "s"
						},
						Type = "Лекція"
					}
				},
				new DateTime(2017, 08, 17),
				new DateTime(2017, 09, 8));

			Assert.AreEqual(3, calendarClasses.Count);

			calendarClasses = calendarClasses.OrderBy(c => c.DateTime).ToList();

			Assert.AreEqual(
				new DateTime(2017, 08, 21, 10, 10, 0),
				calendarClasses[0].DateTime);

			Assert.AreEqual(
				new DateTime(2017, 08, 28, 10, 10, 0),
				calendarClasses[1].DateTime);

			Assert.AreEqual(
				new DateTime(2017, 09, 04, 10, 10, 0),
				calendarClasses[2].DateTime);
		}

		[TestMethod]
		public void NumeratorClassesShouldBeTwiceAWeek()
		{
			var calendarService = new CalendarService(
				this.mockClassrooms.Object,
				this.mockGroups.Object,
				this.mockLecturers.Object,
				this.mockSubjects.Object,
				this.mockOptions.Object);

			var calendarClasses = calendarService.CreateCalendar(
				new List<ScheduleClass>
				{
					new ScheduleClass
					{
						Id = 1,
						Number = 2,
						DayOfWeek = "понеділок",
						Frequency = "по чисельнику",
						Classrooms = new List<ScheduleClassroom>
						{
							new ScheduleClassroom
							{
								Id = 1,
								Number = "111",
								Building = new ScheduleBuilding
								{
									Id = 1,
									Name = "a"
								}
							}
						},
						Groups = new List<ScheduleGroup>
						{
							new ScheduleGroup
							{
								Id = 1,
								Name = "group0",
								Year = 2016
							}
						},
						Lecturers = new List<ScheduleLecturer>
						{
							new ScheduleLecturer
							{
								Id = 1,
								FirstName = "a",
								MiddleName = "b",
								LastName = "c",
								Faculty = new ScheduleFaculty
								{
									Id = 1,
									Name = "f"
								}
							}
						},
						Subject = new ScheduleSubject
						{
							Id = 1,
							Name = "s"
						},
						Type = "Лекція"
					}
				},
				new DateTime(2017, 08, 17),
				new DateTime(2017, 09, 8));

			Assert.AreEqual(1, calendarClasses.Count);

			Assert.AreEqual(
				new DateTime(2017, 08, 28, 10, 10, 0),
				calendarClasses[0].DateTime);
		}

		[TestMethod]
		public void DenominatorClassesShouldBeTwiceAWeek()
		{
			var calendarService = new CalendarService(
				this.mockClassrooms.Object,
				this.mockGroups.Object,
				this.mockLecturers.Object,
				this.mockSubjects.Object,
				this.mockOptions.Object);

			var calendarClasses = calendarService.CreateCalendar(
				new List<ScheduleClass>
				{
					new ScheduleClass
					{
						Id = 1,
						Number = 2,
						DayOfWeek = "понеділок",
						Frequency = "по знаменнику",
						Classrooms = new List<ScheduleClassroom>
						{
							new ScheduleClassroom
							{
								Id = 1,
								Number = "111",
								Building = new ScheduleBuilding
								{
									Id = 1,
									Name = "a"
								}
							}
						},
						Groups = new List<ScheduleGroup>
						{
							new ScheduleGroup
							{
								Id = 1,
								Name = "group0",
								Year = 2016
							}
						},
						Lecturers = new List<ScheduleLecturer>
						{
							new ScheduleLecturer
							{
								Id = 1,
								FirstName = "a",
								MiddleName = "b",
								LastName = "c",
								Faculty = new ScheduleFaculty
								{
									Id = 1,
									Name = "f"
								}
							}
						},
						Subject = new ScheduleSubject
						{
							Id = 1,
							Name = "s"
						},
						Type = "Лекція"
					}
				},
				new DateTime(2017, 08, 17),
				new DateTime(2017, 09, 8));

			Assert.AreEqual(2, calendarClasses.Count);
			
			calendarClasses = calendarClasses.OrderBy(c => c.DateTime).ToList();

			Assert.AreEqual(
				new DateTime(2017, 08, 21, 10, 10, 0),
				calendarClasses[0].DateTime);
			
			Assert.AreEqual(
				new DateTime(2017, 09, 04, 10, 10, 0),
				calendarClasses[1].DateTime);
		}
	}
}
