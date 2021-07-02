#pragma once


// SystemTeacher dialog

class SystemTeacher : public CDialogEx
{
	DECLARE_DYNAMIC(SystemTeacher)

public:
	SystemTeacher(CWnd* pParent = nullptr);   // standard constructor
	virtual ~SystemTeacher();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SYSTEM_TEACHER };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	bool m_bModified;
	CListCtrl m_List;
	virtual BOOL OnInitDialog();
	CEdit m_editTid;
	CEdit m_editName;
	CEdit m_editCourses;
	afx_msg void OnBnClickedButtonSystemTeacherAdd();
	void AddListItem(int i);
	afx_msg void OnNMRClickListSystemTeacher(NMHDR* pNMHDR, LRESULT* pResult);
	int iItem;
	afx_msg void OnSystemDelete();
};
