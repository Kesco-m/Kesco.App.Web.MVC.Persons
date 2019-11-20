ECHO OFF
ECHO Утилита подготовки релизной сборки web-проекта

IF [%1] == []  GOTO EMPTYPARAMS1
IF [%2] == []  GOTO EMPTYPARAMS2

SET excl=%TEMP%\exclude.txt


ECHO .pdb\ > %excl%
ECHO \obj\ >> %excl%
ECHO .publish.xml\ >> %excl%
ECHO .cs\ >> %excl%
ECHO .resx\ >> %excl%
ECHO .sln >> %excl%
ECHO .csproj\ >> %excl%
ECHO .suo\ >> %excl%
ECHO .user\ >> %excl%
ECHO .scc\ >> %excl%
ECHO \media\ >> %excl%
ECHO \App_Config\ >> %excl%


RMDIR /s /q "%1\_Application\"
XCOPY /r /i /s /y /d /f  %1%2 %1\_Application /exclude:%excl%
XCOPY /r /i /s /y /d /f  %1\_ClientResources %1\_Application\bin 
DEL %excl%
ECHO .cs\ >> %excl%
XCOPY /r /i /s /y /d /f  %1%2\App_GlobalResources %1\_Application\App_GlobalResources /exclude:%excl%

DEL /s /q  %1\_Application\bin\*.xml
DEL /s /q  %1\_Application\bin\*.config
DEL /s /q  %1\_Application\packages.config
DEL /s /q  %1\_Application\SampleTemplate.cshtml

DEL %excl%
EXIT %ERRORLEVEL%

:EMPTYPARAMS1
ECHO Не указан параметр 1 - путь к папке решения
EXIT /B 1

:EMPTYPARAMS2
ECHO Не указан параметр 2 - название проекта
EXIT /B 2