﻿Общая информация
Ваша команда создает приложение, работающее с модулями, поставляемыми сторонними командами программистов.
В целях обезопасить свое приложение, вы приняли решение запускать эти модули в отдельном домене.

Пункты с * являются необязательными и выполняются по согласованию с ментором.

Задание 1.
Разработайте библиотеку для запуска плагинов в отдельном домене, на базе API AppDomain. Библиотека должна позволять:
•	Загружать выбранный плагин в отдельный домен и возвращать точку входа к нему (в виде ссылки на объект/интерфейс)
•	Вручную выгружать плагин (и домен)
•	* Автоматически выгружать плагин и домен, если никто его не использует
•	* Загружать одновременно несколько версий плагина (при условии, что интерфейс точки входа не изменился)

Задание 2
Выполните задание аналогично 1-му, но на базе MAF (https://msdn.microsoft.com/en-us/library/bb384200(v=vs.100).aspx),
обратите внимание на пошаговый tutorial (https://msdn.microsoft.com/en-us/library/bb788290(v=vs.100).aspx).