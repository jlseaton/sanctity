@echo off

rem This will build a release package using Velopack
rem You must supply a version number via a command line argument.
SET version="%1"
IF %version%=="" (
	echo.
	echo ERROR: You must pass a version number, e.g. 1.0.0.
	GOTO :eof
)

rem Strip double quotes
SET version=%version:"=%

@echo Building Release v%version%...

dotnet publish -c Release --self-contained -r win-x64 -o publish

vpk pack -u LordsOfChaos --packTitle "Lords of Chaos" --splashImage .\Images\loctitle.png -v %version% -p .\publish -e Game.Client.exe

rem vpk upload 

goto :eof

:usage
@echo Usage: %0 <version>, e.g. 1.0.0
exit /B 1
