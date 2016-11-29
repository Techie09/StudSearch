using StudSearch.Views;
using Syncfusion.Windows.Tools.Controls;
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
        Student student;
        CompletionPercentage avgPercentage;
        List<Student> allStudents;
        List<CompletionPercentage> allPercentages = new List<CompletionPercentage>();

        public MainWindow()
        {
            InitializeComponent();
            allStudents = Students.SearchStudentsGeneral("");

            foreach (Student student in allStudents)
            {
                if (student.courses == null)
                    continue;
                allPercentages.Add(CompletionProgress.ComputeCompletion(student.courses));
            }

            avgPercentage = CompletionProgress.AveragePercentage(allPercentages);
    
        }

        private void tabStudentDetails_GotFocus(object sender, RoutedEventArgs e)
        {
            var tab = (sender as TabItemExt);
            CtrlStudentDetails ctrlStudentDetails = (tab.Content as CtrlStudentDetails);

            //If student is not null, set student, and load values
        }

        private void tabStudentOverview_GotFocus(object sender, RoutedEventArgs e)
        {
            var tab = (sender as TabItemExt);
            CtrlStudentOverview ctrlStudentOverview = (tab.Content as CtrlStudentOverview);

            //If student is not null, set student, and load values
            if (student == null)
            {
                ctrlStudentOverview.bulletCorePercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletElectivePercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
            }
            else
            {
                if(ctrlStudentOverview.grdStudents.ItemsSource == null)
                    ctrlStudentOverview.grdStudents.ItemsSource = Students.SearchStudentsGeneral("");

                CompletionPercentage studentCompletion = CompletionProgress.ComputeCompletion(student.courses);

                ctrlStudentOverview.bulletCorePercent.FeaturedMeasure = studentCompletion.Core;
                ctrlStudentOverview.bulletCorePercent.ComparativeMeasure = avgPercentage.Core;

                ctrlStudentOverview.bulletElectivePercent.FeaturedMeasure = studentCompletion.Elective;
                ctrlStudentOverview.bulletElectivePercent.ComparativeMeasure = avgPercentage.Elective;

                ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = studentCompletion.GenEd;
                ctrlStudentOverview.bulletGenEdPercent.ComparativeMeasure = avgPercentage.GenEd;
            }
        }     

        private void tabCtrlManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl selectedItem = ((sender as TabControlExt).SelectedContent as UserControl);
            if (selectedItem == null)
                return;

            if(selectedItem.GetType() == typeof(CtrlStudentDetails))
            {
                CtrlStudentDetails ctrlStudentDetails = (selectedItem as CtrlStudentDetails);

                //set student to focused student, or null if non-selected
                if (ctrlStudentDetails.lbStudents.SelectedItem == null)
                {
                    student = null;
                    ctrlStudentDetails.tbSearch.Text = String.Empty;
                    ctrlStudentDetails.lbStudents.Items.Clear();
                    ctrlStudentDetails.lbCourses.Items.Clear();
                    ctrlStudentDetails.ClearAllLabels();
                }
                else
                {
                    string[] substrings = ctrlStudentDetails.lbStudents.SelectedItem.ToString().Split(' ');
                    string ID = substrings[3];
                    student = Student.GetStudentById(ID);
                }
            }
            else if(selectedItem.GetType() == typeof(CtrlStudentOverview))
            {
                CtrlStudentOverview ctrlStudentOverview = (selectedItem as CtrlStudentOverview);

                //set student to focused student, or null if non-selected
                if (ctrlStudentOverview?.grdStudents?.SelectedItem == null)
                {
                    student = null;
                    ctrlStudentOverview.bulletCorePercent.FeaturedMeasure = 0;
                    ctrlStudentOverview.bulletElectivePercent.FeaturedMeasure = 0;
                    ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
                }
                else
                {
                    student = (ctrlStudentOverview.grdStudents.SelectedItem as Student);
                }
            }
        }

        private void tabCtrlManager_SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
