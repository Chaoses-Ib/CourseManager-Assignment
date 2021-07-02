// SystemTeacher.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "SystemTeacher.h"
#include "afxdialogex.h"


// SystemTeacher dialog

IMPLEMENT_DYNAMIC(SystemTeacher, CDialogEx)

SystemTeacher::SystemTeacher(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_SYSTEM_TEACHER, pParent)
{
	m_bModified = false;
}

SystemTeacher::~SystemTeacher()
{
}

void SystemTeacher::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYSTEM_TEACHER, m_List);
}


BEGIN_MESSAGE_MAP(SystemTeacher, CDialogEx)
END_MESSAGE_MAP()


// SystemTeacher message handlers
