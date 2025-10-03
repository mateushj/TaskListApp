# TaskListApp

Aplicação de gerenciamento de tarefas com **frontend Angular** e **backend .NET 8**, utilizando **PostgreSQL** como banco de dados. Suporta execução via Docker com Nginx servindo o frontend.

---

## Tecnologias Utilizadas

| Camada         | Tecnologia / Ferramenta                   |
|----------------|-----------------------------------------|
| Backend        | .NET 8 Web API, C#                       |
| Frontend       | Angular 20, Angular Material, SPA       |
| Banco de Dados | PostgreSQL                             |
| Container      | Docker, Docker Compose                    |
| Servidor Web   | Nginx (frontend estático + proxy /api)  |
| ORM            | Entity Framework Core                     |
| Funcionalidades Extras | matSort, matPaginator             |

## Estrutura do Projeto
```text
TaskListApp/
│
├─ backend/TaskListAPI/             # Backend .NET 8 (API REST)
│   ├─ Dockerfile                    # Dockerfile do backend
│   └─ ...                           # Código C#, appsettings.json, etc.
│
├─ frontend/task-list-frontend/     # Frontend Angular SPA
│   ├─ Dockerfile                    # Dockerfile do frontend
│   ├─ nginx.conf                    # Configuração Nginx para servir SPA e proxy /api
│   └─ ...                           # Código Angular, package.json, etc.
│
└─ docker-compose.yml               # Orquestração de containers
```
---

## Funcionalidades

- Cadastro, edição e exclusão de tarefas.
- Controle de status: A Fazer, Em Progresso, Concluída.
- Prazo de conclusão e descrição detalhada.
- Ordenação e paginação de tarefas via Angular Material (`matSort` e `matPaginator`).
- Frontend SPA responsivo.
- API REST consumível por qualquer cliente HTTP.

---

## Setup com Docker

O projeto já vem com **Docker e Docker Compose configurados**, incluindo backend, frontend e PostgreSQL.

### Iniciar a aplicação

```bash
docker-compose up --build
```
Isso irá:

1. Subir o banco de dados PostgreSQL.

2. Construir e iniciar o backend .NET 8.

3. Construir e servir o frontend Angular via Nginx.

### Acessos

Frontend: http://localhost:4200

Backend: http://localhost:5000/api/TaskItem

### Banco de Dados

O banco PostgreSQL é criado automaticamente via Docker Compose.

As migrations do Entity Framework Core são aplicadas ao iniciar o backend.

Configuração de conexão via variável de ambiente ConnectionStrings__DefaultConnection.