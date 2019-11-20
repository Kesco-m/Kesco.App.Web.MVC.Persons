echo OFF
::chcp 1251 > nul
IF [%DEVASSEMBLYCACHEDIR%] == [] GOTO NODEVASSEMBLYCACHEDIR
IF [%1] == []  GOTO TARGETDIRNOTSPECIFIEDORNOTEXIST
echo %1
IF [%2] == []  GOTO TARGETNAMENOTSPECIFIED
echo %2

robocopy %1 "%DEVASSEMBLYCACHEDIR%Web_Data\MVC" "%2*.dll" "%2*.pdb" "%2*.xml" /S /XD .svn /R:1
dir %TEMP% > nul
EXIT %ERRORLEVEL%

:NODEVASSEMBLYCACHEDIR
ECHO "Директория сборок не определена. Укажите переменную среды DEVASSEMBLYCACHEDIR."
EXIT 1

:TARGETDIRNOTSPECIFIEDORNOTEXIST
ECHO "Целевая директория со сборкой не указана или не существует."
EXIT 2

:TARGETNAMENOTSPECIFIED
ECHO "Целевое имя сбоки не указано"
EXIT 3
