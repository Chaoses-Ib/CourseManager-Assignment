using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public class CourseScore : INotifyPropertyChanged
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public string Major { get; set; }
        public string Department { get; set; }

        private string score;
        public string Score {
            get => score;
            set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChanged("Score");
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
    /// Interaction logic for TeacherCourseScore.xaml
    /// </summary>
    public partial class TeacherCourseScore : Page
    {
        long courseId;
        List<CourseScore> courseScores;

        public TeacherCourseScore()
        {
            InitializeComponent();
        }

        private void cmdRefresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            long newCourseId;
            if (!long.TryParse(editId.Text, out newCourseId))
            {
                MessageBox.Show("课程号格式错误！");
                return;
            }

            SQLiteCommand cmd = new("SELECT Course.name, teacher, Teacher.name, credits, type FROM Course LEFT JOIN Teacher ON teacher=Teacher.id WHERE Course.id=@course;", DB.con);
            cmd.Parameters.AddWithValue("@course", newCourseId);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("该课程号不存在！");
                return;
            }
            labelInfo.Content = string.Format("课程名：{0}   教师名：{1}   学分：{2}   类别：{3}",
                reader.IsDBNull(0) ? "" : reader.GetString(0),
                reader.IsDBNull(2) ? "" : reader.GetString(2),
                reader.IsDBNull(3) ? "" : string.Format("{0:0}", reader.GetDouble(3)),
                reader.IsDBNull(4) ? "" : reader.GetInt32(4) == 0 ? "必修" : "选修");
            reader.Close();

            cmd.CommandText =
@"SELECT Student.id, name, gender, class, major, department, score
FROM CourseTaking
LEFT JOIN Student ON student=Student.id
LEFT JOIN Class ON class=Class.id
WHERE course=@course;";
            reader = cmd.ExecuteReader();

            courseScores = new();
            while (reader.Read())
            {
                courseScores.Add(new CourseScore
                {
                    Id = reader.GetInt64(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Gender = reader.IsDBNull(2) ? "" : reader.GetInt32(2) == 0 ? "男" : "女",
                    Class = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Major = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Department = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    Score = reader.IsDBNull(6) ? "" : string.Format($"{reader.GetDouble(6) :0.#}")
                });
            }
            dgCourseScore.ItemsSource = courseScores;

            courseId = newCourseId;
        }

        private void dgCourseScore_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            CourseScore courseScore = (CourseScore)e.Row.Item;
            SQLiteCommand cmd = new(DB.con);
            cmd.Parameters.AddWithValue("@student", courseScore.Id);
            cmd.Parameters.AddWithValue("@course", courseId);

            // 检验输入合法性
            double score;
            if (!double.TryParse(courseScore.Score, out score))
            {
                cmd.CommandText = "SELECT score FROM CourseTaking WHERE student=@student AND course=@course";
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                courseScore.Score = reader.IsDBNull(0) ? "" : string.Format($"{reader.GetDouble(0):0.#}");
                return;
            }

            // 更新分数
            cmd.CommandText = "UPDATE CourseTaking SET score=@score WHERE student=@student AND course=@course";
            cmd.Parameters.AddWithValue("@score", score);
            cmd.ExecuteNonQuery();
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            SQLiteCommand cmd = new("DELETE FROM CourseTaking WHERE student=@student AND course=@course", DB.con);
            cmd.Parameters.AddWithValue("@course", courseId);
            cmd.Parameters.Add("@student", DbType.Int64);
            try
            {
                foreach (object obj in dgCourseScore.SelectedItems)  // CourseScore or MS.Internal.NamedObject
                {
                    CourseScore courseScore = obj as CourseScore;
                    if (courseScore == null)
                        continue;
                    cmd.Parameters["@student"].Value = courseScore.Id;
                    cmd.ExecuteNonQuery();
                    courseScores.Remove(courseScore);
                }
            }
            catch (SQLiteException)
            {
                MessageBox.Show("删除失败，请先移除对该成绩的所有引用！");
                return;
            }
            dgCourseScore.ItemsSource = null;
            dgCourseScore.ItemsSource = courseScores;
        }
    }
}