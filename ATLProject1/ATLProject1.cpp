// ATLProject1.cpp : Implementation of WinMain


#include "stdafx.h"
#include "resource.h"
#include "ATLProject1_i.h"


using namespace ATL;


class CATLProject1Module : public ATL::CAtlExeModuleT< CATLProject1Module >
{
public :
	DECLARE_LIBID(LIBID_ATLProject1Lib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_ATLPROJECT1, "{048305D8-FEBD-4FCF-8E89-004B819ABF41}")
	};

CATLProject1Module _AtlModule;



//
extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
								LPTSTR /*lpCmdLine*/, int nShowCmd)
{
	return _AtlModule.WinMain(nShowCmd);
}

