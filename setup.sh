#!/bin/bash

# 🚀 Script de setup automatique pour Contact Management API
# Usage: ./setup.sh [port]

set -e  # Arrêter en cas d'erreur

# Couleurs pour les messages
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Port par défaut
PORT=${1:-5003}

echo -e "${BLUE}🚀 Contact Management API - Setup Automatique${NC}"
echo -e "${BLUE}================================================${NC}"

# Vérifier les prérequis
echo -e "\n${YELLOW}📋 Vérification des prérequis...${NC}"

# Vérifier .NET 8
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}❌ .NET SDK n'est pas installé${NC}"
    echo -e "${YELLOW}💡 Installez .NET 8 SDK depuis: https://dotnet.microsoft.com/download${NC}"
    exit 1
fi

DOTNET_VERSION=$(dotnet --version)
echo -e "${GREEN}✅ .NET SDK version: $DOTNET_VERSION${NC}"

# Vérifier la version .NET 8
if [[ ! $DOTNET_VERSION == 8.* ]]; then
    echo -e "${YELLOW}⚠️  Version .NET 8 recommandée (version actuelle: $DOTNET_VERSION)${NC}"
fi

# Restaurer les packages
echo -e "\n${YELLOW}📦 Restauration des packages NuGet...${NC}"
dotnet restore
echo -e "${GREEN}✅ Packages restaurés${NC}"

# Installer dotnet-ef si nécessaire
echo -e "\n${YELLOW}🔧 Vérification des outils Entity Framework...${NC}"
if ! command -v dotnet-ef &> /dev/null; then
    echo -e "${YELLOW}📥 Installation de dotnet-ef...${NC}"
    dotnet tool install --global dotnet-ef
    echo -e "${GREEN}✅ dotnet-ef installé${NC}"
else
    EF_VERSION=$(dotnet ef --version)
    echo -e "${GREEN}✅ dotnet-ef version: $EF_VERSION${NC}"
fi

# Créer/Mettre à jour la base de données
echo -e "\n${YELLOW}🗄️  Configuration de la base de données SQLite...${NC}"
if [ -f "ContactManagement.db" ]; then
    echo -e "${YELLOW}📄 Base de données existante trouvée${NC}"
    read -p "Voulez-vous la recréer ? (y/N): " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        rm ContactManagement.db
        echo -e "${YELLOW}🗑️  Ancienne base supprimée${NC}"
    fi
fi

dotnet ef database update
echo -e "${GREEN}✅ Base de données configurée${NC}"

# Compiler le projet
echo -e "\n${YELLOW}🔨 Compilation du projet...${NC}"
dotnet build
echo -e "${GREEN}✅ Projet compilé avec succès${NC}"

# Vérifier si le port est libre
echo -e "\n${YELLOW}🔍 Vérification du port $PORT...${NC}"
if lsof -Pi :$PORT -sTCP:LISTEN -t >/dev/null 2>&1; then
    echo -e "${RED}❌ Port $PORT déjà utilisé${NC}"
    echo -e "${YELLOW}💡 Tuons les processus dotnet existants...${NC}"
    pkill -f "dotnet run" || true
    sleep 2
    
    # Vérifier à nouveau
    if lsof -Pi :$PORT -sTCP:LISTEN -t >/dev/null 2>&1; then
        echo -e "${RED}❌ Port $PORT toujours occupé${NC}"
        echo -e "${YELLOW}💡 Essayez un autre port: ./setup.sh 5004${NC}"
        exit 1
    fi
fi

echo -e "${GREEN}✅ Port $PORT disponible${NC}"

# Résumé
echo -e "\n${GREEN}🎉 Setup terminé avec succès !${NC}"
echo -e "${BLUE}================================================${NC}"
echo -e "${GREEN}📱 Interface utilisateur: ${BLUE}http://localhost:$PORT${NC}"
echo -e "${GREEN}📚 API Swagger: ${BLUE}http://localhost:$PORT/swagger${NC}"
echo -e "${GREEN}🔗 API Endpoints: ${BLUE}http://localhost:$PORT/api/contacts${NC}"
echo -e "${BLUE}================================================${NC}"

# Proposer de lancer l'application
echo -e "\n${YELLOW}🚀 Voulez-vous lancer l'application maintenant ? (Y/n):${NC}"
read -n 1 -r
echo
if [[ ! $REPLY =~ ^[Nn]$ ]]; then
    echo -e "${GREEN}🚀 Lancement de l'application...${NC}"
    echo -e "${YELLOW}💡 Appuyez sur Ctrl+C pour arrêter${NC}"
    echo -e "${BLUE}================================================${NC}"
    dotnet run --urls="http://localhost:$PORT"
else
    echo -e "${YELLOW}💡 Pour lancer manuellement:${NC}"
    echo -e "${BLUE}   dotnet run --urls=\"http://localhost:$PORT\"${NC}"
fi
