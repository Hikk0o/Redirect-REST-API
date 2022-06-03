# RedirectWebAPI
Web API для создания и получения коротких ссылок из длинных
## Полчение длинной ссылки из короткой
- Отправить GET запрос на адрес <code>/api/redirect</code>, в параметрах строки указать <code>r={короткий_url}</code>
- Сервер вернет длинную ссылку

![image](https://user-images.githubusercontent.com/78872275/171828123-5e8f1cef-c0ae-41a1-919c-36a99a625036.png)

## Получение короткой ссылки из длинной
- Отправить POST запрос на адрес <code>/api/redirect</code>, в параметрах строки указать <code>r={длинный_url}</code>
- Сервер вернет короткую ссылку

![image](https://user-images.githubusercontent.com/78872275/171829336-ead915e5-51fb-400e-aee5-6672cb3d0769.png)
