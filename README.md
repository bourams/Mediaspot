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
 - Swagger partielle
 - organisation des endpoints de title

Ce qu’il faudrait faire (liste non exhaustive) :
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

Je vais continuer sur middle.