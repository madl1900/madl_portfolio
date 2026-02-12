USE sheet_music;

-- publisher, book, genre, composer, instrument, song, song_composer, song_instrument

INSERT INTO publisher 
VALUES
	(DEFAULT, 'Hal Leonard', 'Milwaukee', 'WI'),
    (DEFAULT, 'Key 88 Records, Inc', NULL, NULL),
    (DEFAULT, 'AMSCO Publications', NULL, NULL);

INSERT INTO book
VALUES
	('978-0-8256-2028-7', 'Clarinet Solos', 1987, 3),
    ('978-1-4584-2314-6', "Everybody's Favorite Classical Piano Pieces", 2012, 3),
    ('0-7935-1473-8', "Disney's The Musical Newsies", 1992, 1),
    ('6-54367-28305-9', 'Believe', 2010, 2),
    ('978-1-4803-6652-7', 'River Flows in You and Other Eloquent Songs for Solo Piano', 2014, 1);
    
INSERT INTO genre
VALUES
	(DEFAULT, 'Hymn'),
    (DEFAULT, 'Classical'),
    (DEFAULT, 'Traditional'),
    (DEFAULT, 'Soundtrack'),
    (DEFAULT, 'Pop');
    
INSERT INTO composer (fname, mname, lname)
VALUES
	('John', NULL, 'Barry'),
    ('Michael', NULL, 'Ethington'),
    ('Alan', NULL, 'Menken'),
    ('Jack', NULL, 'Feldman'),
    (NULL, NULL, 'Sibelius'),
    ('Sally', NULL, 'Deford'),
    ('William', NULL, 'Creamer'),
    ("J.", "O.R.", "J."),
    ("John", NULL, "Wyeth"),
    ("Amy", "L.", "Potter");

INSERT INTO composer
VALUES    
    (DEFAULT, 'Johann', 'Sebastian', 'Bach', 1685, 1750, 'Germany'),
	(DEFAULT, 'Billy', NULL, 'Joel', NULL, NULL, 'USA'),
    (DEFAULT, "George", "Frideric", "Handel", 1685, 1759, "Germany");
    
UPDATE composer
SET country = 'United States'
WHERE composer_id = 12;

INSERT INTO instrument
VALUES
	(DEFAULT, 'Piano', 'Keyboard'),
    (DEFAULT, 'Voice', NULL),
    (DEFAULT, 'Clarinet', 'Woodwind'),
    (DEFAULT, "Flute", "Woodwind"),
    (DEFAULT, "Violin", "String");

INSERT INTO song
VALUES
	(DEFAULT, 'The John Dunbar Theme', 'Intermediate', 'Book', 1990, NULL, '978-1-4803-6652-7', 4),
    (DEFAULT, 'Lead, Kindly Light', 'Advanced', 'Book', 2010, NULL, '6-54367-28305-9', 1),
    (DEFAULT, 'Santa Fe', 'Intermediate', 'Book', 1992, NULL, '0-7935-1473-8', 4),
    (DEFAULT, 'Toccata and Fugue in D Minor','Advanced', 'Book', 2012, NULL, '978-1-4584-2314-6', 2),
    (DEFAULT, 'Valse Triste', 'Intermediate', 'Book', 1939, NULL, '978-0-8256-2028-7', 3),
    (DEFAULT, 'Because He Lives', 'Advanced', 'Sheet', 2000, NULL, NULL, 1),
    (DEFAULT, 'Vienna', 'Intermediate','Digital', NULL, 'https://musescore.com/user/26982750/scores/5667313', NULL, 5),
    (DEFAULT, "How To Train Your Dragon Medley 2", "Intermediate", "Digital", NULL, "https://musescore.com/user/28095304/scores/6475713", NULL, 4),
    (DEFAULT, "Come Thou Fount of Every Blessing", "Intermediate", "Sheet", 2010, NULL, NULL, 1),
    (DEFAULT, "The Arrival of the Queen of Sheba", "Advanced", "Book", 2012, NULL, "978-1-4584-2314-6", 2);
    
INSERT INTO song_composer
VALUES
	(1, 1),
    (2, 2),
    (3, 3),
    (3, 4),
    (4, 11),
    (5, 5),
    (6, 6),
    (7, 12),
    (7, 7),
    (8, 8),
	(9, 9),
    (9, 10),
    (10, 13);
   
INSERT INTO song_instrument
VALUES
	(1, 1, 'Solo'),
    (2, 1, 'Solo'),
    (3, 1, 'Accompaniment'),
    (3, 2, 'Solo'),
    (4, 1, 'Solo'),
    (5, 1, 'Accompaniment'),
    (5, 3, 'Solo'),
    (6, 1, 'Accompaniment'),
    (6, 2, 'Choir'),
    (7, 1, 'Accompaniment'),
    (7, 2, 'Solo'),
    (8, 3, "Duet"),
    (8, 4, "Duet"),
    (9, 1, "Accompaniment"),
    (9, 5, "Solo"),
    (10, 1, "Solo");
	
