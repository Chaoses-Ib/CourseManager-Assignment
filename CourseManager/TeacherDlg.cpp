// TeacherDlg.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "TeacherDlg.h"
#include "afxdialogex.h"

#include "Data.hpp"
#include <set>


// CTeacherDlg dialog

IMPLEMENT_DYNAMIC(CTeacherDlg, CDialogEx)

CTeacherDlg::CTeacherDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_TEACHER_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

CTeacherDlg::~CTeacherDlg()
{
}

void CTeacherDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST, m_List);
	DDX_Control(pDX, IDC_COMBO_CLASS, m_comboClass);
}


BEGIN_MESSAGE_MAP(CTeacherDlg, CDialogEx)
ON_BN_CLICKED(IDC_IMPORT, &CTeacherDlg::OnBnClickedImport)
ON_CBN_SELCHANGE(IDC_COMBO_CLASS, &CTeacherDlg::OnCbnSelchangeComboClass)
ON_BN_CLICKED(IDCANCEL, &CTeacherDlg::OnBnClickedCancel)
END_MESSAGE_MAP()

// CTeacherDlg message handlers

int __stdcall CompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM sort) {
	int result;
	switch (sort & ~0x80000000) {
	case 0: result = g_scores[lParam1].sid.Compare(g_scores[lParam2].sid); break;
	case 1: result = g_scores[lParam1].name.Compare(g_scores[lParam2].name); break;
	case 2: result = g_scores[lParam1].course.Compare(g_scores[lParam2].course); break;
	case 3: result = g_scores[lParam1].score - g_scores[lParam2].score; break;
	default:
		assert(false);
	}
	return sort & 0x80000000 ? -result : result;  //down : up
}

BOOL CTeacherDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	m_List.CompareFunc = CompareFunc;
	m_List.InsertColumn(0, L"学号", LVCFMT_LEFT, 90);
	m_List.InsertColumn(1, L"姓名", LVCFMT_LEFT, 90);
	m_List.InsertColumn(2, L"课程名称", LVCFMT_LEFT, 200);
	m_List.InsertColumn(3, L"成绩", LVCFMT_LEFT, 90);
	RefreshScores();

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void CTeacherDlg::OnBnClickedImport()
{
	ScoreDataFile file;
	vector<Score> import_scores = file.OpenDlg(this);
	for (Score& score : import_scores)
		g_scores.v.push_back(std::move(score));

	RefreshScores();
}

void CTeacherDlg::OnCbnSelchangeComboClass()
{
	m_List.DeleteAllItems();

	int sel = m_comboClass.GetCurSel();
	if (sel == 0) {
		RefreshScores();
		return;
	}

	CString classid;
	m_comboClass.GetLBText(sel, classid);
	int item = 0;
	for (uint32_t i = 0; i < g_scores.v.size(); i++) {
		auto& score = g_scores[i];
		if (g_students.find_sid(score.sid)->classid == classid) {  //#todo
			m_List.InsertItem(LVIF_TEXT | LVIF_PARAM, item, score.sid, 0, 0, 0, i);  //nItem can't be -1
			m_List.SetItem(item, 1, LVIF_TEXT, score.name, 0, 0, 0, 0);
			m_List.SetItem(item, 2, LVIF_TEXT, score.course, 0, 0, 0, 0);
			CString text;
			text.Format(L"%d", score.score);  //#shit
			m_List.SetItem(item, 3, LVIF_TEXT, text, 0, 0, 0, 0);

			item++;
		}
	}
	m_List.RefreshSort();
}

void CTeacherDlg::RefreshScores()
{
	m_List.DeleteAllItems();

	static std::set<std::wstring> classes;
	for (uint32_t i = 0; i < g_scores.v.size(); i++) {
		auto& score = g_scores[i];

		m_List.InsertItem(LVIF_TEXT | LVIF_PARAM, i, score.sid, 0, 0, 0, i);  //nItem can't be -1
		m_List.SetItem(i, 1, LVIF_TEXT, score.name, 0, 0, 0, 0);
		m_List.SetItem(i, 2, LVIF_TEXT, score.course, 0, 0, 0, 0);
		CString text;
		text.Format(L"%d", score.score);  //#shit
		m_List.SetItem(i, 3, LVIF_TEXT, text, 0, 0, 0, 0);

		classes.emplace(g_students.find_sid(score.sid)->classid);  //#todo
	}
	m_List.RefreshSort();

	m_comboClass.ResetContent();  //#shit name
	m_comboClass.AddString(L"全部");
	for (std::wstring classid : classes) {
		m_comboClass.AddString(classid.c_str());
	}
	m_comboClass.SetCurSel(0);
}


void CTeacherDlg::OnBnClickedCancel()
{
	//#TODO
	CDialogEx::OnCancel();
}