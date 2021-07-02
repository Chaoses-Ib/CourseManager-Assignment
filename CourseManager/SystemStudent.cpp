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
	iItem = -1;
}

SystemStudent::~SystemStudent()
{
}

void SystemStudent::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYSTEM_STUDENT, m_List);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_STUDENT_SID, m_editSid);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_STUDENT_NAME, m_editName);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_STUDENT_CLASS, m_editClass);
	DDX_Control(pDX, IDC_EDIT_SYSTEM_STUDENT_MAJOR, m_editMajor);
}


BEGIN_MESSAGE_MAP(SystemStudent, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_SYSTEM_STUDENT_ADD, &SystemStudent::OnBnClickedButtonSystemStudentAdd)
	ON_NOTIFY(NM_RCLICK, IDC_LIST_SYSTEM_STUDENT, &SystemStudent::OnNMRClickListSystemStudent)
	ON_COMMAND(ID_SYSTEM_DELETE, &SystemStudent::OnSystemDelete)
END_MESSAGE_MAP()


// SystemStudent message handlers


BOOL SystemStudent::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	m_List.SetExtendedStyle(m_List.GetExtendedStyle() | LVS_EX_FULLROWSELECT);
	m_List.InsertColumn(0, L"学号", LVCFMT_LEFT, 90);
	m_List.InsertColumn(1, L"姓名", LVCFMT_LEFT, 90);
	m_List.InsertColumn(2, L"班级", LVCFMT_LEFT, 90);
	m_List.InsertColumn(3, L"专业", LVCFMT_LEFT, 200);

	for (int i = 0; i < g_students.v.size(); i++) {
		AddListItem(i);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void SystemStudent::AddListItem(int i)
{
	Student& student = g_students[i];
	m_List.InsertItem(i, student.sid);
	m_List.SetItemText(i, 1, student.name);
	m_List.SetItemText(i, 2, student.classid);
	m_List.SetItemText(i, 3, student.major);
}

void SystemStudent::OnBnClickedButtonSystemStudentAdd()
{
	Student s;
	m_editSid.GetWindowTextW(s.sid);
	m_editName.GetWindowTextW(s.name);
	m_editClass.GetWindowTextW(s.classid);
	m_editMajor.GetWindowTextW(s.major);

	g_students.v.push_back(std::move(s));
	m_bModified = true;
	AddListItem(g_students.v.size() - 1);
}

void SystemStudent::OnNMRClickListSystemStudent(NMHDR* pNMHDR, LRESULT* pResult)
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

void SystemStudent::OnSystemDelete()
{
	m_List.DeleteItem(iItem);
	g_students.v.erase(std::next(g_students.v.begin(), iItem));
	m_bModified = true;
}