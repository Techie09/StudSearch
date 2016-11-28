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
            var ctrlStudentDetails = tab.Content;

            //If student is not null, set student, and load values
        }

        private void tabStudentOverview_GotFocus(object sender, RoutedEventArgs e)
        {
            var tab = (sender as TabItemExt);
            var ctrlStudentOverview = tab.Content;

            //If student is not null, set student, and load values
        }

        private void tabStudentDetails_LostFocus(object sender, RoutedEventArgs e)
        {
            var tab = (sender as TabItemExt);
            var ctrlStudentDetails = tab.Content;

            //set student to focused student, or null if non-selected
        }

        private void tabStudentOverview_LostFocus(object sender, RoutedEventArgs e)
        {
            var tab = (sender as TabItemExt);
            var ctrlStudentOverview = tab.Content;

            //set student to focused student, or null if non-selected
        }
    }
}
