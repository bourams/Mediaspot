Middle:
Ce qui a été fait:
- Ajout d'une route pour déclencher le transcodage
- Ajout d'une queue
- Ajout partiel d'un worker => je voulais que mon worker s'occupe uniquement du transcoding
  et léve des événements à chaque changement de statut du transcode job.
  Les méthodes Mark mon un peu perturbé, elles m'ont donné l'impression que le test technique demandait de persister dans le worker.

Ce qui reste à faire:
- Supprimer la ligne 13 de TranscodeStatusChangedHandler, je l'ai mise pour te montrer ce que je voulais faire
- Gestion des erreurs
- Gestion du transcodage en cas de crash
- Les tests
