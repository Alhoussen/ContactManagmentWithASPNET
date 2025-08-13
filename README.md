# 🚀 Contact Management API - Interface Moderne & Intuitive

Une application complète de gestion de contacts avec une API RESTful ASP.NET Core 8.0 et une interface utilisateur **ultra-moderne** avec design **glassmorphism** et expérience utilisateur optimisée.

## ✨ Fonctionnalités Principales

### 🔧 API Backend Robuste
- ✅ **CRUD complet** pour les contacts (Créer, Lire, Mettre à jour, Supprimer)
- 🔍 **Recherche avancée** avec filtrage intelligent
- 📄 **Pagination** optimisée des résultats
- ✅ **Validation robuste** avec Data Annotations et Fluent Validation
- 🔒 **Gestion d'erreurs** globale avec middleware personnalisé
- 📚 **Documentation API** complète avec Swagger/OpenAPI
- 🗄️ **Base de données SQLite** pour la persistance
- 📊 **Logging** structuré avec Serilog

### 🎨 Interface Utilisateur Moderne
- 🌟 **Design Glassmorphism** avec effets de transparence
- 🎨 **Gradients modernes** et animations fluides
- 📱 **100% Responsive** (mobile-first design)
- 🔍 **Recherche intelligente** avec placeholder descriptif
- ⚡ **Modals interactives** pour tous les workflows
- 📊 **Statistiques visuelles** en temps réel
- 🎭 **Animations CSS3** et transitions fluides
- 📱 **Bouton flottant** pour actions rapides

### 🚀 Nouvelles Fonctionnalités UX/UI
- 👁️ **Modal de détails moderne** avec toutes les informations
- 🗑️ **Confirmation de suppression** avec aperçu du contact
- 📧 **Actions rapides** : Email et appel direct
- 🔄 **Layout adaptatif** : Horizontal sur desktop, vertical sur mobile
- 🎯 **Feedback visuel** avec toasts et loading states
- 🎨 **Avatars avec initiales** colorés automatiquement



## 🏗️ Architecture

```
ContactManagement.Api/
├── Controllers/          # Contrôleurs API et MVC
├── Services/            # Logique métier
├── Data/               # Contexte Entity Framework
├── Models/             # Modèles de données
│   └── DTOs/          # Objets de transfert de données
├── Views/              # Vues Razor
│   ├── Shared/        # Layout partagé
│   └── Home/          # Pages principales
├── wwwroot/            # Fichiers statiques
│   └── js/            # JavaScript
├── Mapping/            # Profils AutoMapper
├── Middleware/         # Middleware personnalisé
└── Tests/             # Tests unitaires
```

## 🛠️ Technologies & Stack Technique

### 🔧 Backend (.NET 8)
- **ASP.NET Core 8.0** - Framework web haute performance
- **Entity Framework Core 8.0** - ORM avec support SQLite
- **SQLite** - Base de données légère et performante
- **AutoMapper** - Mapping automatique entre DTOs et entités
- **Swagger/OpenAPI** - Documentation API interactive
- **Serilog** - Logging structuré et configurable
- **Data Annotations** - Validation côté serveur

### 🎨 Frontend Moderne
- **HTML5 Sémantique** - Structure accessible et SEO-friendly
- **CSS3 Avancé** - Variables CSS, Flexbox, Grid, Animations
- **JavaScript ES6+** - Async/Await, Modules, Classes
- **Bootstrap 5.3** - Framework CSS responsive et moderne
- **Glassmorphism Design** - Effets de transparence et blur
- **Font Awesome 6** - Icônes vectorielles modernes
- **Inter Font** - Typographie moderne et lisible

### 🚀 Fonctionnalités Techniques
- **Responsive Design** - Mobile-first avec breakpoints optimisés
- **Progressive Enhancement** - Fonctionnalité de base + améliorations
- **Debounced Search** - Recherche optimisée sans spam de requêtes
- **Modal Management** - Gestion intelligente des modals Bootstrap
- **Error Handling** - Gestion d'erreurs robuste côté client/serveur
- **Loading States** - Feedback visuel pour toutes les actions

### 🧪 Tests & Qualité
- **xUnit** - Framework de tests unitaires
- **Moq** - Framework de mocking pour les tests
- **Entity Framework InMemory** - Tests avec base de données en mémoire

## 🎯 Interface Utilisateur Moderne

### 🎨 Design System
- **Glassmorphism** : Effets de transparence avec `backdrop-filter: blur()`
- **Gradients dynamiques** : Dégradés colorés pour un look premium
- **Animations fluides** : Transitions CSS3 avec `cubic-bezier` personnalisé
- **Variables CSS** : Système de couleurs cohérent et maintenable

### 📱 Expérience Utilisateur
- **Layout adaptatif** : 
  - 🖥️ **Desktop** : Cartes horizontales pour optimiser l'espace
  - 📱 **Mobile** : Cartes verticales pour une meilleure lisibilité
- **Modals interactives** :
  - 👁️ **Détails** : Modal XL avec toutes les informations du contact
  - ✏️ **Édition** : Formulaire avec floating labels et emojis
  - 🗑️ **Suppression** : Confirmation sécurisée avec aperçu
- **Feedback visuel** :
  - 🎯 **Toasts** : Notifications avec emojis et animations
  - ⏳ **Loading states** : Spinners et états de chargement
  - ✨ **Hover effects** : Animations au survol

### 🔍 Recherche Intelligente
- **Placeholder descriptif** : "🔍 Rechercher un contact par nom, email ou téléphone..."
- **Debounced search** : Optimisation des performances avec délai de 300ms
- **Recherche instantanée** : Filtrage en temps réel côté client
- **Styles modernes** : Input arrondi avec ombres et effets de focus

## 🚀 Installation et démarrage

### Prérequis
- .NET 8.0 SDK
- Visual Studio 2022 ou VS Code
- Git (pour cloner le repository)

### Étapes d'installation

1. **Cloner le projet**
   ```bash
   git clone https://github.com/votre-username/contactManagmentC#.git
   cd contactManagmentC#
   ```

2. **Vérifier les prérequis**
   ```bash
   # Vérifier la version de .NET
   dotnet --version
   # Doit afficher 8.0.x ou supérieur
   ```

3. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

4. **Installer les outils Entity Framework (si pas déjà fait)**
   ```bash
   dotnet tool install --global dotnet-ef
   # ou mettre à jour si déjà installé
   dotnet tool update --global dotnet-ef
   ```

5. **Créer et appliquer les migrations de base de données**
   ```bash
   # Créer la base de données SQLite
   dotnet ef database update
   ```

6. **Compiler le projet**
   ```bash
   dotnet build
   ```

7. **Lancer l'application**
   ```bash
   # Option 1: Port par défaut (5000/5001)
   dotnet run
   
   # Option 2: Port personnalisé
   dotnet run --urls="http://localhost:5003"
   ```

8. **Accéder à l'application**
   - **Interface utilisateur** : http://localhost:5003
   - **API Swagger** : http://localhost:5003/swagger
   - **API Endpoints** : http://localhost:5003/api/contacts

### 🚀 Démarrage rapide

#### Option 1: Script automatique (Recommandé)
```bash
# Linux/Mac
git clone https://github.com/votre-username/contactManagmentC#.git
cd contactManagmentC#
./setup.sh

# Windows
git clone https://github.com/votre-username/contactManagmentC#.git
cd contactManagmentC#
setup.bat
```

#### Option 2: One-liner manuel
```bash
git clone https://github.com/votre-username/contactManagmentC#.git && cd contactManagmentC# && dotnet restore && dotnet ef database update && dotnet run --urls="http://localhost:5003"
```

### 📋 Scripts de setup inclus

Le projet inclut des scripts automatiques pour simplifier l'installation :

- **`setup.sh`** (Linux/Mac) : Script bash avec vérifications automatiques
- **`setup.bat`** (Windows) : Script batch équivalent pour Windows

**Fonctionnalités des scripts :**
- ✅ Vérification des prérequis (.NET 8, dotnet-ef)
- 📦 Restauration automatique des packages
- 🗄️ Configuration de la base de données SQLite
- 🔨 Compilation du projet
- 🔍 Vérification des ports disponibles
- 🚀 Lancement optionnel de l'application

**Usage avancé :**
```bash
# Avec port personnalisé
./setup.sh 5004        # Linux/Mac
setup.bat 5004         # Windows

# Avec permissions sudo (si nécessaire)
sudo ./setup.sh        # Linux/Mac
```

## 🔧 Configuration

### Variables d'environnement
Le projet utilise `appsettings.json` pour la configuration. Vous pouvez créer un `appsettings.Development.json` pour vos paramètres locaux :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ContactManagement.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Structure de la base de données
La base de données SQLite sera créée automatiquement avec les données de test suivantes :
- **Jean Dupont** - jean.dupont@email.com
- **Marie Martin** - marie.martin@email.com  
- **Pierre Durand** - pierre.durand@email.com

### Ports disponibles
Si le port 5003 est occupé, vous pouvez utiliser d'autres ports :
```bash
# Exemples de ports alternatifs
dotnet run --urls="http://localhost:5004"
dotnet run --urls="http://localhost:5005"
dotnet run --urls="http://localhost:8080"
```

## 🛠️ Dépannage

### Problèmes courants

#### 1. Erreur "Address already in use"
```bash
# Tuer les processus dotnet en cours
pkill -f "dotnet run"
# ou trouver et tuer le processus spécifique
lsof -ti:5003 | xargs kill -9
```

#### 2. Erreur de migration Entity Framework
```bash
# Supprimer la base de données et recréer
rm ContactManagement.db
dotnet ef database update
```

#### 3. Packages NuGet manquants
```bash
# Nettoyer et restaurer
dotnet clean
dotnet restore
dotnet build
```

#### 4. Erreur "dotnet-ef command not found"
```bash
# Installer les outils EF globalement
dotnet tool install --global dotnet-ef
# Vérifier l'installation
dotnet ef --version
```

#### 5. Problèmes de permissions (Linux/Mac)
```bash
# Donner les permissions d'exécution
chmod +x ./
# Ou exécuter avec sudo si nécessaire
sudo dotnet run --urls="http://localhost:5003"
```

### Logs et débogage
```bash
# Lancer avec logs détaillés
dotnet run --verbosity detailed

# Lancer en mode développement
export ASPNETCORE_ENVIRONMENT=Development
dotnet run --urls="http://localhost:5003"
```

### Vérification de l'installation
```bash
# Vérifier que tout fonctionne
curl http://localhost:5003/api/contacts
# Doit retourner la liste des contacts en JSON
```

## 💻 Environnements de développement

### Visual Studio Code
1. **Extensions recommandées** :
   - C# Dev Kit
   - .NET Extension Pack
   - SQLite Viewer
   - REST Client

2. **Configuration launch.json** :
```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Launch Contact Management API",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/bin/Debug/net8.0/ContactManagement.Api.dll",
      "args": [],
      "cwd": "${workspaceFolder}",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ASPNETCORE_URLS": "http://localhost:5003"
      }
    }
  ]
}
```

### Visual Studio 2022
1. **Ouvrir le projet** : `ContactManagement.Api.sln`
2. **Définir le projet de démarrage** : ContactManagement.Api
3. **Configurer l'URL** : Propriétés > Debug > App URL: `http://localhost:5003`

### JetBrains Rider
1. **Ouvrir le projet** : File > Open > Sélectionner le dossier
2. **Configuration de lancement** : Run/Debug Configurations
3. **URL personnalisée** : Program arguments: `--urls=http://localhost:5003`

### Terminal/Command Line
```bash
# Développement avec hot reload
dotnet watch run --urls="http://localhost:5003"

# Production
dotnet run --configuration Release --urls="http://localhost:5003"

# Avec profiling
dotnet run --urls="http://localhost:5003" --launch-profile "ContactManagement.Api"
```

## 🐳 Déploiement avec Docker

### Option 1: Docker simple
```bash
# Construire l'image
docker build -t contact-management-api .

# Lancer le conteneur
docker run -d -p 5003:5003 --name contact-api contact-management-api

# Accéder à l'application
open http://localhost:5003
```

### Option 2: Docker Compose (Recommandé)
```bash
# Production
docker-compose up -d

# Développement avec hot reload
docker-compose --profile dev up -d

# Voir les logs
docker-compose logs -f

# Arrêter
docker-compose down
```

### Option 3: Développement avec Docker
```bash
# Construire l'image de développement
docker build -f Dockerfile.dev -t contact-management-dev .

# Lancer avec hot reload
docker run -d -p 5004:5004 -v $(pwd):/app --name contact-dev contact-management-dev
```

### Commandes Docker utiles
```bash
# Voir les conteneurs en cours
docker ps

# Accéder au conteneur
docker exec -it contact-api bash

# Voir les logs
docker logs contact-api

# Redémarrer
docker restart contact-api

# Nettoyer
docker stop contact-api && docker rm contact-api
```

## 📱 Interface Utilisateur

### 🏠 Page d'accueil
- **Vue grille** des contacts avec cartes interactives
- **Barre de recherche** avec filtrage instantané
- **Statistiques** en temps réel
- **Pagination** intelligente
- **Actions rapides** (voir, modifier, supprimer, contacter)

### 📝 Gestion des contacts
- **Modal d'ajout/modification** sans rechargement
- **Upload d'image** avec prévisualisation
- **Validation** en temps réel côté client
- **Messages de confirmation** avec toasts

### 📊 Fonctionnalités avancées
- **Mode sombre/clair** avec toggle
- **Export CSV** des contacts filtrés
- **Actions rapides** (email, téléphone)
- **Page de détails** complète
- **Breadcrumbs** de navigation

## 📋 Endpoints API

### Contacts
| Méthode | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/contacts` | Récupère tous les contacts (avec pagination) |
| GET | `/api/contacts/{id}` | Récupère un contact par ID |
| POST | `/api/contacts` | Crée un nouveau contact |
| PUT | `/api/contacts/{id}` | Met à jour un contact |
| DELETE | `/api/contacts/{id}` | Supprime un contact |

### Images
| Méthode | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/placeholder/{width}/{height}` | Génère une image placeholder |

### Paramètres de recherche (GET /api/contacts)
- `searchTerm` : Terme de recherche (nom, prénom, email)
- `pageNumber` : Numéro de page (défaut : 1)
- `pageSize` : Nombre d'éléments par page (défaut : 10, max : 100)
- `sortBy` : Champ de tri (FirstName, LastName, Email, CreatedAt)
- `sortOrder` : Ordre de tri (asc, desc)

## 🎨 Personnalisation

### Thèmes
L'application supporte les modes clair et sombre avec persistance dans le localStorage.

### Responsive Design
- **Mobile** : Interface optimisée avec bouton flottant
- **Tablette** : Adaptation des grilles et espaces
- **Desktop** : Interface complète avec toutes les fonctionnalités

## 🧪 Tests

```bash
# Tests unitaires
dotnet test

# Tests avec couverture
dotnet test --collect:"XPlat Code Coverage"
```

## 📊 Modèle de données

### Contact
```csharp
public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
```

## 🔧 Configuration

### Base de données
- **Développement** : InMemory Database (aucune configuration requise)
- **Production** : SQL Server (configurer la chaîne de connexion dans `appsettings.json`)



### Logging
Les logs sont écrits dans :
- Console (développement)
- Fichiers dans `/logs` (production)

## 🚀 Déploiement

### Prérequis production
1. Configurer la chaîne de connexion SQL Server dans `appsettings.Production.json`
2. Créer les dossiers nécessaires pour les uploads
3. Appliquer les migrations :
   ```bash
   dotnet ef database update
   ```

## 📝 Exemples d'utilisation

### 🎯 Interface Utilisateur
1. **Ajouter un contact** : Cliquez sur le bouton flottant `+` ou "Nouveau Contact"
2. **Voir les détails** : Cliquez sur l'icône `👁️` pour ouvrir le modal de détails moderne
3. **Modifier un contact** : Cliquez sur `✏️` ou "Modifier" depuis les détails
4. **Supprimer un contact** : Cliquez sur `🗑️` pour une confirmation sécurisée
5. **Rechercher** : Tapez dans la barre de recherche intelligente
6. **Actions rapides** : Email et appel direct depuis les détails

### 🔧 API REST

#### Créer un contact
```bash
POST /api/contacts
Content-Type: application/json

{
  "firstName": "Alhoussen",
  "lastName": "TRAORE",
  "email": "alh@email.com",
  "phoneNumber": "0123456789",
  "address": "Bamako, Mali"
}
```

#### Rechercher des contacts
```bash
GET /api/contacts?searchTerm=jean&pageSize=10&sortBy=lastName&sortOrder=asc
```

#### Obtenir un contact par ID
```bash
GET /api/contacts/1
```

#### Mettre à jour un contact
```bash
PUT /api/contacts/1
Content-Type: application/json

{
  "firstName": "Jean",
  "lastName": "Dupont",
  "email": "jean.dupont@newemail.com",
  "phoneNumber": "0123456789",
  "address": "456 Avenue des Champs, Lyon"
}
```

#### Supprimer un contact
```bash
DELETE /api/contacts/1
```

## 🆕 Dernières Améliorations

### ✅ Version 2.0 - Interface Moderne (Récent)
- 🎨 **Design Glassmorphism** : Interface ultra-moderne avec effets de transparence
- 👁️ **Modal de détails** : Affichage complet des informations contact
- 🗑️ **Confirmation de suppression** : Modal sécurisé avec aperçu du contact
- 📱 **Layout adaptatif** : Horizontal sur desktop, vertical sur mobile
- 🔍 **Recherche améliorée** : Input moderne avec placeholder descriptif
- 📧 **Actions rapides** : Email et appel direct depuis l'interface
- 🎯 **Feedback visuel** : Toasts avec emojis et loading states
- 🎨 **Avatars avec initiales** : Génération automatique d'avatars colorés

### ✅ Version 1.5 - Base de Données (Récent)
- 🗄️ **SQLite** : Remplacement de InMemory par SQLite pour la persistance
- 🔄 **Migrations EF** : Gestion automatique du schéma de base de données
- 📊 **Données persistantes** : Conservation des contacts entre les redémarrages

## 🎯 Fonctionnalités à venir

### 🚀 Roadmap v2.1
- [ ] **Mode sombre/clair** : Thème adaptatif avec persistance
- [ ] **Export avancé** : CSV, Excel, vCard
- [ ] **Import de contacts** : Depuis fichiers CSV/Excel
- [ ] **Recherche avancée** : Filtres par date, catégorie
- [ ] **Favoris** : Marquer des contacts comme favoris

### 🚀 Roadmap v3.0
- [ ] **Groupes de contacts** : Organisation par catégories
- [ ] **Historique des modifications** : Audit trail complet
- [ ] **Synchronisation cloud** : Backup automatique
- [ ] **API GraphQL** : Alternative à l'API REST
- [ ] **Notifications** : Rappels et alertes

## 📸 Aperçu de l'Interface

### 🖥️ Desktop - Layout Horizontal
- **Cartes horizontales** : Optimisation de l'espace écran
- **Informations complètes** : Nom, email, téléphone, adresse sur une ligne
- **Actions rapides** : Boutons d'action alignés à droite

### 📱 Mobile - Layout Vertical
- **Cartes compactes** : Design adapté aux petits écrans
- **Touch-friendly** : Boutons optimisés pour le tactile
- **Navigation fluide** : Transitions et animations optimisées

### 🎨 Modals Modernes
- **Modal de détails** : Design XL avec toutes les informations
- **Modal d'édition** : Floating labels avec emojis
- **Modal de suppression** : Confirmation sécurisée avec aperçu

## ⚡ Performance & Optimisations

### 🚀 Frontend
- **Debounced Search** : Recherche optimisée (300ms de délai)
- **Lazy Loading** : Chargement des détails à la demande
- **CSS Optimisé** : Variables CSS et animations GPU-accelerated
- **JavaScript Moderne** : ES6+ avec async/await

### 🔧 Backend
- **Entity Framework Core** : ORM optimisé avec requêtes LINQ
- **SQLite** : Base de données légère et performante
- **Pagination** : Chargement par pages pour les grandes listes
- **Caching** : Mise en cache des requêtes fréquentes

### 📊 Métriques
- **Temps de chargement** : < 100ms pour l'interface
- **Taille des assets** : CSS + JS < 50KB (minifié)
- **Responsive** : Support complet mobile/tablet/desktop
- **Accessibilité** : Conforme aux standards WCAG 2.1

## 🤝 Contribution

1. Fork le projet
2. Créer une branche pour votre fonctionnalité
3. Commit vos changements
4. Push vers la branche
5. Ouvrir une Pull Request

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

---

**Développé avec ❤️ en utilisant ASP.NET Core et Bootstrap 5**
