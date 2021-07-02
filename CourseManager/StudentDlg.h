#pragma once


// StudentDlg dialog

class StudentDlg : public CDialogEx
{
	DECLARE_DYNAMIC(StudentDlg)

public:
	StudentDlg(CWnd* pParent = nullptr);   // standard constructor
	virtual ~StudentDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_STUDENT_DIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_List;
	virtual BOOL OnInitDialog();
	CEdit m_editSid;
	afx_msg void OnBnClickedButtonQuery();
	CStatic m_staticInfo;

public:
	double m_JoinedCredit;
	CStatic m_staticJoined;
	void RefreshJoinedCredit();

	int iItem;
	afx_msg void OnNMRClickList(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnStudentJoin();
	afx_msg void OnStudentExit();
	virtual BOOL PreTranslateMessage(MSG* pMsg);
};