# RedirectWebAPI
Web API для создания и получения коротких ссылок из длинных с использованием технологии Entity Framework Core и СУБД PostgreSQL.
## Полчение длинной ссылки из короткой
- Отправить <code><b>GET</b></code> запрос на адрес <code>/api/redirect/{короткий_url}</code>
- Сервер вернет длинную ссылку

![image](https://user-images.githubusercontent.com/78872275/173253550-876d795d-2cb2-49c9-a5e4-6de3d3f4e66c.png)

## Получение короткой ссылки из длинной
- Отправить <code><b>POST</b></code> запрос на адрес <code>/api/redirect?r={длинный_url}</code>
- Сервер вернет короткую ссылку

![image](https://user-images.githubusercontent.com/78872275/171829678-997d057c-e55f-4a3c-8c9c-a2cc220d7386.png)

## Схема базы данных (PostgreSQL)

![image](https://user-images.githubusercontent.com/78872275/173253633-642a99c0-3d88-4539-b138-e83814ce87e9.png)
