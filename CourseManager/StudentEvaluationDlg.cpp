// StudentEvaluationDlg.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "StudentEvaluationDlg.h"
#include "afxdialogex.h"


// StudentEvaluationDlg dialog

IMPLEMENT_DYNAMIC(StudentEvaluationDlg, CDialogEx)

StudentEvaluationDlg::StudentEvaluationDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_STUDENT_EVALUATION, pParent)
{
}

StudentEvaluationDlg::~StudentEvaluationDlg()
{
}

void StudentEvaluationDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_STUDENT_EVALUATION_SCORE, m_editScore);
	DDX_Control(pDX, IDC_EDIT_STUDENT_EVALUATION_COMMENT, m_editComment);
}


BEGIN_MESSAGE_MAP(StudentEvaluationDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &StudentEvaluationDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// StudentEvaluationDlg message handlers

BOOL StudentEvaluationDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	if (m_bEvaluated) {
		CString buf;
		buf.Format(L"%d", m_iScore);
		m_editScore.SetWindowTextW(buf);
		m_editComment.SetWindowTextW(m_sComment);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}

void StudentEvaluationDlg::OnBnClickedOk()
{
	CString buf;
	m_editScore.GetWindowTextW(buf);
	std::wstringstream ss(buf.GetString());
	ss >> m_iScore;
	if (m_iScore < 0) m_iScore = 0;
	if (m_iScore > 10) m_iScore = 10;

	m_editComment.GetWindowTextW(m_sComment);

	CDialogEx::OnOK();
}