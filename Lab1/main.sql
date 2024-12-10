-- Удаление существующих таблиц
DROP TABLE IF EXISTS Transaction;
DROP TABLE IF EXISTS Expense;
DROP TABLE IF EXISTS Income;
DROP TABLE IF EXISTS Category;
DROP TABLE IF EXISTS Company;

-- Создание таблицы Company
CREATE TABLE Company (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    RegistrationDate DATE NOT NULL
);

-- Создание таблицы Category
CREATE TABLE Category (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL
);

-- Создание таблицы Income
CREATE TABLE Income (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    CompanyID INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Date DATE NOT NULL,
    FOREIGN KEY (CompanyID) REFERENCES Company(ID)
);

-- Создание таблицы Expense
CREATE TABLE Expense (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    CompanyID INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Date DATE NOT NULL,
    FOREIGN KEY (CompanyID) REFERENCES Company(ID)
);

-- Создание таблицы Transaction
CREATE TABLE Transaction (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    CompanyID INT NOT NULL,
    CategoryID INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Date DATE NOT NULL,
    FOREIGN KEY (CompanyID) REFERENCES Company(ID),
    FOREIGN KEY (CategoryID) REFERENCES Category(ID)
);

-- Наполнение таблицы Company данными
INSERT INTO Company (Name, RegistrationDate) VALUES 
('Tech Solutions', '2024-01-01'),
('Innovate Inc', '2024-02-15'),
('Future Corp', '2024-03-10');

-- Наполнение таблицы Category данными
INSERT INTO Category (Name) VALUES 
('Marketing'),
('Development'),
('Operations');

-- Наполнение таблицы Income данными
INSERT INTO Income (CompanyID, Amount, Date) VALUES 
(1, 5000.00, '2024-01-10'),
(2, 3000.00, '2024-02-20'),
(3, 4000.00, '2024-03-15');

-- Наполнение таблицы Expense данными
INSERT INTO Expense (CompanyID, Amount, Date) VALUES 
(1, 2000.00, '2024-01-15'),
(2, 1500.00, '2024-02-25'),
(3, 1000.00, '2024-03-20');

-- Наполнение таблицы Transaction данными
INSERT INTO Transaction (CompanyID, CategoryID, Amount, Date) VALUES 
(1, 1, 1000.00, '2024-01-12'),
(1, 2, 1500.00, '2024-01-18'),
(2, 1, 1200.00, '2024-02-22'),
(2, 3, 800.00, '2024-02-28'),
(3, 2, 2000.00, '2024-03-18');

-- Проверка данных: запросы SELECT
SELECT * FROM Company;
SELECT * FROM Category;
SELECT * FROM Income;
SELECT * FROM Expense;
SELECT * FROM Transaction;

-- Пример JOIN-запроса для отображения транзакций с категориями
SELECT 
    t.ID AS TransactionID,
    c.Name AS CompanyName,
    cat.Name AS CategoryName,
    t.Amount,
    t.Date
FROM Transaction t
JOIN Company c ON t.CompanyID = c.ID
JOIN Category cat ON t.CategoryID = cat.ID;

-- Пример фильтрации доходов, превышающих 3000
SELECT * FROM Income WHERE Amount > 3000;

-- Пример агрегации: общая сумма доходов по каждой компании
SELECT 
    CompanyID, 
    SUM(Amount) AS TotalIncome 
FROM Income 
GROUP BY CompanyID;
