@echo off
:: Start the VS dev environment first
call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat"

:: Then run your existing build.bat
cd /d D:\Projects\UNITY\MyGame\NativePlugin
call build.bat
