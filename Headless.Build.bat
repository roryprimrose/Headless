set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319

call %msBuildDir%\msbuild headless.build.proj /verbosity:normal

pause