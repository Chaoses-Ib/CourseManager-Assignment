#pragma once


// StudentEvaluationDlg dialog

class StudentEvaluationDlg : public CDialogEx
{
	DECLARE_DYNAMIC(StudentEvaluationDlg)

public:
	StudentEvaluationDlg(CWnd* pParent = nullptr);   // standard constructor
	virtual ~StudentEvaluationDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_STUDENT_EVALUATION };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_editScore;
	CEdit m_editComment;
	afx_msg void OnBnClickedOk();
	int m_iScore;
	CString m_sComment;
	virtual BOOL OnInitDialog();
	bool m_bEvaluated;
};
