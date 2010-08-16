
set AppName=Zeratul

mkdir BUILD

copy readline5.dll %CD%\BUILD
copy irecovery.exe %CD%\BUILD
copy %AppName%.exe %CD%\BUILD
copy README %CD%\BUILD
copy TODO %CD%\BUILD

echo Files are ready to be archived into SFX.
echo ~ Fallensn0w