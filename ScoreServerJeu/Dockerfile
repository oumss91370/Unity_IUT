# Utiliser l'image officielle de Python
FROM python:3.8-slim

# Définir le répertoire de travail
WORKDIR /app

# Copier le script du serveur dans le conteneur
COPY server.py .

# Exposer le port sur lequel le serveur va écouter
EXPOSE 9999

# Démarrer le serveur
CMD ["python", "server.py"]
