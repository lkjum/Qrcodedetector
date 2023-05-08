# QRCodeDetector 📑 #

## Introduction 💭 ##

Le projet HUB (QRCodeDetector), a été consu pour répondre à un problème: Lorsque nous sommes sur un téléphone, nous ne pouvons pas directement analyser le contenu des QRCodes. Il faut donc un deuxième appareil. Ici, cette solution propose que lorsque vous prenez une capture d'écran du QRCode, celui-ci est automatiquement analysé, et son contenu vous est rendu disponible. Plus besoin de perdre du temps.

## 1. API FLASK 💻 ##

QRCodeDetector utilise une api qui peut être lancé très facile sur un serveur. Le code python est donnée. L'application utilise la libraire OpenCV pour analyser le contenu des QRCode et pallier au QRCode avec une inversion de couleurs. C'est ici qu'on va renvoyé le contenu du QRCode à l'application grâce à la chaine de caractère au format base64 obtenu depuis le screenshot et envoyé à l'api.

## 2. XAMARIN APP (ANDROID UNIQUEMENT) 💽 ## 

Pour utiliser l'application, il faut aller dans le dossier QRCodeDetector. L'application est donnée et encore configurer pour taper sur mon serveur. A vous de modifier ça a votre guise. Il n'y pas de sécurité mise en place au niveau des requête, des variables car ce n'était pas le but du projet.

On a deux features particulièrement importantes:
- Lancement de l'observateur de screenshot au démarrage du téléphone en tâche de fond.
- Lancement de l'observateur de screenshot et d'une tâche de fond pour l'utiliser lors du démarrage de l'application.

Un bouton pour quitter se trouvera dans une tâche de fond. Il n'y a aucune interface graphique, ou presque, l'application possède uniquement une pop-up Oui/Non pour vous demander l'accès à votre naviguateur si le QRCode contient un lien. S'il n'en contient pas, un simple texte Toast vous sera affiché.



