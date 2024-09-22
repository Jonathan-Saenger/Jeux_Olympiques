<h1>Jeux Olympiques 2024 </h1>
<img src="wwwroot/images/image_jo.png" alt="Logo" height="50%"></p>
<h2> Projet billetterie Jeux Olympiques 2024 </h2>

Bonjour et bienvenue ! 

Vous trouverez ci-dessous l'ensemble des �tapes � suivre pour d�ployer localement le projet Jeux Olympiques 2024.
Je vous souhaite une excellente navigation. 

Lien du site : https://jeuxolympiques.azurewebsites.net/ <br>

Lien Github : https://github.com/Jonathan-Saenger/Jeux_Olympiques <br>

Lien Trello : https://trello.com/invite/b/5e7pGNKe/ATTI18b48db20c5f03a11134fc5f7b9ce59343D55F40/jeux-olympiques-paris-2024 <br>

<h2> Pr�-requis avant l'ex�cution local du projet </h2>

Installation de Visual Studio <I>ou</I> Visual Studio Code.
Les outils de d�veloppement sont t�l�chargeables ici : https://visualstudio.microsoft.com/fr/downloads/?cid=learn-onpage-download-cta

<h2> R�cup�ration depuis le d�p�t distant et ouverture du projet  </h2>

<h3> Avec Visual Studio : </h3>

1. Ouvrez Visual Studio
2. Dans la fen�tre de d�marrage, � droite, s�lectionnez <b>Cloner un r�f�rentiel</b>
3. Dans l'emplacement du r�f�rentiel, copier et coller le lien suivant 

```
https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
```

Visual Studio chargera ensuite le projet � partir du r�f�rentiel dans l'Explorateur de solutions.

<h3> Avec la ligne de commande et Visual Studio Code </h3>

Choisissez localement le r�pertoire dans lequel vous souhaitez d�poser le projet puis, depuis votre terminal, saisissez la commande : 
```
git clone https://github.com/Jonathan-Saenger/Jeux_Olympiques.git
```

D�placez-vous dans le dossier en tapant dans le terminal : 
```
cd Jeux_Olympiques
```

puis, pour ouvrir projet avec Visual Studio Code :
```
code .
```

NB : <i>  Un message "Faites-vous confiance aux auteurs des fichiers de ce dossier" apparaitra, vous pourrez s�lectionner
"Oui, je fais confiance aux auteurs" avant de valider. </i>

Une fois le projet ouvert, <b> v�rifiez que vous poss�dez tous les pr�-requis n�cessaires </b>.

Le cas �ch�ant, rendez-vous dans l'onglet <b> Extensions </b> puis installez les extensions suivantes ou celles manquantes : 
- C# (2.45.25)
- .NET Install Tool (2.1.5)
- C# Dev Kit (1.10.18)

Dans un deuxi�me temps, installez le .NET SDK en le t�l�chargeant sur ce lien : https://dotnet.microsoft.com/en-us/download/dotnet/8.0. Notez bien l'emplacement car il servira � configurer la variable d'environnemennt.

Une fois install�, configurez votre <b> Variable d'environnement </b> en vous rendant dans <b>Propri�t� syst�me</b> -> <b> Variables d'environnement.. </b> -> dans <b> Variables syst�mes </b>, cliquez sur <b> Nouvelle </b> pour sp�cifier le chemin vers les runtimes .NET. 
Vous trouverez plus de d�tail sur la configuration de C# dans Visual Studio Code au sein de la documentation officielle : https://code.visualstudio.com/docs/csharp/get-started

Assurez-vous que le SDK .NET est correctement configur� en tapant :
```
dotnet --version
```
Si le num�ro de version s'affiche, vous pouvez passser � l'�tape suivante.

<h2> Lancement de l'application en local </h2>

<h3> Avec Visual Studio </h3>

Appuyer sur la touche F5 <br>
ou <br>
cliquez sur la fl�che de d�marrage verte dans la barre d'outils Visual Studio

<h3> Avec Visual Studio Code </h3>

Utilisez la commande 
```
dotnet run 
```