﻿<h1 align="center">🏅 Projet billetterie Jeux Olympiques 2024 </h1>

<img src="wwwroot/images/readme.png" alt="Logo"></p>


Bonjour et bienvenue dans le projet de billetterie pour les Jeux Olympiques 2024 ! Ce guide vous fournira toutes les 
étapes nécessaires pour déployer et tester l'application localement.

Dans le dossier ***Annexes*** à la racine du projet, vous trouverez : 
* La documentation technique 
* La manuel d'utilisation
* Les rapports des tests unitaires

<h2> 🛠 Pré-requis </h2>

Avant de commencer, assurez-vous d'avoir installé les éléments suivants :

- [ ] **Visual Studio** (2022 ou supérieur) ou **Visual Studio Code** [Télécharger Visual Studio](https://visualstudio.microsoft.com/fr/downloads/?cid=learn-onpage-download-cta)
- [ ] **.NET SDK 8.0+** [Télécharger le SDK .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [ ] **Git** [Télécharger Git](https://git-scm.com/downloads)
- [ ] **SQL Server** (édition Développeur recommandée) [Télécharger SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads)

<h2>📥 Installation  </h2>

<h3> Avec Visual Studio : </h3>

1. Ouvrez Visual Studio
2. Cliquez sur <b>Cloner un référentiel</b>
3. Dans l'emplacement du référentiel, copier et coller l'URL :  

```
https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
```

Visual Studio chargera automatiquement le projet dans l'Explorateur de solutions.

<h3> Avec Visual Studio Code : </h3>

1. Clonez le dépôt et ouvrez-le dans Visual Studio Code :
    ```bash
    git clone https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
    cd Jeux_Olympiques
    code .
    ```

NB : <i> Dans VS Code, un message de confiance apparaîtra. Sélectionnez "Oui, je fais confiance aux auteurs". </i>

2. Installez les extensions recommandées dans VS Code (cliquez sur les liens pour les installer directement ou rendez-vous dans l'onglet Extensions de votre éditeur de code) :
   - [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) (2.45.25 ou supérieure)
   - [NET Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime) (2.1.5 ou supérieure)
   - [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) (1.10.18 ou supérieure)

3. Configurez la variable d'environnement PATH pour inclure le chemin vers le SDK .NET :
   - **Windows** : 
     - Allez dans `Panneau de configuration > Système et sécurité > Système > Paramètres avancés du système`
     - Cliquez sur **Variables d'environnement** et ajoutez le chemin du SDK .NET à la variable `PATH`
   - **macOS/Linux** : 
     - Ouvrez votre fichier `.bashrc` ou `.zshrc`, puis ajoutez la ligne suivante :
     ```bash
     export PATH=$PATH:/chemin/vers/dotnet
     ```

4. Vérifiez l'installation du SDK.NET :
```
dotnet --version
```
Si le numéro de version s'affiche, vous pouvez passser à l'étape suivante.

<h2>🗃 Configuration de la base de données </h2>

1. Vérifiez que votre serveur SQL est démarré.

2. Dans le fichier `appsettings.json`, configurez la chaîne de connexion à la base de données :

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
- NB : Si votre configuration SQL est différente, assurez-vous de mettre à jour la chaîne de connexion. Il est impératif de conserver `TrustServerCertificate=True;` pour éviter des erreurs SSL.


Restaurez les outils d'Entity Framework Core :
```
dotnet tool restore
```

Appliquez les migrations pour configurer la base de données : 
```
dotnet ef database update --context ApplicationDbContext
```

<h2> 🚀 Lancement de l'application en local </h2>

<h3> Visual Studio </h3>

Appuyer sur  F5  <i> ou </i> cliquez sur le bouton  ▶️ (Démarrer) dans la barre d'outils

<h3> Avec Visual Studio Code </h3>

Dans le terminal intégré, exécutez :
```
dotnet run 
```

<h2> 🔐  Se connecter à un compte sur l'application </h2>

<p> Un compte Admin et un compte Utilisateur ont été spécialement créé afin de pouvoir tester
l'application. Les données figurent dans un fichier de migration, ils seront donc automatiquement inséré
dans votre base de données lors de la mise à jour de cette dernière. </p>

 - **Admin Login** : 
     - Email : `admin@jeuxolympiques.com`
     - Password : `Admin2024olympiques?` (hashé dans la base de données)
 - **User Login** : 
     - Email : `user@jeuxolympiques.com`
     - Password : `User2024olympiques?` (hashé dans la base de données)

Pour le test, vous pouvez créer votre propre compte Utilisateur. Conformément à la demande du client, il n'est pas possible 
de créer un compte Admin. 



Excellente navigation dans l'application !