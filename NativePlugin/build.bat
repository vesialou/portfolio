@echo off
setlocal

:: Create build folder if it doesn't exist
if not exist out\build mkdir out\build
cd out\build

:: Run CMake to generate Visual Studio project
cmake ../.. -A x64

:: Build the project in Release mode
cmake --build . --config Release

:: Go back to root
cd ../..
echo === Build finished ===
pause
