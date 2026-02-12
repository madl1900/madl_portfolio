DROP SCHEMA sheet_music;

CREATE SCHEMA sheet_music;

USE sheet_music;

CREATE TABLE publisher (
	publisher_id INT NOT NULL AUTO_INCREMENT,
    publisher_name VARCHAR(40) NOT NULL,
    city VARCHAR(30),
    state CHAR(2),
    PRIMARY KEY (publisher_id)
);

CREATE TABLE book (
	ISBN VARCHAR(20) NOT NULL,
    book_name VARCHAR(70) NOT NULL,
    publishing_year YEAR NOT NULL,
    publisher_id INT NOT NULL,
    PRIMARY KEY (ISBN),
    FOREIGN KEY (publisher_id) REFERENCES publisher(publisher_id)
);

CREATE TABLE genre (
	genre_id INT NOT NULL AUTO_INCREMENT,
    genre_name VARCHAR(25) NOT NULL,
    PRIMARY KEY (genre_id)
);

CREATE TABLE composer (
	composer_id INT NOT NULL AUTO_INCREMENT,
    fname VARCHAR(30),
    mname VARCHAR(30),
    lname VARCHAR(30) NOT NULL,
    birth_year INT,
    death_year INT,
    country VARCHAR(30),
    PRIMARY KEY (composer_id)
);

CREATE TABLE instrument (
	instrument_id INT NOT NULL AUTO_INCREMENT,
    instrument_name VARCHAR(30) NOT NULL,
    classification ENUM('Woodwind', 'Brass', 'String', 'Percussion', 'Keyboard'),
    PRIMARY KEY (instrument_id)
);

CREATE TABLE song (
	song_id INT NOT NULL AUTO_INCREMENT,
    song_title VARCHAR(40) NOT NULL,
    difficulty ENUM('Beginner','Intermediate', 'Advanced'),
    song_format ENUM('Book', 'Digital', 'Sheet') NOT NULL,
    copyright_year YEAR,
    weblink VARCHAR(70),
    ISBN VARCHAR(20),
    genre_id INT NOT NULL,
    PRIMARY KEY (song_id),
    FOREIGN KEY (ISBN) REFERENCES book(ISBN),
    FOREIGN KEY (genre_id) REFERENCES genre(genre_id)
);

ALTER TABLE song
MODIFY COLUMN copyright_year INT;

CREATE TABLE song_composer (
	song_id INT NOT NULL,
    composer_id INT NOT NULL,
    PRIMARY KEY (song_id, composer_id),
    FOREIGN KEY (song_id) REFERENCES song(song_id),
    FOREIGN KEY (composer_id) REFERENCES composer(composer_id)
);

CREATE TABLE song_instrument (
	song_id INT NOT NULL,
    instrument_id INT NOT NULL,
    song_type ENUM('Solo', 'Duet', 'Trio', 'Accompaniment', 'Choir'),
    PRIMARY KEY (song_id, instrument_id),
    FOREIGN KEY (song_id) REFERENCES song(song_id),
    FOREIGN KEY (instrument_id) REFERENCES instrument(instrument_id)
);