# Сервис для управления навыками сотрудников

## Запуск
для запуска необходимо скопировать репозиторий и в корне проекта создать .env файл.
Пример .env:
```
APP_PORT=8080
ENV=Development
DB_CONNECTION_STRING="Host=db;Port=5432;Database=skills;Username=postgres;Password=postgres"

POSTGRES_DB=skills
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
DB_PORT=5432
```
После чего необходимо запустить докер и выполнить команду:

```bash
docker-compose up -d --build
```

## Отправка запросов
После запуска можно перейти по localhost:8080/swagger (указать нужный порт) чтобы перейти в сваггер документацию.

