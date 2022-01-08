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

namespace CourseManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StudentCourse studentCourse = new();
        TeacherCourseScore teacherCourseScore = new();
        StudentManage studentManage = new();
        ClassManage classManage = new();
        CourseManage courseManage = new();
        TeacherManage teacherManage = new();
        DatabaseMaintain databaseMaintain = new();

        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(studentCourse);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StudentCourse_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(studentCourse);
        }

        private void TeacherCourseScore_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(teacherCourseScore);
        }

        private void StudentManage_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(studentManage);
        }

        private void ClassManage_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(classManage);
        }

        private void CourseManage_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(courseManage);
        }

        private void TeacherManage_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(teacherManage);
        }

        private void DatabaseMaintain_Selected(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(databaseMaintain);
        }
    }
}