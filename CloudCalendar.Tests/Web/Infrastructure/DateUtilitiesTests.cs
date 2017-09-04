using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CloudCalendar.Schedule.Services.Options;

using static CloudCalendar.Web.Infrastructure.DateUtilities;

namespace CloudCalendar.Web.Infrastructure
{
	[TestClass]
	public class DateUtilitiesTests
	{
		[TestMethod]
		public void CurrentYearAndSemesterShouldBeCorrect()
		{
			var options = new ScheduleOptions
			{
				Semesters = new List<ScheduleOptions.Semester>
				{
					new ScheduleOptions.Semester
					{
						Start = "01.09",
						End = "31.01"
					},
					new ScheduleOptions.Semester
					{
						Start = "01.02",
						End = "31.08"
					}
				}
			};

			var (year, semester) = GetCurrentYearAndSemester(options);

			Assert.AreEqual((2017, 0), (year, semester));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CurrentYearAndSemesterForNullShouldThrowArgumentNullException()
		{
			GetCurrentYearAndSemester(null);
		}

		[TestMethod]
		public void SemesterDateShouldBeParsedCorrectly()
		{
			const int day = 1;
			const int month = 12;

			var (actualDay, actualMonth) =
				ParseSemesterDate($"{day:00}.{month:00}");

			Assert.AreEqual((day, month), (actualDay, actualMonth));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void IncorrectSemesterDateShouldThrowArgumentException()
		{
			ParseSemesterDate("01.01.01");
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullSemesterDateShouldThrowArgumentNullException()
		{
			ParseSemesterDate(null);
		}
		
		[TestMethod]
		public void SemesterBoundsShouldBeCorrectForTheGivenYear()
		{
			var yearStart = new DateTime(2017, 9, 1);

			var semester = new ScheduleOptions.Semester
			{
				Start = "01.09",
				End = "31.01"
			};

			var start = yearStart;
			var end = new DateTime(2018, 01, 31);

			var (actualStart, actualEnd) =
				GetSemesterBounds(yearStart, semester);

			Assert.AreEqual((start, end), (actualStart, actualEnd));
		}

		[TestMethod]
		public void SemesterBoundsShouldBeCorrectForTheFollowingYear()
		{
			var yearStart = new DateTime(2017, 9, 1);

			var semester = new ScheduleOptions.Semester
			{
				Start = "01.02",
				End = "31.08"
			};

			var start = new DateTime(2018, 2, 1);
			var end = new DateTime(2018, 8, 31);

			var (actualStart, actualEnd) =
				GetSemesterBounds(yearStart, semester);

			Assert.AreEqual((start, end), (actualStart, actualEnd));
		}

		[TestMethod]
		public void YearStartShouldBeStartOfFirstSemester()
		{
			var options = new ScheduleOptions
			{
				Semesters = new List<ScheduleOptions.Semester>
				{
					new ScheduleOptions.Semester
					{
						Start = "01.09",
						End = "31.01"
					},
					new ScheduleOptions.Semester
					{
						Start = "01.02",
						End = "31.08"
					}
				}
			};

			var yearStart = GetYearStart(options, 2017);

			Assert.AreEqual(new DateTime(2017, 9, 1), yearStart);
		}
	}
}
