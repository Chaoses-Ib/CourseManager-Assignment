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
    public class Teacher : INotifyPropertyChanged
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

        private string gender;
        public string Gender {
            get => gender;
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        private string department;
        public string Department
        {
            get => department;
            set
            {
                if (department != value)
                {
                    department = value;
                    OnPropertyChanged("Department");
                }
            }
        }

        public double AverageScore { get; set; }

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
    /// Interaction logic for TeacherManage.xaml
    /// </summary>
    public partial class TeacherManage : Page
    {
        List<Teacher> teachers;

        void SqlToDataGrid(string command)
        {
            SQLiteCommand cmd = new(command, DB.con);
            SqlToDataGrid(cmd);
        }

        void SqlToDataGrid(SQLiteCommand cmd)
        {
            SQLiteDataReader reader = cmd.ExecuteReader();

            teachers = new();
            while (reader.Read())
            {
                teachers.Add(new Teacher
                {
                    Id = reader.GetInt64(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Gender = reader.IsDBNull(2) ? "" : (reader.GetInt32(2) == 0 ? "男" : "女"),
                    Department = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    AverageScore = reader.IsDBNull(4) ? 0 : reader.GetDouble(4)
                });
            }
            dgTeacher.ItemsSource = teachers;
        }

        private void cmdRefresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SqlToDataGrid(
@"SELECT id, name, gender, department, avg_score FROM Teacher
LEFT JOIN
	(SELECT teacher, AVG(teacher_score) avg_score FROM CourseTaking JOIN Course ON course=Course.id GROUP BY teacher)
	ON teacher=id;");
        }

        public TeacherManage()
        {
            InitializeComponent();
            cmdRefresh_Executed(null, null);
        }

        private void dgTeacher_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Teacher teacher = (Teacher)e.Row.Item;
            SQLiteCommand cmd = new(DB.con);
            cmd.Parameters.AddWithValue("@teacher", teacher.Id);

            // 检验输入合法性
            if (teacher.Gender is not "男" and not "女")
            {
                cmd.CommandText = "SELECT name, gender, department FROM Teacher WHERE id=@teacher";
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                teacher.Name = reader.IsDBNull(0) ? "" : reader.GetString(0);
                teacher.Gender = reader.IsDBNull(1) ? "" : (reader.GetInt32(1) == 0 ? "男" : "女");
                teacher.Department = reader.IsDBNull(2) ? "" : reader.GetString(2);
                return;
            }

            cmd.CommandText = "UPDATE Teacher SET name=@name, gender=@gender, department=@department WHERE id=@teacher";
            cmd.Parameters.AddWithValue("@name", teacher.Name);
            cmd.Parameters.AddWithValue("@gender", teacher.Gender == "男" ? 0 : 1);
            cmd.Parameters.AddWithValue("@department", teacher.Department);
            cmd.ExecuteNonQuery();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            long id;
            if (!long.TryParse(editId.Text, out id))
            {
                MessageBox.Show("教师号格式错误！");
                return;
            }

            SQLiteCommand cmd = new("SELECT COUNT(*) FROM Teacher WHERE id=@teacher", DB.con);
            cmd.Parameters.AddWithValue("@teacher", id);
            if ((long)cmd.ExecuteScalar() != 0)
            {
                MessageBox.Show("该教师号已存在！");
                return;
            }

            cmd.CommandText = "INSERT INTO Teacher VALUES (@teacher,NULL,NULL,NULL)";
            cmd.ExecuteNonQuery();

            teachers.Add(new Teacher { Id = id });
            dgTeacher.ItemsSource = null;
            dgTeacher.ItemsSource = teachers;

            editId.Text = "";
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            Teacher teacher = (Teacher)dgTeacher.SelectedItem;
            if (teacher != null)
            {
                SQLiteCommand cmd = new("DELETE FROM Teacher WHERE id=@teacher", DB.con);
                cmd.Parameters.AddWithValue("@teacher", teacher.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                    teachers.Remove(teacher);
                    dgTeacher.ItemsSource = null;
                    dgTeacher.ItemsSource = teachers;
                }
                catch (SQLiteException)
                {
                    MessageBox.Show("删除失败，请先移除对该教师的所有引用！");
                }
            }
        }
    }
}