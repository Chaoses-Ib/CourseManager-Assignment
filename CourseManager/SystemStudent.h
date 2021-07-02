#pragma once


// SystemStudent dialog

class SystemStudent : public CDialogEx
{
	DECLARE_DYNAMIC(SystemStudent)

public:
	SystemStudent(CWnd* pParent = nullptr);   // standard constructor
	virtual ~SystemStudent();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SYSTEM_STUDENT };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	bool m_bModified;
	CListCtrl m_List;
	CEdit m_editSid;
	CEdit m_editName;
	CEdit m_editClass;
	CEdit m_editMajor;
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedButtonSystemStudentAdd();
	int iItem;
	afx_msg void OnNMRClickListSystemStudent(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnSystemDelete();
	void AddListItem(int i);
};
