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
	iItem = -1;
	m_JoinedCredit = 0.0;
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
	DDX_Control(pDX, IDC_STATIC_JOINED, m_staticJoined);
}


BEGIN_MESSAGE_MAP(StudentDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_QUERY, &StudentDlg::OnBnClickedButtonQuery)
	ON_NOTIFY(NM_RCLICK, IDC_LIST, &StudentDlg::OnNMRClickList)
	ON_COMMAND(ID_STUDENT_JOIN, &StudentDlg::OnStudentJoin)
	ON_COMMAND(ID_STUDENT_EXIT, &StudentDlg::OnStudentExit)
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
	m_List.InsertColumn(6, L"选修", LVCFMT_LEFT, 60);

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
	if (!student) {
		MessageBoxW(L"该学号不存在", L"错误", MB_ICONERROR);
		return;
	}
	CString buf;

	double total_credit = 0.;
	for (int i = 0; i < g_courses.v.size(); i++) {
		m_List.SetItemText(i, 5, L"");
		m_List.SetItemText(i, 6, L"");
	}
	for (Score& score : g_scores.v) {
		if (score.sid == sid) {
			int course = g_courses.index_name(score.course);
			if (course == -1)
				//#TODO
				continue;
			
			buf.Format(L"%d", score.score);
			m_List.SetItemText(course, 5, buf);
			m_List.SetItemText(course, 6, L"已修");

			total_credit += g_courses[course].credit;
		}
	}

	buf.Format(L"姓名：%s    班级：%s    专业：%s    已修总学分：%.1f", student->name, student->classid, student->major, total_credit);
	m_staticInfo.SetWindowTextW(buf);

	m_JoinedCredit = total_credit;
	RefreshJoinedCredit();
}

void StudentDlg::RefreshJoinedCredit()
{
	CString buf;
	buf.Format(L"选修总学分：%.1f", m_JoinedCredit);
	m_staticJoined.SetWindowTextW(buf);
}

void StudentDlg::OnNMRClickList(NMHDR* pNMHDR, LRESULT* pResult)
{
	LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<LPNMITEMACTIVATE>(pNMHDR);
	NMLISTVIEW* list = (NMLISTVIEW*)pNMHDR;

	iItem = list->iItem;
	CMenu menu;
	menu.LoadMenuW(IDR_MENU_STUDENT);
	CMenu* submenu = menu.GetSubMenu(0);

	CString join = m_List.GetItemText(iItem, 6);
	if (join == L"")
		submenu->EnableMenuItem(ID_STUDENT_EXIT, MF_GRAYED);
	else if (join == L"选修")
		submenu->EnableMenuItem(ID_STUDENT_JOIN, MF_GRAYED);
	else if (join == L"已修") {
		submenu->EnableMenuItem(ID_STUDENT_JOIN, MF_GRAYED);
		submenu->EnableMenuItem(ID_STUDENT_EXIT, MF_GRAYED);
	}

	POINT point;
	GetCursorPos(&point);
	submenu->TrackPopupMenu(TPM_RIGHTBUTTON, point.x, point.y, this);

	*pResult = 0;
}

void StudentDlg::OnStudentJoin()
{
	m_List.SetItemText(iItem, 6, L"选修");
	m_JoinedCredit += g_courses[iItem].credit;
	RefreshJoinedCredit();
}

void StudentDlg::OnStudentExit()
{
	m_List.SetItemText(iItem, 6, L"");
	m_JoinedCredit -= g_courses[iItem].credit;
	RefreshJoinedCredit();
}

BOOL StudentDlg::PreTranslateMessage(MSG* pMsg)
{
	if (pMsg->message == WM_KEYDOWN && pMsg->wParam == VK_RETURN) {
		if (GetFocus() == &m_editSid)
			OnBnClickedButtonQuery();
		return true;
	}

	return CDialogEx::PreTranslateMessage(pMsg);
}