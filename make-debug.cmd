@echo off
set NETCSC=%Windir%\Microsoft.NET\Framework\v3.5

"%NETCSC%\MSBuild.exe" UT3SteamPatch.sln /p:Configuration=Debug /t:rebuild
pause