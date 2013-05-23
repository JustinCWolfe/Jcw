set SOLUTION_DIR=%1
set CONFIG=%2
set BIN=%SOLUTION_DIR%bin
set LIB=%SOLUTION_DIR%lib
set BUILD_DIR=bin\%CONFIG%

IF NOT EXIST %BIN% (
    MD %BIN%
)

xcopy "%SOLUTION_DIR%Jcw.Resources\%BUILD_DIR%\Jcw.Resources.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Resources\%BUILD_DIR%\Jcw.Resources.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Search\%BUILD_DIR%\Jcw.Search.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Search\%BUILD_DIR%\Jcw.Search.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Common\%BUILD_DIR%\Jcw.Common.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Common\%BUILD_DIR%\Jcw.Common.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Common.Gui\%BUILD_DIR%\Jcw.Common.Gui.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Common.Gui\%BUILD_DIR%\Jcw.Common.Gui.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Common.Gui.WinForms\%BUILD_DIR%\Jcw.Common.Gui.WinForms.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Common.Gui.WinForms\%BUILD_DIR%\Jcw.Common.Gui.WinForms.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Charting\%BUILD_DIR%\Jcw.Charting.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Charting\%BUILD_DIR%\Jcw.Charting.pdb" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Charting\%BUILD_DIR%\Jcw.Charting.XmlSerializers.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Charting\%BUILD_DIR%\Jcw.Charting.XmlSerializers.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Charting.Gui.WinForms\%BUILD_DIR%\Jcw.Charting.Gui.WinForms.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Charting.Gui.WinForms\%BUILD_DIR%\Jcw.Charting.Gui.WinForms.pdb" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Charting.Gui.WinForms\%BUILD_DIR%\Jcw.Charting.Gui.WinForms.XmlSerializers.dll" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Charting.Gui.WinForms\%BUILD_DIR%\Jcw.Charting.Gui.WinForms.XmlSerializers.pdb" "%BIN%" /R /O /Y

xcopy "%SOLUTION_DIR%Jcw.Resources\Resources\ChartNote.gif" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Resources\Resources\CircleMarker.gif" "%BIN%" /R /O /Y
xcopy "%SOLUTION_DIR%Jcw.Resources\Resources\SquareMarker.gif" "%BIN%" /R /O /Y

xcopy "%LIB%\BlueTools.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\BlueToolsMS.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\BlueToolsWC.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\BlueToolsWC150.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\Franson.BlueTools.200.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.BonusSkins.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.Data.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.Design.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.Office.v12.1.Core.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.Printing.v12.1.Core.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.Utils.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraBars.v12.1.Design.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraBars.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraEditors.v12.1.Design.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraEditors.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraNavBar.v12.1.Design.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraNavBar.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraTreeList.v12.1.Design.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\DevExpress.XtraTreeList.v12.1.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\ICSharpCode.SharpZipLib.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\log4net.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\Lucene.Net.dll" "%BIN%" /R /O /Y
xcopy "%LIB%\netchartdir.dll" "%BIN%" /R /O /Y

