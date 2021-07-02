// StudentDlg.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "StudentDlg.h"
#include "afxdialogex.h"


// StudentDlg dialog

IMPLEMENT_DYNAMIC(StudentDlg, CDialogEx)

StudentDlg::StudentDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_STUDENT_DIALOG, pParent)
{

}

StudentDlg::~StudentDlg()
{
}

void StudentDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST, m_List);
	DDX_Control(pDX, IDC_EDIT_SID, m_editSid);
	DDX_Control(pDX, IDC_STATIC_INFO, m_staticInfo);
}


BEGIN_MESSAGE_MAP(StudentDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_QUERY, &StudentDlg::OnBnClickedButtonQuery)
END_MESSAGE_MAP()


// StudentDlg message handlers


BOOL StudentDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	m_List.SetExtendedStyle(m_List.GetExtendedStyle() | LVS_EX_FULLROWSELECT);
	m_List.InsertColumn(0, L"课程编号", LVCFMT_LEFT, 90);
	m_List.InsertColumn(1, L"课程名称", LVCFMT_LEFT, 200);
	m_List.InsertColumn(2, L"学分", LVCFMT_LEFT, 60);
	m_List.InsertColumn(3, L"学时", LVCFMT_LEFT, 60);
	m_List.InsertColumn(4, L"课程类别", LVCFMT_LEFT, 60);
	m_List.InsertColumn(5, L"成绩", LVCFMT_LEFT, 90);

	for (int i = 0; i < g_courses.v.size(); i++) {
		Course& course = g_courses[i];
		m_List.InsertItem(i, course.cid);
		m_List.SetItemText(i, 1, course.name);
		CString buf;
		buf.Format(L"%.1f", course.credit);
		m_List.SetItemText(i, 2, buf);
		buf.Format(L"%d", course.hour);
		m_List.SetItemText(i, 3, buf);
		m_List.SetItemText(i, 4, course.type);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void StudentDlg::OnBnClickedButtonQuery()
{
	CString sid;
	m_editSid.GetWindowTextW(sid);

	Student* student = g_students.find_sid(sid);
	if(!student)
		MessageBoxW(L"该学号不存在", L"错误", MB_ICONERROR);
	CString buf;

	double total_credit = 0.;
	for (int i = 0; i < g_courses.v.size(); i++) {
		m_List.SetItemText(i, 5, L"");
	}
	for (Score& score : g_scores.v) {
		if (score.sid == sid) {
			int course = g_courses.index_name(score.course);
			if (course == -1)
				//#TODO
				continue;
			
			buf.Format(L"%d", score.score);
			m_List.SetItemText(course, 5, buf);

			total_credit += g_courses[course].credit;
		}
	}

	buf.Format(L"姓名：%s    班级：%s    专业：%s    已修总学分：%.1f", student->name, student->classid, student->major, total_credit);
	m_staticInfo.SetWindowTextW(buf);
}