md dds_Original
copy "%CD%\*.dds" "%CD%\dds_Original"
texconv.exe *.dds -ft dds -f BC3_UNORM -y
pause
