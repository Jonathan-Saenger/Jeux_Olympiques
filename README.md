<h1>Jeux Olympiques 2024 </h1>
<img src="wwwroot/images/image_jo.png" alt="Logo" height="50%"></p>
<h2> Projet billetterie Jeux Olympiques 2024 </h2>

Bonjour et bienvenue ! 

Vous trouverez ci-dessous l'ensemble des étapes à suivre pour déployer localement le projet Jeux Olympiques 2024.
Je vous souhaite une excellente navigation. 

Lien du site : https://jeuxolympiques.azurewebsites.net/ <br>

Lien Github : https://github.com/Jonathan-Saenger/Jeux_Olympiques <br>

Lien Trello : https://trello.com/invite/b/5e7pGNKe/ATTI18b48db20c5f03a11134fc5f7b9ce59343D55F40/jeux-olympiques-paris-2024 <br>

<h2> Pré-requis avant l'exécution local du projet </h2>

Installation de Visual Studio <I>ou</I> Visual Studio Code.
Les outils de développement sont téléchargeables ici : https://visualstudio.microsoft.com/fr/downloads/?cid=learn-onpage-download-cta

<h2> Récupération depuis le dépôt distant et ouverture du projet  </h2>

<h3> Avec Visual Studio : </h3>

1. Ouvrez Visual Studio
2. Dans la fenêtre de démarrage, à droite, sélectionnez <b>Cloner un référentiel</b>
3. Dans l'emplacement du référentiel, copier et coller le lien suivant 

```
https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
```

Visual Studio chargera ensuite le projet à partir du référentiel dans l'Explorateur de solutions.

<h3> Avec la ligne de commande et Visual Studio Code </h3>

Choisissez localement le répertoire dans lequel vous souhaitez déposer le projet puis, depuis votre terminal, saisissez la commande : 
```
git clone https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
```

Déplacez-vous dans le dossier en tapant dans le terminal : 
```
cd Jeux_Olympiques
```

puis, pour ouvrir projet avec Visual Studio Code :
```
code .
```

NB : <i>  Un message "Faites-vous confiance aux auteurs des fichiers de ce dossier" apparaitra, vous pourrez sélectionner
"Oui, je fais confiance aux auteurs" avant de valider. </i>

Une fois le projet ouvert, <b> vérifiez que vous possédez tous les pré-requis nécessaires </b>.

Le cas échéant, rendez-vous dans l'onglet <b> Extensions </b> puis installez les extensions suivantes ou celles manquantes : 
- C# (2.45.25)
- .NET Install Tool (2.1.5)
- C# Dev Kit (1.10.18)

Dans un deuxième temps, installez le .NET SDK en le téléchargeant sur ce lien : https://dotnet.microsoft.com/en-us/download/dotnet/8.0. Notez bien l'emplacement car il servira à configurer la variable d'environnemennt.

Une fois installé, configurez votre <b> Variable d'environnement </b> en vous rendant dans <b>Propriété système</b> -> <b> Variables d'environnement.. </b> -> dans <b> Variables systèmes </b>, cliquez sur <b> Nouvelle </b> pour spécifier le chemin vers les runtimes .NET. 
Vous trouverez plus de détail sur la configuration de C# dans Visual Studio Code au sein de la documentation officielle : https://code.visualstudio.com/docs/csharp/get-started

Assurez-vous que le SDK .NET est correctement configuré en tapant :
```
dotnet --version
```
Si le numéro de version s'affiche, vous pouvez passser à l'étape suivante.

<h2> Lancement de l'application en local </h2>

<h3> Avec Visual Studio </h3>

Appuyer sur la touche F5 <br>
ou <br>
cliquez sur la flèche de démarrage verte dans la barre d'outils Visual Studio

<h3> Avec Visual Studio Code </h3>

Utilisez la commande 
```
dotnet run 
```