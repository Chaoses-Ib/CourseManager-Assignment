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

	// Return true if stopped, otherwise false.
	bool ForeachFilteredScore(std::function<void(size_t i, Score& score, bool& stop)> f);
	CListCtrl m_listStatistics;

public:
	CEdit m_editList;
	int m_iEditList;
	
	void ShowEditList(int item);
	afx_msg void OnNMDblclkList(NMHDR* pNMHDR, LRESULT* pResult);

	afx_msg void OnEnKillfocusEditList();
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	CEdit m_editScore;
	afx_msg void OnEnKillfocusEditScore();
};
