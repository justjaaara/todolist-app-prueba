# TODO simple

Lista de tareas con backend en .NET (API mínima, datos en memoria) y frontend en Angular.

## Requisitos

- .NET SDK 10
- Node.js y npm

## Backend (puerto 5080)

    cd backend/TodoApi
    dotnet run

Pruebas, desde esta carpeta:

    dotnet test

## Frontend (puerto 4200)

    cd frontend
    npm install
    npm start

Abre http://localhost:4200. El servidor de desarrollo reenvía `/api` al backend (ver `frontend/proxy.conf.json`), así que el backend debe estar corriendo.

Pruebas del frontend:

    npm test
