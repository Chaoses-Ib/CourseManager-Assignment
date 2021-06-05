#pragma once


// CTeacherDlg dialog

class CTeacherDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CTeacherDlg)

public:
	CTeacherDlg(CWnd* pParent = nullptr);   // standard constructor
	virtual ~CTeacherDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_TEACHER_DIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
//	afx_msg void OnLvnItemchangedList(NMHDR* pNMHDR, LRESULT* pResult);
private:
	CListCtrl m_List;
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedImport();
};
