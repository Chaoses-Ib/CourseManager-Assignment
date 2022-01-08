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
    public class Student : INotifyPropertyChanged
    {
        public long Id { get; set; }
        public string Name { get; set; }

        private string gender;
        public string Gender
        {
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

        private string class_;
        public string Class {
            get => class_;
            set
            {
                if (class_ != value)
                {
                    class_ = value;
                    OnPropertyChanged("Class");
                }
            }
        }

        private string major;
        public string Major
        {
            get => major;
            set
            {
                if (major != value)
                {
                    major = value;
                    OnPropertyChanged("Major");
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
    /// Interaction logic for StudentManage.xaml
    /// </summary>
    public partial class StudentManage : Page
    {
        List<Student> students;

        int SqlToDataGrid(string command)
        {
            SQLiteCommand cmd = new(command, DB.con);
            return SqlToDataGrid(cmd);
        }

        int SqlToDataGrid(SQLiteCommand cmd)
        {
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return 0;
            }

            students = new();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt64(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Gender = reader.IsDBNull(2) ? "" : (reader.GetInt32(2) == 0 ? "男" : "女"),
                    Class = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Major = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Department = reader.IsDBNull(5) ? "" : reader.GetString(5)
                });
            }
            dgStudent.ItemsSource = students;
            return reader.StepCount;
        }

        private void cmdRefresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SQLiteCommand cmd = new(DB.con);
            // 构建 SQL
            cmd.CommandText = "SELECT Student.id, name, gender, class, major, department FROM Student LEFT JOIN Class ON Student.class=Class.id";
            bool where = false;
            if (editSearch.Text != "")
            {
                cmd.CommandText += " WHERE Student.id=@id";
                cmd.Parameters.AddWithValue("@id", editSearch.Text);
                where = true;
            }
            if (comboClass.SelectedIndex != 0)
            {
                if (!where)
                {
                    cmd.CommandText += " WHERE ";
                    where = true;
                }
                else
                {
                    cmd.CommandText += " AND ";
                }
                cmd.CommandText += "class=@class";
                cmd.Parameters.AddWithValue("@class", comboClass.SelectedItem);
            }

            // 查找
            if (SqlToDataGrid(cmd) == 0 && editSearch.Text != "")
            {
                MessageBox.Show("未查找到该学号");
            }
        }

        public StudentManage()
        {
            InitializeComponent();

            // 填充班级过滤 ComboBox
            SQLiteCommand cmd = new("SELECT id FROM Class;", DB.con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboClass.Items.Add(reader.GetString(0));
            }

            cmdRefresh_Executed(null, null);
        }

        private void comboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStudent == null)  // not inited
                return;
            cmdRefresh_Executed(null, null);
        }

        private void dgStudent_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Student student = (Student)e.Row.Item;
            SQLiteCommand cmd = new(DB.con);
            cmd.Parameters.AddWithValue("@student", student.Id);
            cmd.Parameters.AddWithValue("@class", student.Class);
            SQLiteDataReader reader;

            // 检验输入合法性
            cmd.CommandText = "SELECT COUNT(*) FROM Class WHERE id=@class";
            if (student.Gender is not "男" and not "女" || (long)cmd.ExecuteScalar() == 0)
            {
                cmd.CommandText = "SELECT gender, class FROM Student WHERE id=@student";
                reader = cmd.ExecuteReader();
                reader.Read();
                student.Gender = reader.IsDBNull(0) ? "" : (reader.GetInt32(0) == 0 ? "男" : "女");
                student.Class = reader.IsDBNull(1) ? "" : reader.GetString(1);
                return;
            }

            // 更新
            cmd.CommandText = "UPDATE Student SET name=@name, gender=@gender, class=@class WHERE id=@student";
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@gender", student.Gender == "男" ? 0 : 1);
            cmd.ExecuteNonQuery();

            // 刷新被参照属性
            if (student.Class != "")
            {
                cmd.CommandText = "SELECT major, department FROM Class WHERE id=@class";
                reader = cmd.ExecuteReader();
                reader.Read();
                student.Major = reader.GetString(0);
                student.Department = reader.GetString(1);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            long id;
            if (!long.TryParse(editId.Text, out id))
            {
                MessageBox.Show("学号格式错误！");
                return;
            }

            SQLiteCommand cmd = new("SELECT COUNT(*) FROM Student WHERE id=@student", DB.con);
            cmd.Parameters.AddWithValue("@student", id);
            if ((long)cmd.ExecuteScalar() != 0)
            {
                MessageBox.Show("该学号已存在！");
                return;
            }

            cmd.CommandText = "INSERT INTO Student VALUES (@student,NULL,NULL,NULL)";
            cmd.ExecuteNonQuery();

            students.Add(new Student { Id = id });
            dgStudent.ItemsSource = null;
            dgStudent.ItemsSource = students;

            editId.Text = "";
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            Student student = (Student)dgStudent.SelectedItem;
            if (student != null)
            {
                SQLiteCommand cmd = new("DELETE FROM Student WHERE id=@student", DB.con);
                cmd.Parameters.AddWithValue("@student", student.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                    students.Remove(student);
                    dgStudent.ItemsSource = null;
                    dgStudent.ItemsSource = students;
                }
                catch (SQLiteException)
                {
                    MessageBox.Show("删除失败，请先移除对该学生的所有引用！");
                }
            }
        }
    }
}