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
			//#TODO
		}

		return Parse(fs);
	}

	vector<Data> Parse(std::ifstream& f) {
		using namespace std;
		string line;
		getline(f, line);  //ignore first line

		vector<Score> results;
		do {
			getline(f, line);  //#shit
			stringstream ss(line);

			Score sscore;
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
		f << "#���ţ�ID��\t����\t�ڿ�\n";
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
		f << "#�γ̱��\t�γ�����\tѧ��\tѧʱ\t�γ����\n";
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
		f << "#ѧ�ţ�ID��\t����\t�༶\tרҵ\n";
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
		f << "#ѧ��\t����\t�γ�����\t�ɼ�\n";
		for (Score& i : data) {
			f << i.sid << '\t' << i.name << '\t' << i.course << '\t' << i.score;
			f << std::endl;
		}
		f << "#END";
	}
};