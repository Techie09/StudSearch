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
    /// Interaction logic for StudentOverview.xaml
    /// </summary>
    public partial class CtrlStudentOverview : UserControl
    {
        public CtrlStudentOverview()
        {
            InitializeComponent();
        }

        private void grdStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student student = (grdStudents.CurrentItem as Student);
            CompletionPercentage studentCompletion = CompletionProgress.ComputeCompletion(student.courses);

            bulletCorePercent.FeaturedMeasure = studentCompletion.Core;

            bulletElectivePercent.FeaturedMeasure = studentCompletion.Elective;

            bulletGenEdPercent.FeaturedMeasure = studentCompletion.GenEd;
        }

        public void SetBulletFeaturedMeasures(CompletionPercentage studentCompletion)
        {
            bulletCorePercent.FeaturedMeasure = studentCompletion.Core;
            bulletElectivePercent.FeaturedMeasure = studentCompletion.Elective;
            bulletGenEdPercent.FeaturedMeasure = studentCompletion.GenEd;
        }

        public void SetBulletComparativeMeasures(CompletionPercentage avgCompletion)
        {
            bulletCorePercent.ComparativeMeasure = avgCompletion.Core;
            bulletElectivePercent.ComparativeMeasure = avgCompletion.Elective;
            bulletGenEdPercent.ComparativeMeasure = avgCompletion.GenEd;
        }
    }
}
