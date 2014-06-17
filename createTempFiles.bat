@echo off

mkdir UT3SteamPatch\bin\Debug\Patch\Binaries
mkdir UT3SteamPatch\bin\Debug\Patch\Temp
fsutil file createnew UT3SteamPatch\bin\Debug\Patch\Binaries\ut3.exe 28505368
fsutil file createnew UT3SteamPatch\bin\Debug\Patch\Temp\big.bin 2147483648

mkdir UT3SteamPatch\bin\Release\Patch\Binaries
mkdir UT3SteamPatch\bin\Release\Patch\Temp
fsutil file createnew UT3SteamPatch\bin\Release\Patch\Binaries\ut3.exe 28505368
fsutil file createnew UT3SteamPatch\bin\Release\Patch\Temp\big.bin 2147483648


echo {\rtf1 TEMP} > UT3SteamPatch\bin\Debug\EULA.rtf
echo {\rtf1 TEMP} > UT3SteamPatch\bin\Release\EULA.rtf

pause