BASIC

Ce que j’ai fait / ma démarche :
J’ai cherché à rester simple dans un temps malheureusement très court.
j’ai agi comme si j’arrivais sur un projet existant. Je ne cherche pas à révolutionner ce qui est déjà en place ni à tout refactoriser.
J’ai essayé de comprendre pourquoi les choses avaient été faites de cette manière.
Habituellement, j’aurais posé des questions sur : l’existant, les conventions utilisées sur le projet, etc.
J’ai essayé de rester cohérent et dans le même style que l’existant.

Ce que j'ai fait:
 - Ajout de l'aggregate root Title
 - Post title 
 - Get tile
 - Put Title
 - Swagger partielle
 - organisation des endpoints de title

Ce qu’il faudrait faire:
 - Versioner l'api
 - Mettre les endpoints d’Asset dans un dossier dédié (comme pour Title)
 - Faire de même pour les entités dans le DbContext
 - Recevoir et retourner des DTOs (en me documentant, j’ai vu des patterns comme REPR, etc.)
 - Enrichir Swagger
 - Éventuellement fournir une collection Postman avec des tests
 - Gérer les exceptions de façon centralisée
 - Spécialiser les exceptions métier
 - J’ai une petite préférence pour le pattern Result plutôt que d’utiliser des exceptions partout
 - Retourner les enums en tant que chaînes (sérialisation)
 - Ajouter du logging, de la télémétrie, etc.
 - Ajouter les endpoints de mise à jour et de listing pour Title
 - Bien tester

Ce que j’aurais voulu faire idéalement en plus :
 - Ajouter des tests unitaires si besoin
 - Ajouter des tests d’intégration
 - Mettre en place un pipeline build / test etc
 - Gérer les environnements
 - Refactorer les tests unitaires (mieux organisés)

Pour ce qui est du transcodage (middle), voici ce que je pense devoir faire :
1 - Il y a quelque chose, à partir de l’asset, qui déclenche le transcodage (metadata ? mediafile ?)
2 - Mediaspot.Domain.Assets.Events.TranscodeRequested est levé
3 - Mediaspot.Application.Events.TranscodeRequestedHandler intercepte l’événement (save changes, etc.) : le transcode est en Pending
4 - Normalement, le transcode est mis dans une queue pour qu’un worker le consomme
5- Le worker le récupère, met à jour l’état en Running (delay)
6 - Le worker met à jour la base de données (ou lève un event ? ce n’est pas encore clair pour moi)
7 -Une fois fini, le worker met l’état en Completed
8 - La base est mise à jour

En cas d’échec (Fail), dans un premier temps, l’insérer en base. Une fois que ça fonctionne, ajouter un système de retry, d’alerting et de logging.
Faire attention à ce que l’ordre des états (Pending → Running → Succeeded) ne puisse pas être inversé. Voir ce qu’il faut faire si l’API plante.
En me documentant, je suis tombé sur le pattern Outbox. Je ne m’y suis pas attardé car l’objectif de ce test était d’avoir quelque chose de fonctionnel d’abord.
