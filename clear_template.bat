@echo off
echo Limpando pastas bin, obj e node_modules...

for /d /r %%d in (bin, obj, node_modules, Uploads) do (
    if exist "%%d" (
        echo Excluindo: %%d
        rmdir /s /q "%%d"
    )
)

echo Limpeza concluída.
pause
