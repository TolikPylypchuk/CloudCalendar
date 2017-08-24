using System;
using System.Collections.Generic;
using System.Linq;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;

using CalendarClass = CloudCalendar.Data.Models.Class;
using CalendarClassroom = CloudCalendar.Data.Models.Classroom;
using CalendarGroup = CloudCalendar.Data.Models.Group;
using CalendarLecturer = CloudCalendar.Data.Models.Lecturer;
using CalendarSubject = CloudCalendar.Data.Models.Subject;

using ScheduleClassroom = CloudCalendar.Schedule.Models.Classroom;
using ScheduleGroup = CloudCalendar.Schedule.Models.Group;
using ScheduleLecturer = CloudCalendar.Schedule.Models.Lecturer;

namespace CloudCalendar.Schedule.Utilities
{
	public static class ScheduleUtilities
	{
		public static DayOfWeek GetDayOfWeek(string dayOfWeekName)
		{
			DayOfWeek result;

			switch (dayOfWeekName.ToLower())
			{
				case "понеділок":
					result = DayOfWeek.Monday;
					break;
				case "вівторок":
					result = DayOfWeek.Tuesday;
					break;
				case "середа":
					result = DayOfWeek.Wednesday;
					break;
				case "четвер":
					result = DayOfWeek.Thursday;
					break;
				case "п'ятниця":
					result = DayOfWeek.Friday;
					break;
				default:
					throw new ArgumentOutOfRangeException(
						nameof(dayOfWeekName),
						"The day of week name must be in Ukrainian " +
						"and must be between Monday and Friday.");
			}

			return result;
		}

		public static ClassFrequency GetFrequency(string frequencyName)
		{
			ClassFrequency result;

			switch (frequencyName.ToLower())
			{
				case "щотижня":
					result = ClassFrequency.Weekly;
					break;
				case "по чисельнику":
					result = ClassFrequency.Numerator;
					break;
				case "по знаменнику":
					result = ClassFrequency.Denominator;
					break;
				default:
					throw new ArgumentOutOfRangeException(
						nameof(frequencyName),
						"The frequency name is invalid. Must be one of these " +
						"(case insensitive): \"щотижня\", \"по чисельнику\", " +
						"\"по знаменнику\"");
			}

			return result;
		}

		public static string GetCurrentGroupName(ScheduleGroup group)
			=> group.Name.Replace(
				"0", (DateTime.Now.Year - group.Year + 1).ToString());

		public static IList<ClassPlace> GetPlaces(
			CalendarClass c,
			IEnumerable<ScheduleClassroom> rooms,
			IRepository<CalendarClassroom> repository)
			=> rooms.Select(r => new ClassPlace
				{
					ClassroomId = GetClassroomId(r, repository),
					Class = c
				})
				.ToList();

		public static int GetClassroomId(
			ScheduleClassroom room,
			IRepository<CalendarClassroom> repository)
			=> repository.GetAll()
				.FirstOrDefault(
					r => room.Number == r.Name &&
						 room.Building.Name == r.Building.Name)
				.Id;

		public static IList<GroupClass> GetGroups(
			CalendarClass c,
			IEnumerable<ScheduleGroup> groups,
			IRepository<CalendarGroup> repository)
			=> groups.Select(g => new GroupClass
				{
					GroupId = GetGroupId(g, repository),
					Class = c
				})
				.ToList();

		public static int GetGroupId(
			ScheduleGroup group,
			IRepository<CalendarGroup> repository)
			=> repository.GetAll()
				.FirstOrDefault(
					g => GetCurrentGroupName(group) == g.Name)
				.Id;

		public static IList<LecturerClass> GetLecturers(
			CalendarClass c,
			IEnumerable<ScheduleLecturer> lecturers,
			IRepository<CalendarLecturer> repository)
			=> lecturers.Select(l => new LecturerClass
				{
					LecturerId = GetLecturerId(l, repository),
					Class = c
				})
				.ToList();

		public static int GetLecturerId(
			ScheduleLecturer lecturer,
			IRepository<CalendarLecturer> repository)
			=> repository.GetAll()
				.FirstOrDefault(
					l => l.User.FirstName == lecturer.FirstName &&
						 l.User.MiddleName == lecturer.MiddleName &&
						 l.User.LastName == lecturer.LastName &&
						 l.Department.Faculty.Name == lecturer.Faculty.Name)
				.Id;

		public static int GetSubjectId(
			string subjectName,
			IRepository<CalendarSubject> repository)
			=> repository.GetAll().FirstOrDefault(s => s.Name == subjectName).Id;
	}
}
