@ECHO OFF
echo *******************
echo 1. Build
echo *******************
"c:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" %~dp0MailingDemon.csproj /property:Configuration=Debug /verbosity:minimal 
IF %ERRORLEVEL% == 0 GOTO Install
IF NOT %ERRORLEVEL% == 0 GOTO END

:Install
echo.
echo *******************
echo 2. Install service
echo *******************
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe %~dp0bin\MailingDemon.exe

:END
echo.
pause

