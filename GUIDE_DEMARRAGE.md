# 🚀 Guide de Démarrage - Application de Gestion de Contacts

## 📋 Prérequis

- **.NET 8.0 SDK** ou plus récent
- **Visual Studio 2022** ou **VS Code** avec l'extension C#
- **Git** (optionnel)

## 🛠️ Installation et Configuration

### 1. Vérifier l'installation de .NET

```bash
dotnet --version
```

Vous devriez voir une version 8.0.x ou plus récente.

### 2. Naviguer vers le répertoire du projet

```bash
cd /home/alhoussen/Dev/alh/classroom/contactManagmentC#
```

### 3. Restaurer les packages NuGet

```bash
dotnet restore
```

### 4. Créer les répertoires nécessaires

```bash
mkdir -p wwwroot/uploads/profiles
mkdir -p logs
```

## 🚀 Démarrage de l'Application

### Option 1 : Ligne de commande

```bash
# Démarrer l'application
dotnet run

# Ou avec surveillance des changements (hot reload)
dotnet watch run
```

### Option 2 : Visual Studio

1. Ouvrir le fichier `ContactManagement.Api.csproj` dans Visual Studio
2. Appuyer sur `F5` ou cliquer sur le bouton "Démarrer"

### Option 3 : VS Code

1. Ouvrir le dossier dans VS Code
2. Appuyer sur `F5` ou utiliser `Ctrl+F5`

## 🌐 Accès à l'Application

Une fois l'application démarrée, vous verrez des messages similaires à :

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
      Now listening on: http://localhost:5000
```

### URLs d'accès :

- **Interface Web** : https://localhost:5001 ou http://localhost:5000
- **Documentation API (Swagger)** : https://localhost:5001/swagger
- **API REST** : https://localhost:5001/api/contacts

## 🧪 Tests de l'Application

### 1. Interface Web Moderne

#### Page d'accueil
- Accédez à https://localhost:5001
- Vous devriez voir une interface moderne avec :
  - Navigation fixe en haut
  - Bouton de changement de thème (sombre/clair)
  - Barre de recherche
  - Cartes de statistiques
  - Zone pour afficher les contacts

#### Fonctionnalités à tester :

**🎨 Mode Sombre/Clair**
- Cliquez sur l'icône lune/soleil dans la navigation
- Le thème devrait changer instantanément
- Le choix est sauvegardé dans le navigateur

**➕ Créer un Contact**
- Cliquez sur "Nouveau Contact"
- Remplissez le formulaire modal
- Ajoutez optionnellement une photo de profil
- Cliquez sur "Enregistrer"

**🔍 Recherche en Temps Réel**
- Tapez dans la barre de recherche
- Les résultats se filtrent automatiquement
- Testez avec nom, prénom ou email

**📄 Pagination**
- Si vous avez plus de 12 contacts, la pagination apparaît
- Testez la navigation entre les pages

**✏️ Modifier un Contact**
- Cliquez sur l'icône "crayon" sur une carte de contact
- Le modal s'ouvre avec les données pré-remplies
- Modifiez les informations et sauvegardez

**👁️ Voir les Détails**
- Cliquez sur "Voir" sur une carte de contact
- Une page de détails complète s'affiche
- Testez les actions rapides (email, téléphone)

**🗑️ Supprimer un Contact**
- Cliquez sur l'icône "poubelle"
- Confirmez la suppression
- Le contact disparaît de la liste

### 2. API REST

#### Tester avec Swagger
- Accédez à https://localhost:5001/swagger
- Explorez tous les endpoints disponibles
- Testez directement depuis l'interface Swagger

#### Tester avec curl

```bash
# Récupérer tous les contacts
curl -X GET "https://localhost:5001/api/contacts" -k

# Créer un nouveau contact
curl -X POST "https://localhost:5001/api/contacts" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Jean",
    "lastName": "Dupont",
    "email": "jean.dupont@email.com",
    "phoneNumber": "0123456789",
    "address": "123 Rue de Test, Paris"
  }' -k

# Récupérer un contact par ID
curl -X GET "https://localhost:5001/api/contacts/1" -k

# Mettre à jour un contact
curl -X PUT "https://localhost:5001/api/contacts/1" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Jean-Modifié",
    "lastName": "Dupont",
    "email": "jean.modifie@email.com",
    "phoneNumber": "0123456789"
  }' -k

# Supprimer un contact
curl -X DELETE "https://localhost:5001/api/contacts/1" -k
```

### 3. Tests Unitaires

```bash
# Exécuter tous les tests
dotnet test

# Exécuter les tests avec détails
dotnet test --verbosity normal

# Exécuter les tests avec couverture de code
dotnet test --collect:"XPlat Code Coverage"
```

## 📱 Tests Responsifs

### Desktop (1920x1080)
- Interface complète avec sidebar
- Toutes les fonctionnalités visibles

### Tablette (768x1024)
- Navigation adaptée
- Cartes redimensionnées
- Modal optimisé

### Mobile (375x667)
- Menu hamburger
- Bouton flottant pour ajouter un contact
- Interface tactile optimisée

## 🐛 Résolution des Problèmes Courants

### Erreur de Port Occupé
```bash
# Tuer le processus utilisant le port 5001
sudo lsof -ti:5001 | xargs kill -9

# Ou utiliser un autre port
dotnet run --urls="https://localhost:5002;http://localhost:5003"
```

### Problème de Certificat HTTPS
```bash
# Faire confiance au certificat de développement
dotnet dev-certs https --trust
```

### Base de Données
- L'application utilise une base InMemory en développement
- Les données sont perdues à chaque redémarrage
- C'est normal pour le développement !

### Logs
- Les logs sont visibles dans la console
- Les fichiers de logs sont dans le dossier `logs/`

## 📊 Données de Test

L'application se lance avec 3 contacts de démonstration :
1. Jean Dupont (jean.dupont@email.com)
2. Marie Martin (marie.martin@email.com)  
3. Pierre Durand (pierre.durand@email.com)

## 🎯 Fonctionnalités Implémentées

### ✅ Interface Utilisateur
- [x] Design moderne avec Bootstrap 5
- [x] Mode sombre/clair
- [x] Interface responsive (mobile, tablette, desktop)
- [x] Navigation fixe avec menu
- [x] Cartes pour l'affichage des contacts
- [x] Modals pour CRUD sans rechargement
- [x] Animations et transitions fluides

### ✅ Fonctionnalités
- [x] CRUD complet (Créer, Lire, Modifier, Supprimer)
- [x] Upload et affichage d'images de profil
- [x] Recherche instantanée et filtrage
- [x] Pagination automatique
- [x] Validation côté client et serveur
- [x] Gestion d'erreurs avec notifications toast
- [x] Page de détails complète

### ✅ Technique
- [x] API RESTful avec ASP.NET Core 8
- [x] Entity Framework Core avec InMemory DB
- [x] AutoMapper pour le mapping
- [x] Validation avec Data Annotations
- [x] Logging avec Serilog
- [x] Documentation Swagger/OpenAPI
- [x] Tests unitaires avec xUnit

## 🔧 Configuration Avancée

### Utiliser SQL Server au lieu d'InMemory

1. Modifier `appsettings.json` :
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ContactManagementDb;Trusted_Connection=true;"
  }
}
```

2. Modifier `Program.cs` pour forcer SQL Server :
```csharp
// Remplacer la condition par :
builder.Services.AddDbContext<ContactManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
```

3. Créer les migrations :
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 🎉 Félicitations !

Vous avez maintenant une application de gestion de contacts complètement fonctionnelle avec :
- Interface moderne et responsive
- API RESTful documentée
- Tests unitaires
- Bonnes pratiques .NET

L'application est prête pour le développement et peut être étendue avec de nouvelles fonctionnalités !

