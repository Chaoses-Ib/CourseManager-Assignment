using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CourseManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

	}

	public static class DB
    {
		public static SQLiteConnection con = new(@"URI=file:data.db");

        static DB()
        {
			bool not_inited = !System.IO.File.Exists(@"data.db");
			con.Open();
			SQLiteCommand cmd = new(con);

			// 启用外键约束
			cmd.CommandText = "PRAGMA foreign_keys = 1";
			cmd.ExecuteNonQuery();

			if (not_inited)
			{
				cmd.CommandText =
@"CREATE TABLE Student(
	id INTEGER PRIMARY KEY,
	name TEXT,
	gender INTEGER,
	class TEXT,
	FOREIGN KEY (class) REFERENCES Class(id)
);
CREATE TABLE Class(
	id TEXT PRIMARY KEY,
	major TEXT,
	department TEXT
);
CREATE TABLE Course(
	id INTEGER PRIMARY KEY,
	name TEXT,
	teacher INTEGER,
	credits REAL,
	type INTEGER,
	FOREIGN KEY (teacher) REFERENCES Teacher(id)
);
CREATE TABLE Teacher(
	id INTEGER PRIMARY KEY,
	name TEXT,
	gender INTEGER,
	department TEXT
);
CREATE TABLE CourseTaking(
	student INTEGER,
	course INTEGER,
	score REAL,
	teacher_score REAL,
	teacher_remark TEXT,
	PRIMARY KEY (student, course),
	FOREIGN KEY (student) REFERENCES Student(id),
	FOREIGN KEY (course) REFERENCES Course(id)
);";
				cmd.ExecuteNonQuery();
			}
		}
	}
}