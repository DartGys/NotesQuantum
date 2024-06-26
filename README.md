## Опис проекту "Notes"

### Архітектура проекту

Проект "Notes" реалізований з використанням архітектурного підходу, який базується на розділенні на шари. Основні шари включають:

1. **Data Access Layer (DAL)**: Цей шар відповідає за взаємодію з базою даних. У цьому шарі реалізовано модель даних, контекст бази даних і репозиторії для доступу до даних.

2. **Business Logic Layer (BLL)**: Шар бізнес-логіки відповідає за обробку бізнес-логіки, правил та логіки застосунку. У цьому шарі реалізовані сервіси для виконання різних операцій над даними.

3. **Presentation Layer (API)**: Шар презентації відповідає за взаємодію з клієнтами за допомогою API. У цьому шарі реалізовані контролери, які обробляють запити та відправляють відповіді.

4. **UI Layer(WebUI)**: Шар який відповідає за інтерфейс та звязок з Api

### Реалізація

Проект реалізовано на платформі .NET з використанням ASP.NET Core для створення веб-сервісу. Для роботи з базою даних використовується Entity Framework Core. Проект структуровано згідно шаблону проекту ASP.NET Core, з розділенням на файли за функціональністю.

### Функціонал

Проект "Notes" дозволяє користувачам створювати, переглядати, оновлювати та видаляти нотатки. Основний функціонал включає:

- **Додавання нотаток**: Користувач може створювати нові нотатки, вказуючи заголовок та вміст.

- **Перегляд нотаток**: Користувач може переглядати список всіх нотаток.

- **Перегляд окремої нотатки**: Користувач може переглядати окрему нотатку за її унікальним ідентифікатором.

- **Оновлення нотаток**: Користувач може оновлювати інформацію вже існуючих нотаток, змінюючи заголовок та вміст.

- **Видалення нотаток**: Користувач може видаляти нотатки, які вже були створені.

Проект "Notes" реалізує простий та зручний інтерфейс для управління нотатками, що дозволяє користувачам ефективно організовувати свої записи.

### Тести

Проект "Notes" також включає широкий набір тестів для перевірки коректності його функціональності. Тести розділені на дві основні категорії:

1. **Unit Tests**: Ці тести призначені для перевірки окремих компонентів програми на коректність їх роботи в ізоляції від інших компонентів. У проекті "Notes" юніт-тести використовуються для перевірки роботи окремих методів класів, зокрема методів сервісів, репозиторіїв та інших складових системи.

2. **Integration Tests**: Ці тести перевіряють взаємодію різних компонентів програми у реальному середовищі виконання. У проекті "Notes" інтеграційні тести використовуються для перевірки відповідності поведінки API очікуваному результату при взаємодії з ним через HTTP-запити.

Тести в проекті "Notes" допомагають забезпечити надійність та стабільність програми, дозволяючи виявити та виправити помилки на ранніх етапах розробки та забезпечуючи високу якість програмного продукту.
