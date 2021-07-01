// CMyListCtrl.cpp : implementation file
//

#include "pch.h"
#include "CourseManager.h"
#include "CMyListCtrl.h"


// CMyListCtrl

IMPLEMENT_DYNAMIC(CMyListCtrl, CListCtrl)

CMyListCtrl::CMyListCtrl()
{

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

	static int sorted_header = -1;
	if (phdr->iItem != sorted_header) {
		GetHeaderCtrl()->GetItem(sorted_header, &header);
		header.fmt = header.fmt & ~HDF_SORTDOWN & ~HDF_SORTUP;
		GetHeaderCtrl()->SetItem(sorted_header, &header);
		sorted_header = phdr->iItem;
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

	SortItems(CompareFunc, header.fmt & HDF_SORTDOWN ? phdr->iItem | 0x80000000 : phdr->iItem);

	*pResult = 0;
}