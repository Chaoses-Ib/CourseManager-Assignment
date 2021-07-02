#pragma once


// CMyListCtrl

class CMyListCtrl : public CListCtrl
{
	DECLARE_DYNAMIC(CMyListCtrl)

public:
	CMyListCtrl();
	virtual ~CMyListCtrl();

protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnHdnItemclick(NMHDR* pNMHDR, LRESULT* pResult);
	int (__stdcall *CompareFunc)(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort);
	void RefreshSort();
	int m_sorted_column;
};