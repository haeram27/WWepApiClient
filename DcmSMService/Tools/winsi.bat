@echo off
rem === windows service installer ===
rem windows service manual install tool
rem gitub: https://github.com/haeram27
rem error report: haeram27@gmail

cd %~dp0

setlocal ENABLEEXTENSIONS
if errorlevel 1 (
    echo Error: Can't use Command extensions...
)

set keynamev4_="HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"
set valuename_="InstallPath"

for /f "usebackq skip=2 tokens=1-2*" %%A in (`reg query %keynamev4_% /v %valuename_%`) do (
    set dotnethome_=%%C
    echo INFO: .NET Framework Install Path - REGISTRY 
)

if not defined dotnethome_ (
    set dotnethome_=C:\Windows\Microsoft.NET\Framework\v4.0.30319\
    echo INFO: .NET Framework Install Path - PREDEFINED
)

set installutil_="%dotnethome_%"InstallUtil.exe
%installutil_% 2>&1 >nul
if %errorlevel%=="-1" (
    echo Error: Can't find .NET Framework Install Path...
    goto _end
)

if "%1"=="inst" (
    goto _inst
)
if "%1"=="uninst" (
    goto _uninst
)
if "%1"=="start" (
    goto _start
)
if "%1"=="stop" (
    goto _stop
) 
goto _usage


:_inst
echo INFO: Installing service...
%installutil_% %2
goto _end


:_uninst
echo INFO: Uninstalling service...
%installutil_% /u %2
sc delete %2
goto _end


:_start
echo INFO: Starting service...
sc start %2
goto _end


:_stop
echo INFO: Stopping service...
sc stop %2
goto _end


:_usage
echo.
echo.
echo Windows Service Manual Installer
echo usage: 
echo     winsi.bat ^<option^> ^<service name^>
echo.
echo option: 
echo     inst
echo     uninst
echo     start
echo     stop
echo.
echo error report: ^<haeram27@gmail.com^>
echo.


:_end
pause