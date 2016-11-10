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

namespace StudSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lbStudents.Items.Clear(); //Clear our student list box every time the search button is clicked.

            List<Student> students = Students.SearchStudentsGeneral(tbSearch.Text);
            foreach (Student student in students)
            {
                string fullName = student.lastName + ", " + student.firstName + " ID: " + student.id; 
                lbStudents.Items.Add(fullName);
            }

            
        }

        void lbStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selection = lbStudents.SelectedItem.ToString();
            string[] substrings = selection.Split(' ');
            string ID = substrings[3];
            Student student = Student.GetStudentById(ID);

            lblFname.Content = student.firstName;
            lblLname.Content = student.lastName;
            lblID.Content = student.id;

            lbCourses.Items.Clear(); //Clear the course list box each time a student is selected
            

            List<EnrolledCourse> courses = student.courses;
            ComputeCompletion(courses); //Computes and sets completion % for each course type

            foreach(EnrolledCourse course in courses)
            {
                Course c = new Course(ObjectCache.CourseRootList.FirstOrDefault(cs => cs.CourseID == course.courseID));
                lbCourses.Items.Add(c.name);
            }
        }

        private void lbCourses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selection = lbCourses.SelectedItem.ToString();
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
            double coreCompletion = 0;
            double electiveCompletion = 0;
            double genEdCompletion = 0;
            double totCompletion = 0;
            int creditHours = 0;

            var electives = from course in courses
                            where course.info.courseType == "Elective"
                            select course;
            foreach (EnrolledCourse course in electives)
            {
                electiveCompletion++;
            }

            var core = from course in courses
                            where course.info.courseType == "Core"
                            select course;

            foreach (EnrolledCourse course in core)
            {
                coreCompletion++;
            }

            var genEd = from course in courses
                       where course.info.courseType == "General"
                       select course;

            foreach (EnrolledCourse course in genEd)
            {
                genEdCompletion++;
            }

            totCompletion = electiveCompletion + coreCompletion + genEdCompletion;

            // Calcuates and rounds the % completion of each type to the nearest hundreth and sets it to the label
            lblElective.Content = Math.Round(((electiveCompletion / 8) * 100), 2);
            lblCore.Content = Math.Round(((coreCompletion / 26) * 100), 2);
            lblGenEd.Content = Math.Round(((genEdCompletion / 8) * 100), 2);

            lblCompleted.Content = Math.Round(((totCompletion / 42) * 100), 2);

        }
    }
}
