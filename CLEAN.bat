@echo off

: By:        Fallensn0w
: Usage:  Clean up before pushing to gitHub.


set AppName=Zeratul


: del %CD%\%AppName%.exe
del %CD%\%AppName%.bat

del %CD%\%AppName%.pdb
del %CD%\%AppName%.xml


del %CD%\%AppName%\bin\Debug\%AppName%.exe
del %CD%\%AppName%\bin\Debug\%AppName%.pdb
del %CD%\%AppName%\bin\Debug\%AppName%.xml


del %CD%\%AppName%\obj\Release\%AppName%.exe
del %CD%\%AppName%\obj\Release\%AppName%.pdb
del %CD%\%AppName%\obj\Release\%AppName%.xml


del %CD%\%AppName%\obj\Debug\%AppName%.exe
del %CD%\%AppName%\obj\Debug\%AppName%.pdb
del %CD%\%AppName%\obj\Debug\%AppName%.xml