// SystemCourse.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "SystemCourse.h"
#include "afxdialogex.h"


// SystemCourse dialog

IMPLEMENT_DYNAMIC(SystemCourse, CDialogEx)

SystemCourse::SystemCourse(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_SYSTEM_COURSE, pParent)
{
	m_bModified = false;
}

SystemCourse::~SystemCourse()
{
}

void SystemCourse::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYSTEM_COURSE, m_List);
}


BEGIN_MESSAGE_MAP(SystemCourse, CDialogEx)
END_MESSAGE_MAP()


// SystemCourse message handlers
