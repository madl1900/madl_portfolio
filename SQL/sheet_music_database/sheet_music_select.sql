USE sheet_music;

SELECT publisher_name, city 
FROM publisher 
ORDER BY publisher_name;

SELECT book_name, publishing_year 
FROM book 
ORDER BY publishing_year;

SELECT genre_name 
FROM genre 
WHERE genre_name LIKE 'C%';

SELECT fname, lname, country 
FROM composer
WHERE country IS NOT NULL 
ORDER BY lname;

SELECT instrument_name 
FROM instrument 
WHERE classification = 'Woodwind';

SELECT song_title, difficulty 
FROM song 
WHERE song_format = 'Book' 
ORDER BY song_title;

SELECT song_id 
FROM song_composer 
WHERE composer_id = 11; -- Johann Sebastion Bach

SELECT song_id, song_type 
FROM song_instrument 
WHERE instrument_id = 1 
ORDER BY song_type;
