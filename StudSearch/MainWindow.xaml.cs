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
            lbStudents.Items.Clear();

            List<Student> students = Students.SearchStudentsGeneral(tbSearch.Text);
            foreach (Student student in students)
            {
                string fullName = student.lastName + ", " + student.firstName + " ID: " + student.id; 
                lbStudents.Items.Add(fullName);
            }

            
        }

        private void lbStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selection = lbStudents.SelectedItem.ToString();
            string[] substrings = selection.Split(' ');
            string ID = substrings[3];
            Student student = Student.GetStudentById(ID);

            lblFname.Content = student.firstName;
            lblLname.Content = student.lastName;
            lblID.Content = student.id;

            lbCourses.Items.Clear();

            List<EnrolledCourse> courses = student.courses;
            foreach(EnrolledCourse course in courses)
            {
                Course c = new Course(ObjectCache.CourseRootList.FirstOrDefault(cs => cs.CourseID == course.courseID));
                lbCourses.Items.Add(c.name);
            }
        }
    }
}
