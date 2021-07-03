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
	iItem = -1;
}

SystemTeacher::~SystemTeacher()
{
}

void SystemTeacher::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYSTEM_TEACHER, m_List);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_TEACHER_TID, m_editTid);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_TEACHER_NAME, m_editName);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_TEACHER_COURSES, m_editCourses);
}


BEGIN_MESSAGE_MAP(SystemTeacher, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_SYSTEM_TEACHER_ADD, &SystemTeacher::OnBnClickedButtonSystemTeacherAdd)
	ON_NOTIFY(NM_RCLICK, IDC_LIST_SYSTEM_TEACHER, &SystemTeacher::OnNMRClickListSystemTeacher)
	ON_COMMAND(ID_SYSTEM_DELETE, &SystemTeacher::OnSystemDelete)
END_MESSAGE_MAP()


// SystemTeacher message handlers

static int __stdcall CompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM sort) {
	int result;
	switch (sort & ~0x80000000) {
	case 0: result = g_staffs[lParam1].tid.Compare(g_staffs[lParam2].tid); break;
	case 1: result = g_staffs[lParam1].name.Compare(g_staffs[lParam2].name); break;
	case 2: result = g_staffs[lParam1].courses.size() - g_staffs[lParam2].courses.size(); break;
	case 3: result = g_staffs[lParam1].evaluation.avg() - g_staffs[lParam2].evaluation.avg(); break;
	default:
		assert(false);
	}
	return sort & 0x80000000 ? -result : result;  //down : up
}

BOOL SystemTeacher::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	m_List.SetExtendedStyle(m_List.GetExtendedStyle() | LVS_EX_FULLROWSELECT);
	m_List.CompareFunc = CompareFunc;
	m_List.InsertColumn(0, L"工号", LVCFMT_LEFT, 90);
	m_List.InsertColumn(1, L"姓名", LVCFMT_LEFT, 90);
	m_List.InsertColumn(2, L"授课", LVCFMT_LEFT, 400);
	m_List.InsertColumn(3, L"平均评分", LVCFMT_LEFT, 60);

	for (int i = 0; i < g_staffs.v.size(); i++) {
		AddListItem(i);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void SystemTeacher::AddListItem(int i)
{
	Staff& staff = g_staffs[i];
	m_List.InsertItem(LVIF_TEXT | LVIF_PARAM, i, staff.tid, 0, 0, 0, i);
	m_List.SetItemText(i, 1, staff.name);
	CString buf;
	for (CString& s : staff.courses) {
		buf.Append(s);
		buf.Append(L" ");
	}
	m_List.SetItemText(i, 2, buf);

	if (staff.evaluation.n) {
		buf.Format(L"%.1f", staff.evaluation.avg());
		m_List.SetItemText(i, 3, buf);
	}
}

void SystemTeacher::OnBnClickedButtonSystemTeacherAdd()
{
	Staff s;
	m_editTid.GetWindowTextW(s.tid);
	m_editName.GetWindowTextW(s.name);

	CString buf;
	m_editCourses.GetWindowTextW(buf);
	std::wstringstream ss(buf.GetString());
	std::wstring wstr;
	while (ss >> wstr)
		s.courses.emplace_back(wstr.c_str());

	g_staffs.v.push_back(std::move(s));
	m_bModified = true;
	
	int i = g_staffs.v.size() - 1;
	AddListItem(i);
	m_List.EnsureVisible(i, false);
}

void SystemTeacher::OnNMRClickListSystemTeacher(NMHDR* pNMHDR, LRESULT* pResult)
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

void SystemTeacher::OnSystemDelete()
{
	int i = m_List.GetItemData(iItem);
	m_List.DeleteItem(iItem);
	g_staffs.v.erase(std::next(g_staffs.v.begin(), i));
	m_bModified = true;
}