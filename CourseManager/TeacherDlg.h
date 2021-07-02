#pragma once
#include "CMyListCtrl.h"
#include <functional>

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
	CMyListCtrl m_List;
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedImport();
protected:
	HICON m_hIcon;
public:
	afx_msg void OnCbnSelchangeComboClass();
	CComboBox m_comboClass;
protected:
	void RefreshScores(bool refresh_classes);
public:
	afx_msg void OnBnClickedCancel();
protected:
	bool m_modified = false;
public:
	afx_msg void OnBnClickedButtonFind();
	CEdit m_editFind;
	void RefreshStatistics();
	void ForeachFilteredScore(std::function<void(size_t, Score&)>);
	CListCtrl m_listStatistics;
};
