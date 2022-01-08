using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
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
    public class Course : INotifyPropertyChanged
    {
        public long Id { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private long teacher;
        public long Teacher
        {
            get => teacher;
            set
            {
                if (teacher != value)
                {
                    teacher = value;
                    OnPropertyChanged("Teacher");
                }
            }
        }

        private string teacherName;
        public string TeacherName
        {
            get => teacherName;
            set
            {
                if (teacherName != value)
                {
                    teacherName = value;
                    OnPropertyChanged("TeacherName");
                }
            }
        }

        private double credits;
        public double Credits
        {
            get => credits;
            set
            {
                if (credits != value)
                {
                    credits = value;
                    OnPropertyChanged("Credits");
                }
            }
        }

        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public int StudentNum { get; set; }
        public double AverageScore { get; set; }
        public double StandardDeviation { get; set; }
        public double PassRate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /// <summary>
    /// Interaction logic for CourseManage.xaml
    /// </summary>
    public partial class CourseManage : Page
    {
        List<Course> courses;

        void SqlToDataGrid(string command)
        {
            SQLiteCommand cmd = new(command, DB.con);
            SqlToDataGrid(cmd);
        }

        void SqlToDataGrid(SQLiteCommand cmd)
        {
            SQLiteDataReader reader = cmd.ExecuteReader();

            courses = new();
            while (reader.Read())
            {
                courses.Add(new Course
                {
                    Id = reader.GetInt64(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Teacher = reader.IsDBNull(2) ? 0 : reader.GetInt64(2),
                    TeacherName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Credits = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                    Type = reader.IsDBNull(5) ? "" : (reader.GetInt32(5) == 0 ? "必修" : "选修"),
                    StudentNum = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                    AverageScore = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                    StandardDeviation = reader.IsDBNull(8) ? 0 : Math.Sqrt(reader.GetDouble(8)),
                    PassRate = reader.IsDBNull(9) ? 0 : reader.GetDouble(9)
                });
            }
            dgCourse.ItemsSource = courses;
        }

        private void cmdRefresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SqlToDataGrid(
@"SELECT Course.id, Course.name, teacher, teacher.name teacher_name, credits, type, A.student_num, A.avg_score, A.variance, CAST(B.pass_num AS REAL)/A.student_num pass_rate FROM Course
LEFT JOIN Teacher ON Teacher.id=teacher
LEFT JOIN
	(SELECT CourseTaking.course, COUNT(*) student_num, C.avg_score, AVG((score-C.avg_score)*(score-C.avg_score)) variance
		FROM CourseTaking
		JOIN
			(SELECT course, AVG(score) avg_score FROM CourseTaking GROUP BY course) C
			ON CourseTaking.course=C.course
	GROUP BY CourseTaking.course) A
	ON Course.id=A.course
LEFT JOIN
	(SELECT course, COUNT(*) pass_num FROM CourseTaking WHERE score>60 GROUP BY course) B
	ON Course.id=B.course;");
        }

        public CourseManage()
        {
            InitializeComponent();
            cmdRefresh_Executed(null, null);
        }

        private void dgCourse_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Course course = (Course)e.Row.Item;
            SQLiteCommand cmd = new(DB.con);
            cmd.Parameters.AddWithValue("@course", course.Id);
            cmd.Parameters.AddWithValue("@teacher", course.Teacher);
            SQLiteDataReader reader;

            // 检验输入合法性
            cmd.CommandText = "SELECT COUNT(*) FROM Teacher WHERE id=@teacher";
            if (course.Type is not "必修" and not "选修" || (long)cmd.ExecuteScalar() == 0)
            {
                cmd.CommandText = "SELECT name, teacher, credits, type FROM Course WHERE id=@course";
                reader = cmd.ExecuteReader();
                reader.Read();
                //course.Name = reader.IsDBNull(0) ? "" : reader.GetString(0);  // #TODO
                course.Teacher = reader.IsDBNull(1) ? 0 : reader.GetInt64(1);
                course.Credits = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
                course.Type = reader.IsDBNull(3) ? "" : (reader.GetInt32(3) == 0 ? "必修" : "选修");
                return;
            }

            // 更新
            cmd.CommandText = "UPDATE Course SET name=@name, teacher=@teacher, credits=@credits, type=@type WHERE id=@course";
            cmd.Parameters.AddWithValue("@name", course.Name);
            cmd.Parameters.AddWithValue("@credits", course.Credits);
            cmd.Parameters.AddWithValue("@type", course.Type == "必修" ? 0 : 1);
            cmd.ExecuteNonQuery();

            // 刷新被参照属性
            if (course.Teacher != 0)
            {
                cmd.CommandText = "SELECT Teacher.name FROM Course JOIN Teacher ON Teacher.id=teacher WHERE Course.id=@course";
                reader = cmd.ExecuteReader();
                reader.Read();
                course.TeacherName = reader.IsDBNull(0) ? "" : reader.GetString(0);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            long id;
            if (!long.TryParse(editId.Text, out id))
            {
                MessageBox.Show("课程号格式错误！");
                return;
            }

            SQLiteCommand cmd = new("SELECT COUNT(*) FROM Course WHERE id=@id", DB.con);
            cmd.Parameters.AddWithValue("@id", id);
            if ((long)cmd.ExecuteScalar() != 0)
            {
                MessageBox.Show("该课程号已存在！");
                return;
            }

            cmd.CommandText = "INSERT INTO Course VALUES (@id,NULL,NULL,NULL,NULL)";
            cmd.ExecuteNonQuery();

            courses.Add(new Course { Id = id });
            dgCourse.ItemsSource = null;
            dgCourse.ItemsSource = courses;

            editId.Text = "";
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            Course course = (Course)dgCourse.SelectedItem;
            if (course != null)
            {
                SQLiteCommand cmd = new("DELETE FROM Course WHERE id=@course", DB.con);
                cmd.Parameters.AddWithValue("@course", course.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                    courses.Remove(course);
                    dgCourse.ItemsSource = null;
                    dgCourse.ItemsSource = courses;
                }
                catch (SQLiteException)
                {
                    MessageBox.Show("删除失败，请先移除对该课程的所有引用！");
                }
            }
        }
    }
}
