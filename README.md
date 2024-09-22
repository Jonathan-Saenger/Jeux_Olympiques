<h1>🏅 Projet billetterie Jeux Olympiques 2024 </h1>
<img src="wwwroot/images/image_jo.png" alt="Logo"></p>

Bonjour et bienvenue dans le projet de billetterie pour les 

Vous trouverez ci-dessous l'ensemble des étapes à suivre pour déployer localement le projet Jeux Olympiques 2024.
Je vous souhaite une excellente navigation. 

<h2> 🔗 Lien du projet </h2>

Lien du site : https://jeuxolympiques.azurewebsites.net/ <br>

Lien Github : https://github.com/Jonathan-Saenger/Jeux_Olympiques <br>

Lien Trello : https://trello.com/invite/b/5e7pGNKe/ATTI18b48db20c5f03a11134fc5f7b9ce59343D55F40/jeux-olympiques-paris-2024 <br>

<h2> 🛠 Pré-requis </h2>

- Visual Studio (recommandé) <I>ou</I> Visual Studio Code.
Les outils de développement sont téléchargeables ici : https://visualstudio.microsoft.com/fr/downloads/?cid=learn-onpage-download-cta
- .NET SDK 8.0
- GIT
- SQL Server téléchargeable sur ce lien : https://www.microsoft.com/fr-fr/sql-server/sql-server-downloa	ds (l'édition spécialisée gratuite pour Developpeur sera suffisante)
<h2>📥 Installation  </h2>

<h3> Avec Visual Studio : </h3>

1. Ouvrez Visual Studio
2. Cliquez sur <b>Cloner un référentiel</b>
3. Dans l'emplacement du référentiel, copier et coller l'URL :  

```
https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
```

Visual Studio chargera automatiquement le projet dans l'Explorateur de solutions.

<h3> Avec Visual Studio Code </h3>

1. Ouvrez un terminal et exécutez :
```
git clone https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
cd Jeux_Olympiques
code .
```

NB : <i> Dans VS Code, un message de confiance apparaîtra. Sélectionnez "Oui, je fais confiance aux auteurs". </i>

2. Installez les extensions recommandées : 
	- C# (2.45.25 ou supérieure)
	- .NET Install Tool (2.1.5 ou supérieure)
	- C# Dev Kit (1.10.18 ou supérieure)

3. Configurez la variable d'environnement PATH pour inclure le chemin vers le SDK.NET
	- Windows : Propriétés système > Variables d'environnement > Variables système > Nouvelle pour spécifier le chemin vers SDK.NET
	- macOS/Linux : Ajoutez export PATH=$PATH:/chemin/vers/dotnet à votre fichier .bashrc ou .zshrc

4. Vérifiez l'installation du SDK.NET :
```
dotnet --version
```
Si le numéro de version s'affiche, vous pouvez passser à l'étape suivante.

<h2>🗃 Configuration de la base de données </h2>

Restaurez l'outil Entity Framework Core
```
dotnet tool restore
```

Appliquez les migrations à la base de données locale  : 
```
dotnet ef database update --contexte ApplicationDbContext
```

<h2> 🚀 Lancement de l'application en local </h2>

<h3> Visual Studio </h3>

Appuyer sur  F5  <i> ou </i> cliquez sur le bouton  ▶️ (Démarrer) dans la barre d'outils

<h3> Avec Visual Studio Code </h3>

Dans le terminal intégré, exécutez :
```
dotnet run 
```