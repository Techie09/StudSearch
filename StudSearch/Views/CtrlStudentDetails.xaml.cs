using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudSearch.Views
{
    /// <summary>
    /// Interaction logic for StudentDetails.xaml
    /// </summary>
    public partial class CtrlStudentDetails : UserControl
    {
        public CtrlStudentDetails()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lbStudents.Items.Clear(); //Clear our student list box every time the search button is clicked.
            lbCourses.Items.Clear();  //Clears courses list box upon making a new search

            List<Student> students = Students.SearchStudentsGeneral(tbSearch.Text);
            foreach (Student student in students)
            {
                string fullName = student.lastName + ", " + student.firstName + " ID: " + student.id;
                lbStudents.Items.Add(fullName);
            }

            if (students.Count == 0)
            {
                lbStudents.Items.Add("***No Students Records Found***");
            }

            ClearAllLabels();

        }

        public void PopulateStudentInfo(Student student)
        {
            if (student == null)
            {
                return;
            }

            lblFname.Content = student.firstName;
            lblLname.Content = student.lastName;
            lblID.Content = student.id;

            lbCourses.Items.Clear(); //Clear the course list box each time a student is selected


            List<EnrolledCourse> courses = student.courses;
            ComputeCompletion(courses); //Computes and sets completion % for each course type

            foreach (EnrolledCourse course in courses)
            {
                Course c = new Course(ObjectCache.CourseRootList.FirstOrDefault(cs => cs.CourseID == course.courseID));
                lbCourses.Items.Add(c.name);
            }
        }

        void lbStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClearAllLabels();
            string selection;

            //Workaround for a crash that occured when the ctrl key was being held while selecting a student.
            try
            {
                selection = lbStudents.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                lbCourses.Items.Clear();
                return;
            }

            string[] substrings = selection.Split(' ');
            string ID = substrings[3];
            Student student = Student.GetStudentById(ID);
            if (student == null)
            {
                return;
            }

            PopulateStudentInfo(student);

        }

        private void lbCourses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selection;

            //Workaround for a crash that occured when the ctrl key was being held while selecting a course.
            try
            {
                selection = lbCourses.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                return;
            }

            string studentID = lblID.Content.ToString();
            Student student = Student.GetStudentById(studentID);
            List<EnrolledCourse> courses = student.courses;
            var courseInfo = from c in courses
                             where c.info.name == selection
                             select c;
            foreach (EnrolledCourse course in courseInfo)
            {
                lblCourseId.Content = course.courseID;
                lblCourseNam.Content = course.info.name;
                lblCourseNum.Content = course.info.number;
                lblCourseCred.Content = course.info.credits;
                lblSemster.Content = course.semester;
                lblYear.Content = course.year;
                lblCourseType.Content = course.info.courseType;
                lblCourseGrade.Content = course.grade;

            }

        }

        public void ComputeCompletion(List<EnrolledCourse> courses)
        {

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

            // Calcuates and rounds the % completion of each type to the nearest hundreth and sets it to the label
            lblElective.Content = Math.Round(((electivesCompleted.ToList().Count / 8f) * 100), 2);
            lblCore.Content = Math.Round(((coreCompleted.ToList().Count / 26f) * 100), 2);
            lblGenEd.Content = Math.Round(((genEdCompleted.ToList().Count / 8f) * 100), 2);

            lblCompleted.Content = Math.Round(((coursesCompleted.ToList().Count / 42f) * 100), 2);

        }


        public void ClearAllLabels()
        {
            lblCompleted.Content = "";
            lblCore.Content = "";
            lblCourseCred.Content = "";
            lblCourseGrade.Content = "";
            lblCourseId.Content = "";
            lblCourseNam.Content = "";
            lblCourseNum.Content = "";
            lblCourseType.Content = "";
            lblElective.Content = "";
            lblFname.Content = "";
            lblGenEd.Content = "";
            lblID.Content = "";
            lblLname.Content = "";
            lblSemster.Content = "";
            lblYear.Content = "";
        }
    }
}
