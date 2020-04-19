для запуска testreactapp

npm install

команды (все одним разом)

npm run dev

после Compiled successfully

в терминале Project is running at


----------------------- (использованые dependencies)

npm i react

npm i react-router

npm i react-router-dom

-- (все одним разом)
npm install --save-dev @babel/core @babel/preset-env  @babel/preset-react babel-loader css-loader webpack webpack-cli style-loader webpack-dev-server


--

npm install --save i18next

npm install --save react-i18next

npm install --save-dev jest


------- (для тестов)

сервер должен быть запущен

npm test

 
 
ошибка может возникнуть при запуске тестов 

The error message also says to add SKIP_PREFLIGHT_CHECK=true

to an .env file in your project.

Couldn't find any .env file.

1. Delete file package-lock.json

2. Delete folder node_modules

3. Remove all text in devDependencies from package.json

4. Run: npm install
--------- 


--------- (для приложения)
npm run dev
---------



Cтарт cервер от проекта: TestWebApiCore(запуститься консоль и браузер) 

скрипт для базы данных: TestWebApiCore->app->data->scriptdb.txt

настройки для базы данных: TestWebApiCore->appsettings.json

База данных сделана в таком виде для упрощение старта проекта



	Важно: чтобы работали тесты и приложение
1.тесты

будет предедущая ошибка при запуске тестов если есть devDependencies

повторить шаги (для тестов)

2.приложение

после завершение тестирование повторить шаги (все одним разом)

