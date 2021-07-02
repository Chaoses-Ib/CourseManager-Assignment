#pragma once
#include "pch.h"
#include <string>
#include <sstream>
#include <iostream>
#include <fstream>
#include <vector>

struct Staff {
	CString tid;
	CString name;
	vector<CString> courses;
};

struct Course {
	CString cid;
	CString name;
	double credit;
	uint32_t hour;
	CString type;
};

struct Student {
	CString sid;
	CString name;
	CString classid;
	CString major;
};

struct Score {
	CString sid;
	CString name;
	CString course;
	uint32_t score;
};


inline std::ostream& operator<<(std::ostream& o, CString s) {
	//ofstream will be converted to ostream by (<< '\t')
	o << CStringA(s).GetString();
	return o;
}

template<typename Data>
class DataFile {
public:
	vector<Data> OpenDlg(CWnd* parent) {
		CFileDialog dlg(TRUE, L"csv", NULL, OFN_FILEMUSTEXIST, L"Text files (*.txt)|*.txt|CSV Files (*.csv)|*.csv", parent);
		if (dlg.DoModal() == IDOK) {
			return OpenFile(dlg.GetPathName());
		}
		return {};
	}

	vector<Data> OpenFile(CString path) {
		std::ifstream fs(path);
		if (!fs.is_open()) {
			assert(false);  //#TODO
		}

		return Parse(fs);
	}

	vector<Data> Parse(std::ifstream& f) {
		using namespace std;
		string line;
		getline(f, line);  //ignore first line

		vector<Data> results;
		do {
			getline(f, line);  //#shit
			stringstream ss(line);

			string buf;
			ss >> buf;
			if (buf == "#END")
				break;

			results.push_back(ParseLine(stringstream(line)));
		} while (true);

		return results;
	}

	virtual Data ParseLine(std::stringstream ss) = 0;

	void SaveFile(CString path, vector<Data>& data) {
		std::ofstream fs(path);
		if (!fs.is_open()) {
			//#TODO
		}
		
		Emit(fs, data);
	}

	virtual void Emit(std::ofstream& f, vector<Data>& data) = 0;
};

class StaffDataFile : public DataFile<Staff> {
public:
	Staff ParseLine(std::stringstream ss) override {
		Staff staff;
		std::string buf;
		ss >> buf; staff.tid = buf.c_str();  //#shit
		ss >> buf; staff.name = buf.c_str();
		while (ss >> buf) {
			staff.courses.emplace_back(buf.c_str());
		}
		return staff;
	}

	void Emit(std::ofstream& f, vector<Staff>& data) override {
		f << "#工号（ID）\t姓名\t授课\n";
		for (Staff& i : data) {
			f << i.tid << '\t' << i.name;
			for (CString& course : i.courses) {
				f << '\t' << course;
			}
			f << std::endl;
		}
		f << "#END";
	}
};

class CourseDataFile : public DataFile<Course> {
public:
	Course ParseLine(std::stringstream ss) override {
		Course course;
		std::string buf;
		ss >> buf; course.cid = buf.c_str();  //#shit
		ss >> buf; course.name = buf.c_str();
		ss >> course.credit;
		ss >> course.hour;
		ss >> buf; course.type = buf.c_str();
		return course;
	}

	void Emit(std::ofstream& f, vector<Course>& data) override {
		f << "#课程编号\t课程名称\t学分\t学时\t课程类别\n";
		for (Course& i : data) {
			f << i.cid << '\t' << i.name << '\t' << i.credit << '\t' << i.hour << '\t' << i.type;
			f << std::endl;
		}
		f << "#END";
	}
};

class StudentDataFile : public DataFile<Student> {
public:
	Student ParseLine(std::stringstream ss) override {
		Student student;
		std::string buf;
		ss >> buf; student.sid = buf.c_str();  //#shit
		ss >> buf; student.name = buf.c_str();
		ss >> buf; student.classid = buf.c_str();
		ss >> buf; student.major = buf.c_str();
		return student;
	}

	void Emit(std::ofstream& f, vector<Student>& data) override {
		f << "#学号（ID）\t姓名\t班级\t专业\n";
		for (Student& i : data) {
			f << i.sid << '\t' << i.name << '\t' << i.classid << '\t' << i.major;
			f << std::endl;
		}
		f << "#END";
	}
};

class ScoreDataFile : public DataFile<Score> {
public:
	Score ParseLine(std::stringstream ss) override {
		Score score;
		std::string buf;
		ss >> buf; score.sid = buf.c_str();  //#shit
		ss >> buf; score.name = buf.c_str();
		ss >> buf; score.course = buf.c_str();
		ss >> score.score;
		return score;
	}

	void Emit(std::ofstream& f, vector<Score>& data) override {
		f << "#学号\t姓名\t课程名称\t成绩\n";
		for (Score& i : data) {
			f << i.sid << '\t' << i.name << '\t' << i.course << '\t' << i.score;
			f << std::endl;
		}
		f << "#END";
	}
};

template<typename Data>
struct DataUtil {
	vector<Data> v;

	Data& operator[](size_t i) {
		return v[i];
	}
	const Data& operator[](size_t i) const {
		return v[i];
	}
};

extern struct StaffUtil : DataUtil<Staff> {
	StaffDataFile file;

	Staff* find_tid(CString tid) {
		for (Staff& staff : v) {
			if (staff.tid == tid)
				return &staff;
		}
		return nullptr;
	}
} g_staffs;

extern struct CourseUtil : DataUtil<Course> {
	CourseDataFile file;

	Course* find_cid(CString cid) {
		for (Course& course : v) {
			if (course.cid == cid)
				return &course;
		}
		return nullptr;
	}
} g_courses;

extern struct StudentUtil : DataUtil<Student> {
	StudentDataFile file;

	Student* find_sid(CString sid) {
		for (Student& student : v) {
			if (student.sid == sid)
				return &student;
		}
		return nullptr;
	}
} g_students;

extern struct ScoreUtil : DataUtil<Score> {
	ScoreDataFile file;
} g_scores;