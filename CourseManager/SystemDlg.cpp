// SystemDialog.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "SystemDlg.h"
#include "afxdialogex.h"

// SystemDlg dialog

IMPLEMENT_DYNAMIC(SystemDlg, CDialogEx)

SystemDlg::SystemDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_SYSTEM_DIALOG, pParent)
{
}

SystemDlg::~SystemDlg()
{
}

void SystemDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB1, m_Tab);
}


BEGIN_MESSAGE_MAP(SystemDlg, CDialogEx)
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB1, &SystemDlg::OnTcnSelchangeTab1)
	ON_BN_CLICKED(IDCANCEL, &SystemDlg::OnBnClickedCancel)
END_MESSAGE_MAP()


// SystemDlg message handlers


BOOL SystemDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	m_Tab.InsertItem(0, L"教师");
	m_Tab.InsertItem(1, L"学生");
	m_Tab.InsertItem(2, L"课程");

	m_Teacher.Create(IDD_SYSTEM_TEACHER, &m_Tab);
	m_Student.Create(IDD_SYSTEM_STUDENT, &m_Tab);
	m_Course.Create(IDD_SYSTEM_COURSE, &m_Tab);

	m_Pages[0] = &m_Teacher;
	m_Pages[1] = &m_Student;
	m_Pages[2] = &m_Course;

	CRect rect;
	m_Tab.GetClientRect(rect);
	rect.top += 22;
	rect.left += 1;
	rect.right -= 3;
	rect.bottom -= 2;
	for (CDialogEx* page : m_Pages) {
		page->MoveWindow(&rect);
		page->ShowWindow(SW_HIDE);
	}
	m_iCurSel = 0;
	m_Pages[0]->ShowWindow(SW_SHOW);

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void SystemDlg::OnTcnSelchangeTab1(NMHDR* pNMHDR, LRESULT* pResult)
{
	m_Pages[m_iCurSel]->ShowWindow(SW_HIDE);
	m_iCurSel = m_Tab.GetCurSel();
	m_Pages[m_iCurSel]->ShowWindow(SW_SHOW);

	*pResult = 0;
}

void SystemDlg::OnBnClickedCancel()
{
	if (m_Teacher.m_bModified || m_Student.m_bModified || m_Course.m_bModified) {
		int button = MessageBoxW(L"是否保存更改？", L"询问", MB_YESNOCANCEL | MB_ICONQUESTION);
		switch (button) {
		default: assert(false);
		case IDCANCEL: return;
		case IDYES:
			if (m_Teacher.m_bModified)
				g_scores.file.SaveFile(LR"(.\data\staff.txt)", g_scores.v);
			if (m_Student.m_bModified)
				g_students.file.SaveFile(LR"(.\data\student.txt)", g_students.v);
			if (m_Course.m_bModified)
				g_courses.file.SaveFile(LR"(.\data\module.txt)", g_courses.v);
		case IDNO: CDialogEx::OnCancel();
		}
	}
	else {
		CDialogEx::OnCancel();
	}
}