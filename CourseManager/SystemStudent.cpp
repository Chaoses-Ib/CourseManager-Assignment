// SystemStudent.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "SystemStudent.h"
#include "afxdialogex.h"


// SystemStudent dialog

IMPLEMENT_DYNAMIC(SystemStudent, CDialogEx)

SystemStudent::SystemStudent(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_SYSTEM_STUDENT, pParent)
{
	m_bModified = false;
}

SystemStudent::~SystemStudent()
{
}

void SystemStudent::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYSTEM_STUDENT, m_List);
}


BEGIN_MESSAGE_MAP(SystemStudent, CDialogEx)
END_MESSAGE_MAP()


// SystemStudent message handlers
