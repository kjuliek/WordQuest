# WordQuest
Explorez, apprenez, dominez avec WordQuest : votre aventure vers l'anglais parfait !

*Explore, learn, dominate with WordQuest: your journey to perfect English!*

<!-- Sommaire -->
## Sommaire
-   [Installations Nécessaires](#installations-nécessaires)
-   [Préparation du Projet ASP.NET Core 8](#création-du-projet-aspnet-core-8-pour-lapi)
-   [Installations des Machines Virtuelles](#installations-des-machines-virtuelles)
-   [Configuration des IP dans l'API](#configuration-des-ip-dans-lapi)
-   [Lancer du projet](#lancer-le-projet)

<!-- /Sommaire -->

## Installations Nécessaires
 - Visual Studio Code

 ### Pour un lancement du projet en local :
 - XAMPP

Dans Visual Studio Code on installe les packages : Live Server et MySQL

<img src="Documentation/images/LiveServer.png" alt="Description" style="width: 45%; height: auto;">
<img src="Documentation/images/MySQL.png" alt="Description" style="width: 40%; height: auto;">

## Lancement du Projet ASP.NET Core 8 pour l'API
### Télécharger le dossier WordQuest :

<img src="Documentation/images/github.png" alt="Description" style="width: 60%; height: auto;">

### Ouvrir le dossier WordQuest dans VS Code :

<img src="Documentation/images/VSCodeFolder.png" alt="Description" style="width: 60%; height: auto;">

### Afin de reconstruire les fichiers générés par le compilateur et les outils de build, dans le terminal lancer les commandes suivantes :

    - cd .\WordQuestAPI\
    - dotnet restore
    - dotnet build

<img src="Documentation/images/commands.png" alt="Description" style="width: 70%; height: auto;">

### Les dossiers bin et obj sont maintenant visibles dans WordQuestAPI :
<img src="Documentation/images/obj_bin.png" alt="Description" style="width: 15%; height: auto;">

## Lancement en local

Cette partie n'est utile que si vous souhaitez lancer le projet en local, sinon allez directement à la partie : [Installations des Machines Virtuelles](#installations-des-machines-virtuelles)

### Lancer un serveur MySQL sur XAMPP 
<img src="Documentation/images/xampp.png" alt="Description" style="width: 40%; height: auto;">

### Initialiser la base de donnée MySQL
avec les paramètres : 
- base de donnée : "wordquest"
- port : "3306"
- utilisateur : "root"
- mot de passe : ""

<img src="Documentation/images/database.png" alt="Description" style="width: 40%; height: auto;">

### Lancer les commandes suivantes dans le terminal
    - cd .\WordQuestAPI\ (pas nécessaire si vous êtes déjà dans le fichier)
    - dotnet ef database update

### Ouvrir le fichier /WordQuestFront/index.html et cliquer sur le bouton GoLive en bas de l'écran
<img src="Documentation/images/golive.png" alt="Description" style="width: 100%; height: auto;">

### On a donc le port du serveur Front qui s'affiche ici c'est "http:/127.0.0.1:5501"
<img src="Documentation/images/port.png" alt="Description" style="width: 40%; height: auto;">


## Installations des Machines Virtuelles
Cette partie n'est utile pas utile si vous souhaitez lancer le projet en local, si c'est le cas allez directement à la partie : [Configuration des IP dans l'API](#configuration-des-ip-dans-lapi)

Mettre le doc Machine virtuel ici

## Configuration des IP dans l'API
### Dans le fichier /WodQuestAPI/Models/Startup.cs modifier l'adresse par l'adresse du serveur Front
<img src="Documentation/images/CORS.png" alt="Description" style="width: 100%; height: auto;">

### Dans le fichier /WodQuestAPI/Properties/launchSettings.json modifier les adresses par l'adresse du serveur API correspondantes
<img src="Documentation/images/API.png" alt="Description" style="width: 60%; height: auto;">

## Lancer le projet

### Lancer les commandes suivantes :
    - cd .\WordQuestAPI\ (pas nécessaire si vous êtes déjà dans WordQuestAPI)
    - dotnet run

<img src="Documentation/images/run.png" alt="Description" style="width: 60%; height: auto;">