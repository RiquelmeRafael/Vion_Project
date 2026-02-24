@echo off
echo ========================================================
echo        VERIFICADOR DE IP PARA VION
echo ========================================================
echo.
echo O seu endereco IPv4 atual e:
echo.
ipconfig | findstr /i "ipv4"
echo.
echo ========================================================
echo COPIE O NUMERO ACIMA (ex: 192.168.x.x)
echo E COLE NO ARQUIVO appsettings.json
echo EM "SiteSettings": { "BaseUrl": "http://SEU_IP:5100" }
echo ========================================================
echo.
pause