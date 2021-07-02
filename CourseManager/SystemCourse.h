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
};
