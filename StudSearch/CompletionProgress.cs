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
                                 where course.info.courseType.Contains(CourseTypes.GEN_ED.ToString())
                                 select course;


            completionPercentage.Elective = Math.Round(((electivesCompleted.ToList().Count / 8f) * 100), 2);
            completionPercentage.Core = Math.Round(((coreCompleted.ToList().Count / 26f) * 100), 2);
            completionPercentage.GenEd = Math.Round(((genEdCompleted.ToList().Count / 8f) * 100), 2);
            completionPercentage.Total = Math.Round(((coursesCompleted.ToList().Count / 42f) * 100), 2);

            return completionPercentage;
        }

        public static CompletionPercentage AveragePercentage(List<CompletionPercentage> percentages)
        {
            CompletionPercentage average = new CompletionPercentage();

            foreach (CompletionPercentage percentage in percentages)
            {

                average.Elective += percentage.Elective;
                average.Core += percentage.Core;
                average.GenEd += percentage.GenEd;
                average.Total += percentage.Total;
            }            

            average.Total = Math.Round(average.Total / percentages.Count, 2);
            average.Elective = Math.Round(average.Elective / percentages.Count, 2);
            average.GenEd = Math.Round(average.GenEd / percentages.Count, 2);
            average.Core = Math.Round(average.Core / percentages.Count, 2);

            return average;
        }
    }

    public class CompletionPercentage
    {
        double core = 0;
        public double Core
        {
            get { return core; }
            set { core = value; }
        }

        double elective = 0;
        public double Elective
        {
            get { return elective; }
            set { elective = value; }
        }

        double genEd = 0;
        public double GenEd
        {
            get { return genEd; }
            set { genEd = value; }
        }

        double total = 0;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }
        public CompletionPercentage()
        {
            this.core = 0;
            this.elective = 0;
            this.genEd = 0;
            this.total = 0;
        }

        public CompletionPercentage(double core, double elective, double genEd, double total)
        {
            this.core = core;
            this.elective = elective;
            this.genEd = genEd;
            this.total = total;
        }
    }
}
