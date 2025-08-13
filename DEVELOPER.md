# 👨‍💻 Guide du Développeur - Contact Management API

## 🚀 Démarrage Rapide pour Développeurs

### Méthode 1: Script Automatique (Recommandé)
```bash
# Linux/Mac
./setup.sh

# Windows
setup.bat
```

### Méthode 2: Manuel
```bash
git clone <repo-url>
cd contactManagmentC#
dotnet restore
dotnet ef database update
dotnet run --urls="http://localhost:5003"
```

### Méthode 3: Docker
```bash
docker-compose up -d
```

## 🛠️ Environnement de Développement

### Prérequis
- **.NET 8.0 SDK** ou supérieur
- **Visual Studio 2022** / **VS Code** / **JetBrains Rider**
- **Git**
- **Docker** (optionnel)

### Extensions VS Code Recommandées
```json
{
  "recommendations": [
    "ms-dotnettools.csharp",
    "ms-dotnettools.csdevkit",
    "alexcvzz.vscode-sqlite",
    "humao.rest-client",
    "ms-vscode.vscode-json",
    "bradlc.vscode-tailwindcss"
  ]
}
```

## 📁 Structure du Projet

```
ContactManagement.Api/
├── 📁 Controllers/          # Contrôleurs API et MVC
│   ├── ContactsController.cs
│   └── HomeController.cs
├── 📁 Services/            # Logique métier
│   ├── IContactService.cs
│   └── ContactService.cs
├── 📁 Data/               # Contexte Entity Framework
│   └── ContactManagementDbContext.cs
├── 📁 Models/             # Modèles de données
│   ├── Contact.cs
│   └── DTOs/             # Objets de transfert
├── 📁 Views/              # Vues Razor
│   ├── Shared/
│   └── Home/
├── 📁 wwwroot/            # Fichiers statiques
│   ├── js/
│   └── css/
├── 📁 Mapping/            # Profils AutoMapper
├── 📁 Middleware/         # Middleware personnalisé
├── 📁 Migrations/         # Migrations EF Core
└── 📄 Program.cs          # Point d'entrée
```

## 🔧 Configuration

### appsettings.json
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

### Variables d'Environnement
```bash
# Développement
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=http://localhost:5003

# Production
export ASPNETCORE_ENVIRONMENT=Production
```

## 🗄️ Base de Données

### Migrations Entity Framework
```bash
# Créer une nouvelle migration
dotnet ef migrations add NomDeLaMigration

# Appliquer les migrations
dotnet ef database update

# Supprimer la dernière migration
dotnet ef migrations remove

# Voir l'historique
dotnet ef migrations list

# Générer un script SQL
dotnet ef migrations script
```

### Données de Test
Le projet inclut des données de seed automatiques :
- Jean Dupont (jean.dupont@email.com)
- Marie Martin (marie.martin@email.com)
- Pierre Durand (pierre.durand@email.com)

## 🧪 Tests et Développement

### Commandes Utiles
```bash
# Hot reload en développement
dotnet watch run --urls="http://localhost:5003"

# Build en mode Release
dotnet build --configuration Release

# Publier l'application
dotnet publish --configuration Release --output ./publish

# Nettoyer les artifacts
dotnet clean
```

### Tests API avec curl
```bash
# Lister les contacts
curl http://localhost:5003/api/contacts

# Créer un contact
curl -X POST http://localhost:5003/api/contacts \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Test",
    "lastName": "User",
    "email": "test@example.com",
    "phoneNumber": "0123456789",
    "address": "Test Address"
  }'

# Obtenir un contact par ID
curl http://localhost:5003/api/contacts/1

# Mettre à jour un contact
curl -X PUT http://localhost:5003/api/contacts/1 \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Updated",
    "lastName": "User",
    "email": "updated@example.com",
    "phoneNumber": "0123456789",
    "address": "Updated Address"
  }'

# Supprimer un contact
curl -X DELETE http://localhost:5003/api/contacts/1
```

## 🐳 Développement avec Docker

### Build et Run
```bash
# Build de l'image
docker build -t contact-management-api .

# Run en mode développement
docker-compose --profile dev up -d

# Logs en temps réel
docker-compose logs -f
```

### Debug dans le Conteneur
```bash
# Accéder au conteneur
docker exec -it contact-management-dev bash

# Voir les processus
docker exec -it contact-management-dev ps aux

# Voir les variables d'environnement
docker exec -it contact-management-dev env
```

## 🔍 Debugging et Profiling

### Logs
Les logs sont configurés avec Serilog et affichent :
- Requêtes HTTP entrantes
- Requêtes SQL Entity Framework
- Erreurs et exceptions
- Métriques de performance

### Points de Breakpoint Recommandés
- `ContactsController` : Actions CRUD
- `ContactService` : Logique métier
- `GlobalExceptionMiddleware` : Gestion d'erreurs

## 📝 Conventions de Code

### Naming
- **PascalCase** : Classes, méthodes, propriétés publiques
- **camelCase** : Variables locales, paramètres
- **UPPERCASE** : Constantes
- **I + PascalCase** : Interfaces (ex: `IContactService`)

### Structure des Méthodes
```csharp
public async Task<ActionResult<ContactDto>> CreateContactAsync(
    CreateContactDto createDto, 
    CancellationToken cancellationToken = default)
{
    // Validation
    if (await IsEmailExistsAsync(createDto.Email, cancellationToken: cancellationToken))
    {
        return BadRequest("Email already exists");
    }

    // Logique métier
    var contact = _mapper.Map<Contact>(createDto);
    contact.CreatedAt = DateTime.UtcNow;
    contact.UpdatedAt = DateTime.UtcNow;

    // Persistance
    _context.Contacts.Add(contact);
    await _context.SaveChangesAsync(cancellationToken);

    // Retour
    var contactDto = _mapper.Map<ContactDto>(contact);
    return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contactDto);
}
```

## 🚀 Déploiement

### Environnements
- **Development** : `dotnet run`
- **Staging** : `docker-compose up`
- **Production** : `docker build` + orchestrateur

### Checklist de Déploiement
- [ ] Tests passent
- [ ] Migrations appliquées
- [ ] Variables d'environnement configurées
- [ ] Logs configurés
- [ ] Monitoring en place
- [ ] Backup de la base de données

## 🤝 Contribution

### Workflow Git
```bash
# Créer une branche feature
git checkout -b feature/nouvelle-fonctionnalite

# Développer et commiter
git add .
git commit -m "feat: ajouter nouvelle fonctionnalité"

# Pousser et créer une PR
git push origin feature/nouvelle-fonctionnalite
```

### Format des Commits
- `feat:` nouvelle fonctionnalité
- `fix:` correction de bug
- `docs:` documentation
- `style:` formatage
- `refactor:` refactoring
- `test:` tests
- `chore:` maintenance

## 📞 Support

### Problèmes Courants
1. **Port occupé** : `pkill -f "dotnet run"`
2. **Migration échouée** : `rm *.db && dotnet ef database update`
3. **Packages manquants** : `dotnet restore`

### Ressources
- [Documentation ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [AutoMapper](https://automapper.org)
- [Swagger/OpenAPI](https://swagger.io)
