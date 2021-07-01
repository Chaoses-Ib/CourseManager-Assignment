
// CourseManager.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols
#include "DataFile.hpp"


// CCourseManagerApp:
// See CourseManager.cpp for the implementation of this class
//

class CCourseManagerApp : public CWinApp
{
public:
	CCourseManagerApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CCourseManagerApp theApp;

extern vector<Staff> g_staffs;
extern vector<Course> g_courses;
extern vector<Student> g_students;
extern vector<Score> g_scores;