@echo off

set releaseName=MPE
set releaseVersion=v0.1.0
set pathTo7z="C:\Program Files\7-zip\7z.exe"

set releasePrefix=%releaseName%-%releaseVersion%

for /f %%f in ('dir /b /a:d .\MultiProcessorExtensions\bin\Release') do (
	%pathTo7z% a .\MultiProcessorExtensions\bin\Release\%releasePrefix%-%%f.zip .\MultiProcessorExtensions\bin\Release\%%f\* .\README.md .\LICENSE.md
)