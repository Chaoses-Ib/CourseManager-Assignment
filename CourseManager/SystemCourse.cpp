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
	iItem = -1;
}

SystemCourse::~SystemCourse()
{
}

void SystemCourse::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYSTEM_COURSE, m_List);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_COURSE_CID, m_editCid);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_COURSE_NAME, m_editName);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_COURSE_CREDIT, m_editCredit);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_COURSE_HOUR, m_editHour);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_COURSE_TYPE, m_editType);
}


BEGIN_MESSAGE_MAP(SystemCourse, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_SYSTEM_COURSE_ADD, &SystemCourse::OnBnClickedButtonSystemCourseAdd)
	ON_NOTIFY(NM_RCLICK, IDC_LIST_SYSTEM_COURSE, &SystemCourse::OnNMRClickListSystemCourse)
	ON_COMMAND(ID_SYSTEM_DELETE, &SystemCourse::OnSystemDelete)
END_MESSAGE_MAP()


// SystemCourse message handlers


BOOL SystemCourse::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	m_List.SetExtendedStyle(m_List.GetExtendedStyle() | LVS_EX_FULLROWSELECT);
	m_List.InsertColumn(0, L"课程编号", LVCFMT_LEFT, 90);
	m_List.InsertColumn(1, L"课程名称", LVCFMT_LEFT, 200);
	m_List.InsertColumn(2, L"学分", LVCFMT_LEFT, 60);
	m_List.InsertColumn(3, L"学时", LVCFMT_LEFT, 60);
	m_List.InsertColumn(4, L"课程类别", LVCFMT_LEFT, 60);

	for (int i = 0; i < g_courses.v.size(); i++) {
		AddListItem(i);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void SystemCourse::AddListItem(int i)
{
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

void SystemCourse::OnBnClickedButtonSystemCourseAdd()
{
	Course s;
	m_editCid.GetWindowTextW(s.cid);
	m_editName.GetWindowTextW(s.name);

	CString buf;
	m_editCredit.GetWindowTextW(buf);
	std::wstringstream ss1(buf.GetString());
	ss1 >> s.credit;

	m_editHour.GetWindowTextW(buf);
	std::wstringstream ss2(buf.GetString());
	ss2 >> s.hour;

	m_editType.GetWindowTextW(s.type);

	g_courses.v.push_back(std::move(s));
	m_bModified = true;
	AddListItem(g_courses.v.size() - 1);
}

void SystemCourse::OnNMRClickListSystemCourse(NMHDR* pNMHDR, LRESULT* pResult)
{
	LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<LPNMITEMACTIVATE>(pNMHDR);
	NMLISTVIEW* list = (NMLISTVIEW*)pNMHDR;

	iItem = list->iItem;
	CMenu menu;
	menu.LoadMenuW(IDR_MENU_SYSTEM);
	CMenu* submenu = menu.GetSubMenu(0);

	POINT point;
	GetCursorPos(&point);
	submenu->TrackPopupMenu(TPM_RIGHTBUTTON, point.x, point.y, this);

	*pResult = 0;
}

void SystemCourse::OnSystemDelete()
{
	m_List.DeleteItem(iItem);
	g_courses.v.erase(std::next(g_courses.v.begin(), iItem));
	m_bModified = true;
}