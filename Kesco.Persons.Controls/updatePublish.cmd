robocopy "C:\PROJECTS\Kesco\Kesco.Persons\Kesco.Persons.Controls\bin" "C:\_PUBLISH\Web_Data\MVC" "Kesco.Persons.*.dll" "Kesco.Persons.*.pdb" "Kesco.Persons.*.xml" /S /XD .svn /R:1
dir %TEMP% > nul