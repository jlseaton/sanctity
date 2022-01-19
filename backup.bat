@echo off
REM Makes a backup of all source code, ignoring specific directories and files
"C:\Program Files\7-Zip\7z.exe" a game.zip . -r -xr!bin -xr!obj -x!packages -x!.vs -x!.git -x!testresults -x!update -x!Publish
