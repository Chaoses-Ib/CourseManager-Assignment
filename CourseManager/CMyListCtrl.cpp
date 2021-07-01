// CMyListCtrl.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "CMyListCtrl.h"


// CMyListCtrl

IMPLEMENT_DYNAMIC(CMyListCtrl, CListCtrl)

CMyListCtrl::CMyListCtrl()
{

	m_sorted_column = 0;
}

CMyListCtrl::~CMyListCtrl()
{
}


BEGIN_MESSAGE_MAP(CMyListCtrl, CListCtrl)
	ON_NOTIFY(HDN_ITEMCLICKA, 0, &CMyListCtrl::OnHdnItemclick)
	ON_NOTIFY(HDN_ITEMCLICKW, 0, &CMyListCtrl::OnHdnItemclick)
END_MESSAGE_MAP()



// CMyListCtrl message handlers

void CMyListCtrl::OnHdnItemclick(NMHDR* pNMHDR, LRESULT* pResult)
{
	if (!CompareFunc) {
		*pResult = 0;
		return;
	}
	LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
	
	HDITEM header;
	header.mask = HDI_FORMAT;

	if (phdr->iItem != m_sorted_column) {
		GetHeaderCtrl()->GetItem(m_sorted_column, &header);
		header.fmt = header.fmt & ~HDF_SORTDOWN & ~HDF_SORTUP;
		GetHeaderCtrl()->SetItem(m_sorted_column, &header);
		m_sorted_column = phdr->iItem;
	}

	GetHeaderCtrl()->GetItem(phdr->iItem, &header);
	if (header.fmt & HDF_SORTDOWN) {
		header.fmt = header.fmt & ~HDF_SORTDOWN | HDF_SORTUP;
	}
	else if (header.fmt & HDF_SORTUP) {
		header.fmt = header.fmt & ~HDF_SORTUP | HDF_SORTDOWN;
	}
	else {
		header.fmt |= HDF_SORTDOWN;
	}
	GetHeaderCtrl()->SetItem(phdr->iItem, &header);

	RefreshSort();

	*pResult = 0;
}

void CMyListCtrl::RefreshSort()
{
	HDITEM header;
	header.mask = HDI_FORMAT;
	GetHeaderCtrl()->GetItem(m_sorted_column, &header);
	SortItems(CompareFunc, header.fmt & HDF_SORTDOWN ? m_sorted_column | 0x80000000 : m_sorted_column);
}
