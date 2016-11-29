using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudSearch
{
    public static class CompletionProgress
    {
        public static CompletionPercentage ComputeCompletion(List<EnrolledCourse> courses)
        {
            CompletionPercentage completionPercentage = new CompletionPercentage();

            var coursesCompleted = from course in courses
                                   where course.grade == LetterGrade.A
                                   || course.grade == LetterGrade.B
                                   || course.grade == LetterGrade.C
                                   select course;

            var electivesCompleted = from course in coursesCompleted
                                     where course.info.courseType.Equals(CourseTypes.ELECTIVE.ToString(), StringComparison.OrdinalIgnoreCase)
                                     select course;

            var coreCompleted = from course in coursesCompleted
                                where course.info.courseType.Equals(CourseTypes.CORE.ToString(), StringComparison.OrdinalIgnoreCase)
                                select course;

            var genEdCompleted = from course in coursesCompleted
                                 where course.info.courseType.Equals(CourseTypes.GEN_ED.ToString(), StringComparison.OrdinalIgnoreCase)
                                 select course;


            completionPercentage.Elective = Math.Round(((electivesCompleted.ToList().Count / 8f) * 100), 2);
            completionPercentage.Core = Math.Round(((coreCompleted.ToList().Count / 26f) * 100), 2);
            completionPercentage.GenEd = Math.Round(((genEdCompleted.ToList().Count / 8f) * 100), 2);
            completionPercentage.Total = Math.Round(((coursesCompleted.ToList().Count / 42f) * 100), 2);

            return completionPercentage;
        }
    }

    public class CompletionPercentage
    {
        double core;
        public double Core
        {
            get { return core; }
            set { core = value; }
        }

        double elective;
        public double Elective
        {
            get { return elective; }
            set { elective = value; }
        }

        double genEd;
        public double GenEd
        {
            get { return genEd; }
            set { genEd = value; }
        }

        double total;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
