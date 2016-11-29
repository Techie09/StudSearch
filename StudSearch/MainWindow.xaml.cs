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

        public MainWindow()
        {
            InitializeComponent();            
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
                ctrlStudentOverview.bulletCorePercent.ComparativeMeasure = 0;
                ctrlStudentOverview.bulletElectivePercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletElectivePercent.ComparativeMeasure = 0;
                ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
            }
        }

        private void tabStudentDetails_LostFocus(object sender, RoutedEventArgs e)
        {
            return;
            var tab = (sender as TabItemExt);
            CtrlStudentDetails ctrlStudentDetails = (tab.Content as CtrlStudentDetails);

            //set student to focused student, or null if non-selected
            string selection = ctrlStudentDetails.lbStudents.SelectedItem.ToString();
            string[] substrings = selection.Split(' ');
            if (substrings.Length < 3)
            {
                student = null;
                ctrlStudentDetails.tbSearch.Text = String.Empty;
                ctrlStudentDetails.lbStudents.Items.Clear();
                ctrlStudentDetails.lbCourses.Items.Clear();
                ctrlStudentDetails.ClearAllLabels();
            }
            else
            {
                string ID = substrings[3];
                student = Student.GetStudentById(ID);
            }
        }

        private void tabStudentOverview_LostFocus(object sender, RoutedEventArgs e)
        {
            return;
            var tab = (sender as TabItemExt);
            CtrlStudentOverview ctrlStudentOverview = (tab.Content as CtrlStudentOverview);

            //set student to focused student, or null if non-selected
            if (ctrlStudentOverview.grdStudents.SelectedItem == null)
            {
                student = null;
                ctrlStudentOverview.bulletCorePercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletCorePercent.ComparativeMeasure = 0;
                ctrlStudentOverview.bulletElectivePercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletElectivePercent.ComparativeMeasure = 0;
                ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
                ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
            }
            else
            {
                student = (ctrlStudentOverview.grdStudents.SelectedItem as Student);
            }
        }

        private void tabCtrlManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItemExt selectedItem = ((sender as TabControlExt).SelectedItem as TabItemExt);
            if (selectedItem == null)
                return;

            if(selectedItem.Content.GetType() == typeof(CtrlStudentDetails))
            {
                CtrlStudentDetails ctrlStudentDetails = (selectedItem.Content as CtrlStudentDetails);

                //set student to focused student, or null if non-selected
                if (ctrlStudentDetails?.lbStudents?.SelectedItem == null)
                {
                    student = null;
                    ctrlStudentDetails.tbSearch.Text = String.Empty;
                    ctrlStudentDetails.lbStudents.Items.Clear();
                    ctrlStudentDetails.lbCourses.Items.Clear();
                    ctrlStudentDetails.ClearAllLabels();
                }
                else
                {
                    student = (ctrlStudentDetails.lbStudents.SelectedItem as Student);
                }
            }
            else if(selectedItem.Content.GetType() == typeof(CtrlStudentOverview))
            {
                CtrlStudentOverview ctrlStudentOverview = (selectedItem.Content as CtrlStudentOverview);

                //set student to focused student, or null if non-selected
                if (ctrlStudentOverview?.grdStudents?.SelectedItem == null)
                {
                    student = null;
                    ctrlStudentOverview.bulletCorePercent.FeaturedMeasure = 0;
                    ctrlStudentOverview.bulletCorePercent.ComparativeMeasure = 0;
                    ctrlStudentOverview.bulletElectivePercent.FeaturedMeasure = 0;
                    ctrlStudentOverview.bulletElectivePercent.ComparativeMeasure = 0;
                    ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
                    ctrlStudentOverview.bulletGenEdPercent.FeaturedMeasure = 0;
                }
                else
                {
                    student = (ctrlStudentOverview.grdStudents.SelectedItem as Student);
                }
            }
        }
    }
}
