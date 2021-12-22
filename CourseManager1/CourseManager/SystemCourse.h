#pragma once


// SystemCourse dialog

class SystemCourse : public CDialogEx
{
	DECLARE_DYNAMIC(SystemCourse)

public:
	SystemCourse(CWnd* pParent = nullptr);   // standard constructor
	virtual ~SystemCourse();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SYSTEM_COURSE };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	bool m_bModified;
	CListCtrl m_List;
	CEdit m_editCid;
	CEdit m_editName;
	CEdit m_editCredit;
	CEdit m_editHour;
	CEdit m_editType;
	afx_msg void OnBnClickedButtonSystemCourseAdd();
	virtual BOOL OnInitDialog();
	int iItem;
	void AddListItem(int i);
	afx_msg void OnNMRClickListSystemCourse(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnSystemDelete();
};
