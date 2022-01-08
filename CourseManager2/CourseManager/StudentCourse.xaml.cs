using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Globalization;
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
    public class CourseTaking : INotifyPropertyChanged
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public double Credits { get; set; }
        public string Type { get; set; }

        private string taking;
        public string Taking
        {
            get => taking;
            set
            {
                if (taking != value)
                {
                    taking = value;
                    OnPropertyChanged("Taking");
                }
            }
        }

        public string Score { get; set; }
        public string TeacherName { get; set; }

        private string teacherScore;
        public string TeacherScore
        {
            get => teacherScore;
            set
            {
                if (teacherScore != value)
                {
                    teacherScore = value;
                    OnPropertyChanged("TeacherScore");
                }
            }
        }

        private string teacherRemark;
        public string TeacherRemark {
            get => teacherRemark;
            set
            {
                if (teacherRemark != value)
                {
                    teacherRemark = value;
                    OnPropertyChanged("TeacherRemark");
                }
            }
        }

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
    /// Interaction logic for StudentCourse.xaml
    /// </summary>
    public partial class StudentCourse : Page
    {
        long studentId;
        List<CourseTaking> courseTakings;

        private void cmdRefresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            long newStudentId;
            if (!long.TryParse(editId.Text, out newStudentId))
            {
                MessageBox.Show("学号格式错误！");
                return;
            }

            // 查询学生个人信息
            // 学分绩点查询 SQL：
            // SELECT score, CAST((score-60)/5 AS INT)/2.0+1 gp FROM CourseTaking JOIN Course ON course=Course.id WHERE student=20081003494 AND score >= 60
            SQLiteCommand cmd = new(
@"SELECT name, gender, class, major, department, take_credits, total_credits, gpa
FROM Student
LEFT JOIN Class ON class=Class.id,
(SELECT SUM(credits) take_credits FROM CourseTaking JOIN Course ON course=Course.id WHERE student=@student),
(SELECT SUM(credits) total_credits, SUM(credits*(CAST((score-60)/5 AS INT)/2.0+1))/SUM(credits) gpa FROM CourseTaking JOIN Course ON course=Course.id WHERE student=@student AND score >= 60)
WHERE Student.id=@student;", DB.con);
            cmd.Parameters.AddWithValue("@student", newStudentId);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("该学号不存在！");
                return;
            }
            labelInfo.Content = string.Format("姓名：{0}   班级：{1}   专业：{2}   学院：{3}学院\n已选学分：{4:0.#}   已修学分：{5:0.#}   平均学分绩点：{6:0.00}",
                reader.IsDBNull(0) ? "" : reader.GetString(0),
                reader.IsDBNull(2) ? "" : reader.GetString(2),
                reader.IsDBNull(3) ? "" : reader.GetString(3),
                reader.IsDBNull(4) ? "" : reader.GetString(4),

                reader.GetDouble(5),
                reader.GetDouble(6),
                reader.GetDouble(7));
            reader.Close();

            // 查询课程及学生选课信息
            cmd.CommandText =
@"SELECT Course.id, Course.name, credits, type, course IS NOT NULL taking, score, Teacher.name teacher_name, teacher_score, teacher_remark
FROM Course
LEFT JOIN (SELECT * FROM CourseTaking WHERE student=@student) ON course=Course.id
LEFT JOIN Teacher ON teacher=teacher.id;";
            reader = cmd.ExecuteReader();

            courseTakings = new();
            while (reader.Read())
            {
                courseTakings.Add(new CourseTaking
                {
                    Id = reader.GetInt64(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Credits = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                    Type = reader.IsDBNull(3) ? "" : reader.GetInt32(3) == 0 ? "必修" : "选修",
                    Taking = reader.GetInt32(4) == 0 ? "" : "已选",
                    Score = reader.IsDBNull(5) ? "" : string.Format($"{reader.GetDouble(5):0.#}"),
                    TeacherName = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    TeacherScore = reader.IsDBNull(7) ? "" : string.Format($"{reader.GetDouble(7):0.#}"),
                    TeacherRemark = reader.IsDBNull(8) ? "" : reader.GetString(8)
                });
            }
            dgCourseTaking.ItemsSource = courseTakings;

            studentId = newStudentId;
        }

        public StudentCourse()
        {
            InitializeComponent();
        }

        private void dgCourseTaking_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            CourseTaking courseTaking = (CourseTaking)e.Row.Item;

            // 未选课程禁止评价
            if (courseTaking.Taking == "")
            {
                courseTaking.TeacherScore = courseTaking.TeacherRemark = "";
                return;
            }

            SQLiteCommand cmd = new(DB.con);
            cmd.Parameters.AddWithValue("@student", studentId);
            cmd.Parameters.AddWithValue("@course", courseTaking.Id);

            // 检验输入合法性
            double score;
            if (!double.TryParse(courseTaking.TeacherScore, out score))
            {
                cmd.CommandText = "SELECT teacher_score, teacher_remark FROM CourseTaking WHERE student=@student AND course=@course";
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                courseTaking.TeacherScore = reader.IsDBNull(0) ? "" : string.Format($"{reader.GetDouble(0):0.#}");
                courseTaking.TeacherRemark = reader.IsDBNull(1) ? "" : reader.GetString(1);
                return;
            }
            
            // 更新评价信息
            cmd.CommandText = "UPDATE CourseTaking SET teacher_score=@score, teacher_remark=@remark WHERE student=@student AND course=@course";
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@remark", courseTaking.TeacherRemark);
            cmd.ExecuteNonQuery();
        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            CourseTaking courseTaking = (CourseTaking)dgCourseTaking.SelectedItem;
            if (courseTaking != null)
            {
                menuJoin.IsEnabled = courseTaking.Taking == "";  // 未选课
                menuExit.IsEnabled = courseTaking.Taking != "" && courseTaking.Score == "";  // 选课但未结课时可退选
            }
        }

        private void menuJoin_Click(object sender, RoutedEventArgs e)
        {
            CourseTaking courseTaking = (CourseTaking)dgCourseTaking.SelectedItem;
            if (courseTaking != null)
            {
                SQLiteCommand cmd = new("INSERT INTO CourseTaking VALUES (@student, @course, NULL, NULL, NULL)", DB.con);
                cmd.Parameters.AddWithValue("@student", studentId);
                cmd.Parameters.AddWithValue("@course", courseTaking.Id);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    courseTaking.Taking = "已选";
                }
            }
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            CourseTaking courseTaking = (CourseTaking)dgCourseTaking.SelectedItem;
            if (courseTaking != null)
            {
                SQLiteCommand cmd = new("DELETE FROM CourseTaking WHERE student=@student AND course=@course", DB.con);
                cmd.Parameters.AddWithValue("@student", studentId);
                cmd.Parameters.AddWithValue("@course", courseTaking.Id);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    courseTaking.Taking = "";
                }
            }
        }
    }

    public class IsScoreFailConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsScoreFailConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value != "" && double.Parse((string)value) < 60;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsScoreExcellentConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsScoreExcellentConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value != "" && double.Parse((string)value) >= 90;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}