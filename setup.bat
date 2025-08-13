@echo off
REM 🚀 Script de setup automatique pour Contact Management API (Windows)
REM Usage: setup.bat [port]

setlocal enabledelayedexpansion

REM Port par défaut
set PORT=%1
if "%PORT%"=="" set PORT=5003

echo 🚀 Contact Management API - Setup Automatique
echo ================================================

REM Vérifier les prérequis
echo.
echo 📋 Vérification des prérequis...

REM Vérifier .NET 8
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo ❌ .NET SDK n'est pas installé
    echo 💡 Installez .NET 8 SDK depuis: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

for /f "tokens=*" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo ✅ .NET SDK version: %DOTNET_VERSION%

REM Restaurer les packages
echo.
echo 📦 Restauration des packages NuGet...
dotnet restore
if errorlevel 1 (
    echo ❌ Erreur lors de la restauration des packages
    pause
    exit /b 1
)
echo ✅ Packages restaurés

REM Installer dotnet-ef si nécessaire
echo.
echo 🔧 Vérification des outils Entity Framework...
dotnet ef --version >nul 2>&1
if errorlevel 1 (
    echo 📥 Installation de dotnet-ef...
    dotnet tool install --global dotnet-ef
    if errorlevel 1 (
        echo ❌ Erreur lors de l'installation de dotnet-ef
        pause
        exit /b 1
    )
    echo ✅ dotnet-ef installé
) else (
    for /f "tokens=*" %%i in ('dotnet ef --version') do set EF_VERSION=%%i
    echo ✅ dotnet-ef version: !EF_VERSION!
)

REM Créer/Mettre à jour la base de données
echo.
echo 🗄️  Configuration de la base de données SQLite...
if exist "ContactManagement.db" (
    echo 📄 Base de données existante trouvée
    set /p RECREATE="Voulez-vous la recréer ? (y/N): "
    if /i "!RECREATE!"=="y" (
        del ContactManagement.db
        echo 🗑️  Ancienne base supprimée
    )
)

dotnet ef database update
if errorlevel 1 (
    echo ❌ Erreur lors de la configuration de la base de données
    pause
    exit /b 1
)
echo ✅ Base de données configurée

REM Compiler le projet
echo.
echo 🔨 Compilation du projet...
dotnet build
if errorlevel 1 (
    echo ❌ Erreur lors de la compilation
    pause
    exit /b 1
)
echo ✅ Projet compilé avec succès

REM Vérifier si le port est libre
echo.
echo 🔍 Vérification du port %PORT%...
netstat -an | findstr ":%PORT% " >nul 2>&1
if not errorlevel 1 (
    echo ❌ Port %PORT% déjà utilisé
    echo 💡 Tuons les processus dotnet existants...
    taskkill /f /im dotnet.exe >nul 2>&1
    timeout /t 2 >nul
)

echo ✅ Port %PORT% disponible

REM Résumé
echo.
echo 🎉 Setup terminé avec succès !
echo ================================================
echo 📱 Interface utilisateur: http://localhost:%PORT%
echo 📚 API Swagger: http://localhost:%PORT%/swagger
echo 🔗 API Endpoints: http://localhost:%PORT%/api/contacts
echo ================================================

REM Proposer de lancer l'application
echo.
set /p LAUNCH="🚀 Voulez-vous lancer l'application maintenant ? (Y/n): "
if /i not "%LAUNCH%"=="n" (
    echo 🚀 Lancement de l'application...
    echo 💡 Appuyez sur Ctrl+C pour arrêter
    echo ================================================
    dotnet run --urls="http://localhost:%PORT%"
) else (
    echo 💡 Pour lancer manuellement:
    echo    dotnet run --urls="http://localhost:%PORT%"
    pause
)

endlocal
