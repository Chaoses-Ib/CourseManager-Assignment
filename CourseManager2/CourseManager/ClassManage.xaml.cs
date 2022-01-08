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
    public class Class : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Major { get; set; }
        public string Department { get; set; }
        public int StudentNum { get; set; }

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
    /// Interaction logic for ClassManage.xaml
    /// </summary>
    public partial class ClassManage : Page
    {
        List<Class> classes;

        void SqlToDataGrid(string command)
        {
            SQLiteCommand cmd = new(command, DB.con);
            SqlToDataGrid(cmd);
        }

        void SqlToDataGrid(SQLiteCommand cmd)
        {
            SQLiteDataReader reader = cmd.ExecuteReader();

            classes = new();
            while (reader.Read())
            {
                classes.Add(new Class
                {
                    Id = reader.GetString(0),
                    Major = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Department = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    StudentNum = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                });
            }
            dgClass.ItemsSource = classes;
        }

        private void cmdRefresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SqlToDataGrid(
@"SELECT id, major, department, A.student_num FROM Class
	LEFT JOIN (SELECT class, COUNT(*) student_num FROM Student GROUP BY class) A ON A.class=id;");
        }

        public ClassManage()
        {
            InitializeComponent();
            cmdRefresh_Executed(null, null);
        }

        private void dgClass_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Class class_ = (Class)e.Row.Item;
            SQLiteCommand cmd = new("UPDATE Class SET major=@major, department=@department WHERE id=@class", DB.con);
            cmd.Parameters.AddWithValue("@class", class_.Id);
            cmd.Parameters.AddWithValue("@major", class_.Major);
            cmd.Parameters.AddWithValue("@department", class_.Department);
            cmd.ExecuteNonQuery();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SQLiteCommand cmd = new("SELECT COUNT(*) FROM Student WHERE id=@id", DB.con);
            cmd.Parameters.AddWithValue("@id", editId.Text);
            if ((long)cmd.ExecuteScalar() != 0)
            {
                MessageBox.Show("该班号已存在！");
                return;
            }

            cmd.CommandText = "INSERT INTO Class VALUES (@id,NULL,NULL)";
            cmd.ExecuteNonQuery();

            classes.Add(new Class { Id = editId.Text });
            dgClass.ItemsSource = null;
            dgClass.ItemsSource = classes;

            editId.Text = "";
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            Class class_ = (Class)dgClass.SelectedItem;
            if (class_ != null)
            {
                SQLiteCommand cmd = new("DELETE FROM Class WHERE id=@class", DB.con);
                cmd.Parameters.AddWithValue("@class", class_.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                    classes.Remove(class_);
                    dgClass.ItemsSource = null;
                    dgClass.ItemsSource = classes;
                }
                catch (SQLiteException)
                {
                    MessageBox.Show("删除失败，请先移除对该班级的所有引用！");
                }
            }
        }
    }
}