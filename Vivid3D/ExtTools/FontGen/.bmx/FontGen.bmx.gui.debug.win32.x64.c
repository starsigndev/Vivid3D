#include "FontGen.bmx.gui.debug.win32.x64.h"
struct BBString_4{BBClass_String* clas;BBULONG hash;int length;BBChar buf[4];};
struct BBString_3{BBClass_String* clas;BBULONG hash;int length;BBChar buf[3];};
struct BBString_15{BBClass_String* clas;BBULONG hash;int length;BBChar buf[15];};
static struct BBString_4 _s14={
	&bbStringClass,
	0x2fe105a741f7b240,
	4,
	{32,67,72,58}
};
static struct BBString_3 _s12={
	&bbStringClass,
	0xa7f377fa77315946,
	3,
	{46,112,102}
};
static struct BBString_3 _s13={
	&bbStringClass,
	0xd4ed6e3196c6da14,
	3,
	{67,87,58}
};
static struct BBString_15 _s11={
	&bbStringClass,
	0xbf22078e444a77ee,
	15,
	{115,121,115,116,101,109,102,111,110,116,50,46,116,116,102}
};
struct BBDebugScope_1{int kind; const char *name; BBDebugDecl decls[2]; };
struct BBDebugScope_2{int kind; const char *name; BBDebugDecl decls[3]; };
struct BBDebugScope_3{int kind; const char *name; BBDebugDecl decls[4]; };
struct BBDebugScope_4{int kind; const char *name; BBDebugDecl decls[5]; };
static int _bb_main_inited = 0;
int _bb_main(){
	if (!_bb_main_inited) {
		_bb_main_inited = 1;
		__bb_brl_blitz_blitz();
		__bb_brl_appstub_appstub();
		__bb_brl_audio_audio();
		__bb_brl_bank_bank();
		__bb_brl_bankstream_bankstream();
		__bb_brl_base64_base64();
		__bb_brl_basic_basic();
		__bb_brl_bmploader_bmploader();
		__bb_brl_bytebuffer_bytebuffer();
		__bb_brl_clipboard_clipboard();
		__bb_brl_collections_collections();
		__bb_brl_d3d7max2d_d3d7max2d();
		__bb_brl_d3d9max2d_d3d9max2d();
		__bb_brl_directsoundaudio_directsoundaudio();
		__bb_brl_eventqueue_eventqueue();
		__bb_brl_freeaudioaudio_freeaudioaudio();
		__bb_brl_freetypefont_freetypefont();
		__bb_brl_glgraphics_glgraphics();
		__bb_brl_glmax2d_glmax2d();
		__bb_brl_gnet_gnet();
		__bb_brl_jpgloader_jpgloader();
		__bb_brl_map_map();
		__bb_brl_matrix_matrix();
		__bb_brl_maxlua_maxlua();
		__bb_brl_maxunit_maxunit();
		__bb_brl_maxutil_maxutil();
		__bb_brl_objectlist_objectlist();
		__bb_brl_oggloader_oggloader();
		__bb_brl_openalaudio_openalaudio();
		__bb_brl_platform_platform();
		__bb_brl_pngloader_pngloader();
		__bb_brl_polygon_polygon();
		__bb_brl_quaternion_quaternion();
		__bb_brl_retro_retro();
		__bb_brl_tgaloader_tgaloader();
		__bb_brl_threadpool_threadpool();
		__bb_brl_timer_timer();
		__bb_brl_timerdefault_timerdefault();
		__bb_brl_utf8stream_utf8stream();
		__bb_brl_uuid_uuid();
		__bb_brl_volumes_volumes();
		__bb_brl_wavloader_wavloader();
		__bb_pub_freejoy_freejoy();
		__bb_pub_freeprocess_freeprocess();
		__bb_pub_glad_glad();
		__bb_pub_nfd_nfd();
		__bb_pub_nx_nx();
		__bb_pub_opengles_opengles();
		__bb_pub_opengles3_opengles3();
		__bb_pub_vulkan_vulkan();
		__bb_pub_xmmintrin_xmmintrin();
		bbRegisterSource(0x257047f300739c7, "C:/G3D/Vivid3D/ExtTools/FontGen/FontGen.bmx");
		BBSTRING bbt_lf=(&bbEmptyString);
		struct brl_max2d_imagefont_TImageFont_obj* bbt_font=(struct brl_max2d_imagefont_TImageFont_obj*)((struct brl_max2d_imagefont_TImageFont_obj*)&bbNullObject);
		struct brl_stream_TStream_obj* bbt_op=(struct brl_stream_TStream_obj*)((struct brl_stream_TStream_obj*)&bbNullObject);
		struct BBDebugScope_3 __scope = {
			BBDEBUGSCOPE_FUNCTION,
			"FontGen",
			{
				{
					BBDEBUGDECL_LOCAL,
					"lf",
					"$",
					.var_address=&bbt_lf
				},
				{
					BBDEBUGDECL_LOCAL,
					"font",
					":TImageFont",
					.var_address=&bbt_font
				},
				{
					BBDEBUGDECL_LOCAL,
					"op",
					":TStream",
					.var_address=&bbt_op
				},
				{
					BBDEBUGDECL_END
				}
			}
		};
		bbOnDebugEnterScope((BBDebugScope *)&__scope);
		struct BBDebugStm __stmt_0 = {0x257047f300739c7, 2, 0};
		bbOnDebugEnterStm(&__stmt_0);
		brl_graphics_Graphics(800,600,0,60,0LL,-1,-1);
		struct BBDebugStm __stmt_1 = {0x257047f300739c7, 4, 0};
		bbOnDebugEnterStm(&__stmt_1);
		bbt_lf=((BBString*)&_s11);
		struct BBDebugStm __stmt_2 = {0x257047f300739c7, 6, 0};
		bbOnDebugEnterStm(&__stmt_2);
		bbt_font=(struct brl_max2d_imagefont_TImageFont_obj*)brl_max2d_LoadImageFont((BBOBJECT)bbt_lf,38,4);
		struct BBDebugStm __stmt_3 = {0x257047f300739c7, 8, 0};
		bbOnDebugEnterStm(&__stmt_3);
		brl_max2d_SetImageFont((struct brl_max2d_imagefont_TImageFont_obj*)bbt_font);
		struct BBDebugStm __stmt_4 = {0x257047f300739c7, 13, 0};
		bbOnDebugEnterStm(&__stmt_4);
		bbt_op=(struct brl_stream_TStream_obj*)brl_filesystem_WriteFile((BBOBJECT)bbStringConcat(bbt_lf,((BBString*)&_s12)));
		struct BBDebugStm __stmt_5 = {0x257047f300739c7, 17, 0};
		bbOnDebugEnterStm(&__stmt_5);
		{
			BBINT bbt_i=0;
			for(;(bbt_i<256);bbt_i=(bbt_i+1)){
				BBINT bbt_cw=0;
				BBINT bbt_ch=0;
				struct brl_pixmap_TPixmap_obj* bbt_pm=(struct brl_pixmap_TPixmap_obj*)((struct brl_pixmap_TPixmap_obj*)&bbNullObject);
				struct BBDebugScope_4 __scope = {
					BBDEBUGSCOPE_LOCALBLOCK,
					0,
					{
						{
							BBDEBUGDECL_LOCAL,
							"i",
							"i",
							.var_address=&bbt_i
						},
						{
							BBDEBUGDECL_LOCAL,
							"cw",
							"i",
							.var_address=&bbt_cw
						},
						{
							BBDEBUGDECL_LOCAL,
							"ch",
							"i",
							.var_address=&bbt_ch
						},
						{
							BBDEBUGDECL_LOCAL,
							"pm",
							":TPixmap",
							.var_address=&bbt_pm
						},
						{
							BBDEBUGDECL_END
						}
					}
				};
				bbOnDebugEnterScope((BBDebugScope *)&__scope);
				struct BBDebugStm __stmt_0 = {0x257047f300739c7, 19, 0};
				bbOnDebugEnterStm(&__stmt_0);
				brl_max2d_Cls();
				struct BBDebugStm __stmt_1 = {0x257047f300739c7, 21, 0};
				bbOnDebugEnterStm(&__stmt_1);
				brl_max2d_DrawText(bbStringFromChar(bbt_i),0.00000000f,0.00000000f);
				struct BBDebugStm __stmt_2 = {0x257047f300739c7, 23, 0};
				bbOnDebugEnterStm(&__stmt_2);
				bbt_cw=brl_max2d_TextWidth(bbStringFromChar(bbt_i));
				struct BBDebugStm __stmt_3 = {0x257047f300739c7, 24, 0};
				bbOnDebugEnterStm(&__stmt_3);
				bbt_ch=brl_max2d_TextHeight(bbStringFromChar(bbt_i));
				struct BBDebugStm __stmt_4 = {0x257047f300739c7, 26, 0};
				bbOnDebugEnterStm(&__stmt_4);
				brl_standardio_Print(bbStringConcat(bbStringConcat(bbStringConcat(((BBString*)&_s13),bbStringFromInt(bbt_cw)),((BBString*)&_s14)),bbStringFromInt(bbt_ch)));
				struct BBDebugStm __stmt_5 = {0x257047f300739c7, 28, 0};
				bbOnDebugEnterStm(&__stmt_5);
				brl_stream_WriteInt((struct brl_stream_TStream_obj*)bbt_op,bbt_cw);
				struct BBDebugStm __stmt_6 = {0x257047f300739c7, 29, 0};
				bbOnDebugEnterStm(&__stmt_6);
				brl_stream_WriteInt((struct brl_stream_TStream_obj*)bbt_op,bbt_ch);
				struct BBDebugStm __stmt_7 = {0x257047f300739c7, 31, 0};
				bbOnDebugEnterStm(&__stmt_7);
				bbt_pm=(struct brl_pixmap_TPixmap_obj*)brl_max2d_GrabPixmap(0,0,bbt_cw,bbt_ch);
				struct BBDebugStm __stmt_8 = {0x257047f300739c7, 32, 0};
				bbOnDebugEnterStm(&__stmt_8);
				bbt_pm=(struct brl_pixmap_TPixmap_obj*)brl_pixmap_ConvertPixmap((struct brl_pixmap_TPixmap_obj*)bbt_pm,6);
				struct BBDebugStm __stmt_9 = {0x257047f300739c7, 35, 0};
				bbOnDebugEnterStm(&__stmt_9);
				{
					BBINT bbt_py=0;
					BBINT bbt_=bbt_ch;
					for(;(bbt_py<bbt_);bbt_py=(bbt_py+1)){
						struct BBDebugScope_1 __scope = {
							BBDEBUGSCOPE_LOCALBLOCK,
							0,
							{
								{
									BBDEBUGDECL_LOCAL,
									"py",
									"i",
									.var_address=&bbt_py
								},
								{
									BBDEBUGDECL_END
								}
							}
						};
						bbOnDebugEnterScope((BBDebugScope *)&__scope);
						struct BBDebugStm __stmt_0 = {0x257047f300739c7, 36, 0};
						bbOnDebugEnterStm(&__stmt_0);
						{
							BBINT bbt_px=0;
							BBINT bbt_2=bbt_cw;
							for(;(bbt_px<bbt_2);bbt_px=(bbt_px+1)){
								BBBYTE* bbt_buf=0;
								struct BBDebugScope_2 __scope = {
									BBDEBUGSCOPE_LOCALBLOCK,
									0,
									{
										{
											BBDEBUGDECL_LOCAL,
											"px",
											"i",
											.var_address=&bbt_px
										},
										{
											BBDEBUGDECL_LOCAL,
											"buf",
											"*b",
											.var_address=&bbt_buf
										},
										{
											BBDEBUGDECL_END
										}
									}
								};
								bbOnDebugEnterScope((BBDebugScope *)&__scope);
								struct BBDebugStm __stmt_0 = {0x257047f300739c7, 38, 0};
								bbOnDebugEnterStm(&__stmt_0);
								bbt_buf=brl_pixmap_PixmapPixelPtr((struct brl_pixmap_TPixmap_obj*)bbt_pm,bbt_px,bbt_py);
								struct BBDebugStm __stmt_1 = {0x257047f300739c7, 40, 0};
								bbOnDebugEnterStm(&__stmt_1);
								brl_stream_WriteByte((struct brl_stream_TStream_obj*)bbt_op,((BBINT)bbt_buf[0]));
								struct BBDebugStm __stmt_2 = {0x257047f300739c7, 41, 0};
								bbOnDebugEnterStm(&__stmt_2);
								brl_stream_WriteByte((struct brl_stream_TStream_obj*)bbt_op,((BBINT)bbt_buf[1]));
								struct BBDebugStm __stmt_3 = {0x257047f300739c7, 42, 0};
								bbOnDebugEnterStm(&__stmt_3);
								brl_stream_WriteByte((struct brl_stream_TStream_obj*)bbt_op,((BBINT)bbt_buf[2]));
								struct BBDebugStm __stmt_4 = {0x257047f300739c7, 43, 0};
								bbOnDebugEnterStm(&__stmt_4);
								brl_stream_WriteByte((struct brl_stream_TStream_obj*)bbt_op,((BBINT)bbt_buf[3]));
								bbOnDebugLeaveScope();
							}
						}
						bbOnDebugLeaveScope();
					}
				}
				bbOnDebugLeaveScope();
			}
		}
		struct BBDebugStm __stmt_6 = {0x257047f300739c7, 51, 0};
		bbOnDebugEnterStm(&__stmt_6);
		((struct brl_stream_TStream_obj*)bbNullObjectTest((BBObject*)bbt_op))->clas->m_Flush((struct brl_stream_TIO_obj*)bbt_op);
		struct BBDebugStm __stmt_7 = {0x257047f300739c7, 53, 0};
		bbOnDebugEnterStm(&__stmt_7);
		((struct brl_stream_TStream_obj*)bbNullObjectTest((BBObject*)bbt_op))->clas->m_Close((struct brl_stream_TIO_obj*)bbt_op);
		bbOnDebugLeaveScope();
		return 0;
	}
	return 0;
}