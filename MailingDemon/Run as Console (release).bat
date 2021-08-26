@ECHO OFF
echo *******************
echo 1. Build (release)
echo *******************
"c:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" %~dp0MailingDemon.csproj /property:Configuration=Release /verbosity:minimal 
IF %ERRORLEVEL% == 0 GOTO Run
IF NOT %ERRORLEVEL% == 0 GOTO END

:Run
echo.
echo *******************
echo 2. Run
echo *******************
%~dp0bin\MailingDemon.exe

:END
echo.
pause