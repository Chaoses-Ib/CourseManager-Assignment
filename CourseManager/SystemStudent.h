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
};
