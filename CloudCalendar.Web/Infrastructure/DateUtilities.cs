using System;
using System.Globalization;

using CloudCalendar.Schedule.Services.Options;

namespace CloudCalendar.Web.Infrastructure
{
	public static class DateUtilities
	{
		public static (int, int) GetCurrentYearAndSemester(
			ScheduleOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			var (day, month) = ParseSemesterDate(options.Semesters[0].Start);

			var now = DateTime.Now.Date;

			int year = new DateTime(now.Year, month, day) < now
				? now.Year
				: now.Year - 1;

			int semester = 0;

			foreach (var s in options.Semesters)
			{
				var (start, end) = GetSemesterBounds(
					GetYearStart(options, year), s);

				if (now >= start && now <= end)
				{
					break;
				}

				semester++;
			}

			return (year, semester);
		}

		public static (int, int) ParseSemesterDate(string date)
		{
			var tokens = date?.Split('.')
				?? throw new ArgumentNullException(nameof(date));

			if (tokens.Length != 2)
			{
				throw new ArgumentException(
					"The date must a have a dd.MM format.",
					nameof(date));
			}

			return (Int32.Parse(tokens[0]), Int32.Parse(tokens[1]));
		}

		public static (DateTime, DateTime) GetSemesterBounds(
			DateTime yearStart, ScheduleOptions.Semester semester)
		{
			if (semester == null)
			{
				throw new ArgumentNullException(nameof(semester));
			}

			var (startDay, startMonth) = ParseSemesterDate(semester.Start);
			var (endDay, endMonth) = ParseSemesterDate(semester.End);

			var start = new DateTime(yearStart.Year, startMonth, startDay);

			if (start < yearStart)
			{
				start = new DateTime(yearStart.Year + 1, startMonth, startDay);
			}

			var end = new DateTime(start.Year, endMonth, endDay);

			if (end <= start)
			{
				end = new DateTime(start.Year + 1, endMonth, endDay);
			}

			return (start, end);
		}

		public static DateTime GetYearStart(ScheduleOptions options, int year)
		{
			return DateTime.ParseExact(
				$"{options.Semesters[0].Start}.{year:0000}",
				"dd.MM.yyyy",
				CultureInfo.InvariantCulture);
		}
	}
}
