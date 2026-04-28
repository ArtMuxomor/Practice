CREATE DATABASE GameDB;
GO

USE GameDB;
GO

/*
	Список характеристик
*/
CREATE TABLE Stats (
	StatId INT IDENTITY(1,1) PRIMARY KEY
	, StatName NVARCHAR(50) NOT NULL

	, CHECK (LEN(StatName) > 0)
);

/*
	Список типов оружия
*/
CREATE TABLE WeaponType (
	WeaponTypeId INT IDENTITY(1,1) PRIMARY KEY
	, WeaponTypeName NVARCHAR(50) NOT NULL

	, CHECK (LEN(WeaponTypeName) > 0)
);

/*
	Список типов артефактов
*/
CREATE TABLE ArtifactType (
	ArtifactTypeId INT IDENTITY(1,1) PRIMARY KEY
	, ArtifactTypeName NVARCHAR(50) NOT NULL

	, CHECK (LEN(ArtifactTypeName) > 0)
);

/*
	Таблица персонажей
*/
CREATE TABLE Character (
	CharacterGuid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID()
	, CharacterName NVARCHAR(100) NOT NULL
	, CharacterSex NCHAR(1)
	, CharacterLevel INT NOT NULL DEFAULT 1
	, CharacterExp INT NOT NULL DEFAULT 0
	, CharacterCoins INT NOT NULL DEFAULT 0
	, CharacterCreation_Time DATETIME2 NOT NULL DEFAULT GETDATE()
	, IsOnline BIT DEFAULT 0

	, CHECK (LEN(CharacterName) > 0)
	, CHECK (CharacterSex IN (N'М', N'Ж'))
	, CHECK (CharacterLevel >= 0)
	, CHECK (CharacterExp >= 0)
	, CHECK (CharacterCoins >= 0)
);

/*
	Таблица оружия
*/
CREATE TABLE Weapon (
	WeaponGuid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID()
	, WeaponTypeId INT NOT NULL
	, WeaponLevel INT NOT NULL DEFAULT 0
	, WeaponExp INT NOT NULL DEFAULT 0
	, StatId INT NOT NULL
	, WeaponMultiplier DECIMAL(5, 2) NOT NULL DEFAULT 1.0
	, WeaponGetTime DATETIME2 NOT NULL DEFAULT GETDATE()

	, FOREIGN KEY (WeaponTypeId) REFERENCES WeaponType(WeaponTypeId)
	, FOREIGN KEY (StatId) REFERENCES Stats(StatId)

	, CHECK (WeaponLevel >= 0)
	, CHECK (WeaponExp >= 0)
	, CHECK (WeaponMultiplier >= 1.0)
);

/*
	Таблица артефактов
*/
CREATE TABLE Artifact (
	ArtifactGuid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID()
	, ArtifactTypeId INT NOT NULL
	, ArtifactLevel INT NOT NULL DEFAULT 0
	, ArtifactExp INT NOT NULL DEFAULT 0
	, StatId INT NOT NULL
	, ArtifactMultiplier DECIMAL(5, 2) NOT NULL DEFAULT 1.0
	, ArtifactGetTime DATETIME2 NOT NULL DEFAULT GETDATE()

	, FOREIGN KEY (ArtifactTypeId) REFERENCES ArtifactType(ArtifactTypeId)
	, FOREIGN KEY (StatId) REFERENCES Stats(StatId)

	, CHECK (ArtifactLevel >= 0)
	, CHECK (ArtifactExp >= 0)
	, CHECK (ArtifactMultiplier >= 1.0)
);

/*
	Связь Персонаж-Оружие
*/
CREATE TABLE Character_Weapon (
	CharacterGuid UNIQUEIDENTIFIER NOT NULL
	, WeaponGuid UNIQUEIDENTIFIER NOT NULL
	, IsEquipped BIT DEFAULT 0

	, PRIMARY KEY (CharacterGuid, WeaponGuid)

	, FOREIGN KEY (CharacterGuid) REFERENCES Character(CharacterGuid)
	, FOREIGN KEY (WeaponGuid) REFERENCES Weapon(WeaponGuid)
);

/*
	Связь Персонаж-Артефакт
*/
CREATE TABLE Character_Artifact (
	CharacterGuid UNIQUEIDENTIFIER NOT NULL
	, ArtifactGuid UNIQUEIDENTIFIER NOT NULL
	, IsEquipped BIT DEFAULT 0

	, PRIMARY KEY (CharacterGuid, ArtifactGuid)

	, FOREIGN KEY (CharacterGuid) REFERENCES Character(CharacterGuid)
	, FOREIGN KEY (ArtifactGuid) REFERENCES Artifact(ArtifactGuid)
);



/*
	1. Справочники
*/
INSERT INTO Stats (StatName) VALUES
	(N'Атака')
	, (N'ХП')
	, (N'Скорость')
	, (N'Крит')
	, (N'Понижение урона')
	, (N'Бонус опыта');

INSERT INTO WeaponType (WeaponTypeName) VALUES
	(N'Меч')
	, (N'Лук')
	, (N'Копьё')
	, (N'Арбалет')
	, (N'Коса')
	, (N'Пика');

INSERT INTO ArtifactType (ArtifactTypeName) VALUES
	(N'Свиток')
	, (N'Зелье')
	, (N'Доспех')
	, (N'Книга')
	, (N'Кольцо')
	, (N'Амулет');

DECLARE @character1 UNIQUEIDENTIFIER = NEWID();
DECLARE @character2 UNIQUEIDENTIFIER = NEWID();
DECLARE @character3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Character (CharacterGuid, CharacterName, CharacterSex, CharacterLevel, CharacterExp, CharacterCoins) VALUES
	(@character1, N'Гост', N'М', 10, 1000, 500)
	, (@character2, N'Макс', N'М', 15, 2500, 1200)
	, (@character3, N'Аки', N'Ж', 50, 99999, 100000);

INSERT INTO Weapon (WeaponTypeId, WeaponLevel, StatId, WeaponMultiplier) VALUES
	-- Мечи
	(1, 1, 1, 1.1), (1, 5, 1, 1.5), (1, 10, 4, 2.0)

	-- Луки
	, (2, 1, 3, 1.1), (2, 5, 4, 1.3), (2, 10, 1, 1.8)

	-- Копья
	, (3, 1, 1, 1.2), (3, 7, 5, 1.4), (3, 12, 1, 2.1)

	-- Арбалеты
	, (4, 2, 4, 1.2), (4, 6, 3, 1.5), (4, 11, 4, 1.9)

	-- Косы
	, (5, 3, 1, 1.5), (5, 8, 4, 1.8), (5, 15, 6, 2.5)

	-- Пики
	, (6, 4, 5, 1.3), (6, 9, 2, 1.6), (6, 14, 1, 2.2);

INSERT INTO Artifact (ArtifactTypeId, ArtifactLevel, StatId, ArtifactMultiplier) VALUES
	-- Свитки
	(1, 1, 6, 1.05), (1, 3, 6, 1.1), (1, 5, 1, 1.2), (1, 2, 6, 1.08), (1, 10, 6, 1.5)

	-- Зелья
	, (2, 1, 2, 1.1), (2, 4, 2, 1.3), (2, 1, 3, 1.1), (2, 2, 2, 1.15), (2, 5, 5, 1.4)

	-- Доспехи
	, (3, 5, 2, 1.5), (3, 1, 5, 1.1), (3, 10, 5, 2.0), (3, 3, 2, 1.25), (3, 7, 5, 1.6)

	-- Книги
	, (4, 1, 6, 1.2), (4, 5, 1, 1.3), (4, 10, 4, 1.8), (4, 2, 6, 1.25), (4, 8, 1, 1.6)

	-- Кольца
	, (5, 1, 4, 1.1), (5, 5, 4, 1.4), (5, 10, 4, 2.0), (5, 3, 1, 1.2), (5, 7, 3, 1.5)

	-- Амулеты
	, (6, 1, 2, 1.1), (6, 6, 5, 1.5), (6, 12, 5, 2.2), (6, 2, 3, 1.15), (6, 9, 4, 1.8);

/*
	Гост (@character1): Меч и 1 артефакт
*/
INSERT INTO Character_Weapon (CharacterGuid, WeaponGuid, IsEquipped) VALUES
(
	@character1
	, (SELECT TOP 1 W.WeaponGuid
		FROM Weapon W
		WHERE W.WeaponTypeId = 1
		AND NOT EXISTS (
			SELECT 1
				FROM Character_Weapon CW
				WHERE CW.WeaponGuid = W.WeaponGuid
		)
	)
	, 1
);

INSERT INTO Character_Artifact (CharacterGuid, ArtifactGuid, IsEquipped)
	SELECT TOP 1 @character1, A.ArtifactGuid, 1
	FROM Artifact A
	WHERE NOT EXISTS (
		SELECT 1
		FROM Character_Artifact CA
		WHERE CA.ArtifactGuid = A.ArtifactGuid
	);

/*
	Макс (@character2): Лук и 2 Артефакта
*/
INSERT INTO Character_Weapon (CharacterGuid, WeaponGuid, IsEquipped) VALUES
(
	@character2
	, (SELECT TOP 1 W.WeaponGuid
		FROM Weapon W
		WHERE W.WeaponTypeId = 1
		AND NOT EXISTS (
			SELECT 1
				FROM Character_Weapon CW
				WHERE CW.WeaponGuid = W.WeaponGuid
		)
	)
	, 1
);

INSERT INTO Character_Artifact (CharacterGuid, ArtifactGuid, IsEquipped)
	SELECT TOP 2 @character2, A.ArtifactGuid, 1
	FROM Artifact A
	WHERE NOT EXISTS (
		SELECT 1
		FROM Character_Artifact CA
		WHERE CA.ArtifactGuid = A.ArtifactGuid
	);

/*
	Аки (@character3): Коса и 3 Артефактов
*/
INSERT INTO Character_Weapon (CharacterGuid, WeaponGuid, IsEquipped) VALUES
(
	@character3
	, (SELECT TOP 1 W.WeaponGuid
		FROM Weapon W
		WHERE W.WeaponTypeId = 1
		AND NOT EXISTS (
			SELECT 1
				FROM Character_Weapon CW
				WHERE CW.WeaponGuid = W.WeaponGuid
		)
	)
	, 1
);

INSERT INTO Character_Artifact (CharacterGuid, ArtifactGuid, IsEquipped)
	SELECT TOP 3 @character3, A.ArtifactGuid, 1
	FROM Artifact A
	WHERE NOT EXISTS (
		SELECT 1
		FROM Character_Artifact CA
		WHERE CA.ArtifactGuid = A.ArtifactGuid
	);
GO



/*
	Представления
*/

/*
	Топ 10 высокоуровневых персонажей
*/
CREATE VIEW top_10_High_Level_Heroes AS
SELECT TOP 10
	CharacterName
	, CharacterLevel
	, CharacterCoins
FROM Character
WHERE CharacterLevel >= 30
ORDER BY CharacterCoins DESC;
GO

/*
	Статистика по артефактам
*/
CREATE VIEW Artifact_Total_Stats AS
SELECT
	at.ArtifactTypeName
	, COUNT(a.ArtifactGuid) AS TotalCount
	, AVG(a.ArtifactMultiplier) AS AveragePower
FROM ArtifactType at
LEFT JOIN Artifact a ON at.ArtifactTypeId = a.ArtifactTypeId
GROUP BY at.ArtifactTypeName;
GO

/*
	Информация по оружиям
*/
CREATE VIEW Character_Weapon_Stats AS
SELECT
	C.CharacterName
	, ISNULL(WT.WeaponTypeName, N'Нет оружия') AS WeaponTypeName
	, ISNULL(W.WeaponMultiplier, 0.0) AS WeaponMultiplier
FROM Character C
LEFT JOIN Character_Weapon CW ON C.CharacterGuid = CW.CharacterGuid AND CW.IsEquipped = 1
LEFT JOIN Weapon W ON CW.WeaponGuid = W.WeaponGuid
LEFT JOIN WeaponType WT ON W.WeaponTypeId = WT.WeaponTypeId;
GO



/*
	Запросы
*/

/*
	Запрос на обновление данных (множители арбалетов * 1,05)
*/
UPDATE W
SET W.WeaponMultiplier = W.WeaponMultiplier * 1.05
FROM Weapon W
INNER JOIN WeaponType WT ON W.WeaponTypeId = WT.WeaponTypeId
WHERE WT.WeaponTypeName = N'Арбалет';

/*
	Запрос на удаление данных (ничьи артефакты)
*/
DELETE FROM Artifact
WHERE NOT EXISTS (
	SELECT 1 
	FROM Character_Artifact CA 
	WHERE CA.ArtifactGuid = Artifact.ArtifactGuid AND CA.IsEquipped = 1
);