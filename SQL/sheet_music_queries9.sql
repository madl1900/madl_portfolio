USE sheet_music;

-- give a row number for every song, by what book they are in
SELECT song_title, book_name,
ROW_NUMBER() OVER(PARTITION BY s.ISBN ORDER BY song_id) AS book_row_num
FROM song s
	JOIN book b
		ON s.ISBN = b.ISBN;

-- rank composers by their age, based on birth year        
SELECT fname, mname, lname, birth_year,
DENSE_RANK() OVER(ORDER BY birth_year) AS birth_order
FROM composer
WHERE birth_year IS NOT NULL;