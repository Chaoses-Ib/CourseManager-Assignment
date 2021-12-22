#pragma once

#include "SystemTeacher.h"
#include "SystemCourse.h"
#include "SystemStudent.h"

// SystemDlg dialog

class SystemDlg : public CDialogEx
{
	DECLARE_DYNAMIC(SystemDlg)

public:
	SystemDlg(CWnd* pParent = nullptr);   // standard constructor
	virtual ~SystemDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SYSTEM_DIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CTabCtrl m_Tab;
	CDialogEx* m_Pages[3];
	SystemTeacher m_Teacher;
	SystemStudent m_Student;
	SystemCourse m_Course;
	afx_msg void OnTcnSelchangeTab1(NMHDR* pNMHDR, LRESULT* pResult);
	int m_iCurSel;
	afx_msg void OnBnClickedCancel();
//	bool m_bModified;
};
