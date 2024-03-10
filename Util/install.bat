REM USAGE: Install.bat <DEBUG/RELEASE> <UUID>
REM Example: Install.bat RELEASE com.barraider.spotify
setlocal
cd /d %~dp0

REM Change to the parent directory where the .csproj file is located
cd ..

REM *** MAKE SURE THE FOLLOWING VARIABLES ARE CORRECT ***
REM The build configuration (DEBUG/RELEASE) is passed as the first argument to the script
SET BUILD_CONFIGURATION=%1
REM The UUID of the plugin is passed as the second argument to the script
SET PLUGIN_UUID=%2

REM (Distribution tool can be downloaded from: https://docs.elgato.com/sdk/plugins/packaging )
REM (Check the paths if they are correct for your system.)
SET OUTPUT_DIR="C:\TEMP"
SET DISTRIBUTION_TOOL="C:\Program Files\Elgato\StreamDeck\DistributionTool.exe"
SET STREAM_DECK_FILE="C:\Program Files\Elgato\StreamDeck\StreamDeck.exe"
SET STREAM_DECK_LOAD_TIMEOUT=7

REM Kill any running StreamDeck processes
taskkill /f /im streamdeck.exe
taskkill /f /im %PLUGIN_UUID%.exe
timeout /t 2

REM Delete old plugin file if exists
del %OUTPUT_DIR%\%PLUGIN_UUID%.streamDeckPlugin

REM Restore dependencies and build the project using dotnet CLI
dotnet restore --no-cache
dotnet build -c %BUILD_CONFIGURATION% --no-restore

REM Create the plugin with the DistributionTool
cd bin\%1
%DISTRIBUTION_TOOL% -b -i %PLUGIN_UUID%.sdPlugin -o %OUTPUT_DIR%

REM Remove old plugin data
rmdir %APPDATA%\Elgato\StreamDeck\Plugins\%PLUGIN_UUID%.sdPlugin /s /q

REM Start StreamDeck application
START "" %STREAM_DECK_FILE%
timeout /t %STREAM_DECK_LOAD_TIMEOUT%

REM Install the new plugin
%OUTPUT_DIR%\%PLUGIN_UUID%.streamDeckPlugin