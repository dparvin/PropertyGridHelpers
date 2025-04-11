@echo off
setlocal

REM Set the path to SHFBConsole.exe
REM Adjust this if SHFB is not on the PATH or installed to a different location
set SHFBEXE=SHFBConsole.exe

REM Get the directory this batch file is in
set "SCRIPT_DIR=%~dp0"

echo Building Net 4.7.2 documentation...
"%SHFBEXE%" "%SCRIPT_DIR%Net 4.7.2\PropertyGridHelpers.shfbproj" > "%SCRIPT_DIR%Net472_Build.log" 2>&1

echo Building Net 9.0 documentation...
"%SHFBEXE%" "%SCRIPT_DIR%Net 9.0\PropertyGridHelpers.shfbproj" > "%SCRIPT_DIR%Net90_Build.log" 2>&1

echo Build completed. See Net472_Build.log and Net90_Build.log for details.

endlocal
pause
