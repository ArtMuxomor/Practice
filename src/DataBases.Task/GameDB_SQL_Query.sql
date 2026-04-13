CREATE DATABASE GameDB;
GO

USE GameDB;
GO

/*
	Список характеристик
*/
CREATE TABLE Stats (
	stat_id INT IDENTITY(1,1) PRIMARY KEY,
	stat_name NVARCHAR(50) NOT NULL,

	CHECK (LEN(stat_name) > 0)
);

/*
	Список типов оружия
*/
CREATE TABLE WeaponType (
	weapon_type_id INT IDENTITY(1,1) PRIMARY KEY,
	weapon_type_name NVARCHAR(50) NOT NULL,

	CHECK (LEN(weapon_type_name) > 0)
);

/*
	Список типов артефактов
*/
CREATE TABLE ArtifactType (
	artifact_type_id INT IDENTITY(1,1) PRIMARY KEY,
	artifact_type_name NVARCHAR(50) NOT NULL,

	CHECK (LEN(artifact_type_name) > 0)
);

/*
	Таблица персонажей
*/
CREATE TABLE Character (
	character_guid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	character_name NVARCHAR(100) NOT NULL,
	character_sex NCHAR(1),
	character_level INT NOT NULL DEFAULT 1,
	character_exp INT NOT NULL DEFAULT 0,
	character_coins INT NOT NULL DEFAULT 0,
	character_creation_time DATETIME2 NOT NULL DEFAULT GETDATE(),
	is_online BIT DEFAULT 0,

	CHECK (LEN(character_name) > 0),
	CHECK (character_sex IN (N'М', N'Ж')),
	CHECK (character_level >= 0),
	CHECK (character_exp >= 0),
	CHECK (character_coins >= 0)
);

/*
	Таблица оружия
*/
CREATE TABLE Weapon (
	weapon_guid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	weapon_type_id INT NOT NULL,
	weapon_level INT NOT NULL DEFAULT 0,
	weapon_exp INT NOT NULL DEFAULT 0,
	stat_id INT NOT NULL,
	weapon_multiplier DECIMAL(5, 2) NOT NULL DEFAULT 1.0,
	weapon_get_time DATETIME2 NOT NULL DEFAULT GETDATE(),

	FOREIGN KEY (weapon_type_id) REFERENCES WeaponType(weapon_type_id),
	FOREIGN KEY (stat_id) REFERENCES Stats(stat_id),

	CHECK (weapon_level >= 0),
	CHECK (weapon_exp >= 0),
	CHECK (weapon_multiplier >= 1.0)
);

/*
	Таблица артефактов
*/
CREATE TABLE Artifact (
	artifact_guid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	artifact_type_id INT NOT NULL,
	artifact_level INT NOT NULL DEFAULT 0,
	artifact_exp INT NOT NULL DEFAULT 0,
	stat_id INT NOT NULL,
	artifact_multiplier DECIMAL(5, 2) NOT NULL DEFAULT 1.0,
	artifact_get_time DATETIME2 NOT NULL DEFAULT GETDATE(),

	FOREIGN KEY (artifact_type_id) REFERENCES ArtifactType(artifact_type_id),
	FOREIGN KEY (stat_id) REFERENCES Stats(stat_id),

	CHECK (artifact_level >= 0),
	CHECK (artifact_exp >= 0),
	CHECK (artifact_multiplier >= 1.0)
);

/*
	Связь Персонаж-Оружие
*/
CREATE TABLE Character_Weapon (
	character_guid UNIQUEIDENTIFIER NOT NULL,
	weapon_guid UNIQUEIDENTIFIER NOT NULL,
	is_equipped BIT DEFAULT 0,

	PRIMARY KEY (character_guid, weapon_guid),

	FOREIGN KEY (character_guid) REFERENCES Character(character_guid),
	FOREIGN KEY (weapon_guid) REFERENCES Weapon(weapon_guid)
);

/*
	Связь Персонаж-Артефакт
*/
CREATE TABLE Character_Artifact (
	character_guid UNIQUEIDENTIFIER NOT NULL,
	artifact_guid UNIQUEIDENTIFIER NOT NULL,
	is_equipped BIT DEFAULT 0,

	PRIMARY KEY (character_guid, artifact_guid),

	FOREIGN KEY (character_guid) REFERENCES Character(character_guid),
	FOREIGN KEY (artifact_guid) REFERENCES Artifact(artifact_guid)
);



/*
	1. Справочники
*/
INSERT INTO Stats (stat_name) VALUES
	(N'Атака'),
	(N'ХП'),
	(N'Скорость'),
	(N'Крит'),
	(N'Понижение урона'),
	(N'Бонус опыта');

INSERT INTO WeaponType (weapon_type_name) VALUES
	(N'Меч'),
	(N'Лук'),
	(N'Копьё'),
	(N'Арбалет'),
	(N'Коса'),
	(N'Пика');

INSERT INTO ArtifactType (artifact_type_name) VALUES
	(N'Свиток'),
	(N'Зелье'),
	(N'Доспех'),
	(N'Книга'),
	(N'Кольцо'),
	(N'Амулет');

DECLARE @character1 UNIQUEIDENTIFIER = NEWID();
DECLARE @character2 UNIQUEIDENTIFIER = NEWID();
DECLARE @character3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Character (character_guid, character_name, character_sex, character_level, character_exp, character_coins) VALUES
	(@character1, N'Гост', N'М', 10, 1000, 500),
	(@character2, N'Макс', N'М', 15, 2500, 1200),
	(@character3, N'Аки', N'Ж', 50, 99999, 100000);

INSERT INTO Weapon (weapon_type_id, weapon_level, stat_id, weapon_multiplier) VALUES
	-- Мечи
	(1, 1, 1, 1.1), (1, 5, 1, 1.5), (1, 10, 4, 2.0),

	-- Луки
	(2, 1, 3, 1.1), (2, 5, 4, 1.3), (2, 10, 1, 1.8),

	-- Копья
	(3, 1, 1, 1.2), (3, 7, 5, 1.4), (3, 12, 1, 2.1),

	-- Арбалеты
	(4, 2, 4, 1.2), (4, 6, 3, 1.5), (4, 11, 4, 1.9),

	-- Косы
	(5, 3, 1, 1.5), (5, 8, 4, 1.8), (5, 15, 6, 2.5),

	-- Пики
	(6, 4, 5, 1.3), (6, 9, 2, 1.6), (6, 14, 1, 2.2);

INSERT INTO Artifact (artifact_type_id, artifact_level, stat_id, artifact_multiplier) VALUES
	-- Свитки
	(1, 1, 6, 1.05), (1, 3, 6, 1.1), (1, 5, 1, 1.2), (1, 2, 6, 1.08), (1, 10, 6, 1.5),

	-- Зелья
	(2, 1, 2, 1.1), (2, 4, 2, 1.3), (2, 1, 3, 1.1), (2, 2, 2, 1.15), (2, 5, 5, 1.4),

	-- Доспехи
	(3, 5, 2, 1.5), (3, 1, 5, 1.1), (3, 10, 5, 2.0), (3, 3, 2, 1.25), (3, 7, 5, 1.6),

	-- Книги
	(4, 1, 6, 1.2), (4, 5, 1, 1.3), (4, 10, 4, 1.8), (4, 2, 6, 1.25), (4, 8, 1, 1.6),

	-- Кольца
	(5, 1, 4, 1.1), (5, 5, 4, 1.4), (5, 10, 4, 2.0), (5, 3, 1, 1.2), (5, 7, 3, 1.5),

	-- Амулеты
	(6, 1, 2, 1.1), (6, 6, 5, 1.5), (6, 12, 5, 2.2), (6, 2, 3, 1.15), (6, 9, 4, 1.8);

/*
	Гост (@character1): Меч и 1 артефакт
*/
INSERT INTO Character_Weapon (character_guid, weapon_guid, is_equipped) VALUES
(
	@character1,
	(SELECT TOP 1 W.weapon_guid
		FROM Weapon W
		WHERE W.weapon_type_id = 1
		AND NOT EXISTS (
			SELECT 1
				FROM Character_Weapon CW
				WHERE CW.weapon_guid = W.weapon_guid
		)
	),
	1
);

INSERT INTO Character_Artifact (character_guid, artifact_guid, is_equipped)
	SELECT TOP 1 @character1, A.artifact_guid, 1
	FROM Artifact A
	WHERE NOT EXISTS (
		SELECT 1
		FROM Character_Artifact CA
		WHERE CA.artifact_guid = A.artifact_guid
	);

/*
	Макс (@character2): Лук и 2 Артефакта
*/
INSERT INTO Character_Weapon (character_guid, weapon_guid, is_equipped) VALUES
(
	@character2,
	(SELECT TOP 1 W.weapon_guid
		FROM Weapon W
		WHERE W.weapon_type_id = 1
		AND NOT EXISTS (
			SELECT 1
				FROM Character_Weapon CW
				WHERE CW.weapon_guid = W.weapon_guid
		)
	),
	1
);

INSERT INTO Character_Artifact (character_guid, artifact_guid, is_equipped)
	SELECT TOP 2 @character2, A.artifact_guid, 1
	FROM Artifact A
	WHERE NOT EXISTS (
		SELECT 1
		FROM Character_Artifact CA
		WHERE CA.artifact_guid = A.artifact_guid
	);

/*
	Аки (@character3): Коса и 3 Артефактов
*/
INSERT INTO Character_Weapon (character_guid, weapon_guid, is_equipped) VALUES
(
	@character3,
	(SELECT TOP 1 W.weapon_guid
		FROM Weapon W
		WHERE W.weapon_type_id = 1
		AND NOT EXISTS (
			SELECT 1
				FROM Character_Weapon CW
				WHERE CW.weapon_guid = W.weapon_guid
		)
	),
	1
);

INSERT INTO Character_Artifact (character_guid, artifact_guid, is_equipped)
	SELECT TOP 3 @character3, A.artifact_guid, 1
	FROM Artifact A
	WHERE NOT EXISTS (
		SELECT 1
		FROM Character_Artifact CA
		WHERE CA.artifact_guid = A.artifact_guid
	);
GO



/*
	Представления
*/

/*
	Топ 10 высокоуровневых персонажей
*/
CREATE VIEW top_10_high_level_heroes AS
SELECT TOP 10
	character_name,
	character_level,
	character_coins
FROM Character
WHERE character_level >= 30
ORDER BY character_coins DESC;
GO

/*
	Статистика по артефактам
*/
CREATE VIEW artifact_total_stats AS
SELECT
	at.artifact_type_name,
	COUNT(a.artifact_guid) AS TotalCount,
	AVG(a.artifact_multiplier) AS AveragePower
FROM ArtifactType at
LEFT JOIN Artifact a ON at.artifact_type_id = a.artifact_type_id
GROUP BY at.artifact_type_name;
GO

/*
	Информация по оружиям
*/
CREATE VIEW character_weapon_stats AS
SELECT
	C.character_name,
	ISNULL(WT.weapon_type_name, N'Нет оружия') AS weapon_type_name,
	ISNULL(W.weapon_multiplier, 0.0) AS weapon_multiplier
FROM Character C
LEFT JOIN Character_Weapon CW ON C.character_guid = CW.character_guid AND CW.is_equipped = 1
LEFT JOIN Weapon W ON CW.weapon_guid = W.weapon_guid
LEFT JOIN WeaponType WT ON W.weapon_type_id = WT.weapon_type_id;
GO



/*
	Запросы
*/

/*
	Запрос на обновление данных (множители арбалетов * 1,05)
*/
UPDATE W
SET W.weapon_multiplier = W.weapon_multiplier * 1.05
FROM Weapon W
INNER JOIN WeaponType WT ON W.weapon_type_id = WT.weapon_type_id
WHERE WT.weapon_type_name = N'Арбалет';

/*
	Запрос на удаление данных (ничьи артефакты)
*/
DELETE FROM Artifact
WHERE NOT EXISTS (
	SELECT 1 
	FROM Character_Artifact CA 
	WHERE CA.artifact_guid = Artifact.artifact_guid AND CA.is_equipped = 1
);