---books table

create table Books
(
	BookId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BookName VARCHAR(255) NOT NULL,
	AuthorName VARCHAR(255) NOT NULL,
	BookDescription VARCHAR(255) NOT NULL,
	Image VARCHAR(255) NOT NULL,
	BookCount INT NOT NULL,
	Price INT NOT NULL,
	OriginalPrice INT NOT NULL,
	Rating INT,
	RatingCount INT
);


---Add book stored procedure
Alter PROC spAddBook
	@BookName VARCHAR(255),
	@AuthorName VARCHAR(255),
	@BookDescription VARCHAR(255),
	@BookImage VARCHAR(255),
	@Count INT,
	@Price INT,
	@OriginalPrice INT,
	@Rating INT,
	@RatingCount INT,
	@book INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM Books WHERE BookName = @BookName )
		SET @book=NULL;
	ELSE
		INSERT INTO [Books](BookName, AuthorName, BookDescription, Image, BookCount, Price, OriginalPrice, Rating, RatingCount)
		VALUES(@BookName, @AuthorName, @BookDescription, @BookImage, @Count, @Price, @OriginalPrice, @Rating, @RatingCount)
		SET @book = SCOPE_IDENTITY();
END
------------------------------------------------------------------------
select * from Books
CREATE PROC spGetParticularBook
	@BookId INT
AS
BEGIN 
	SELECT * FROM [Books]
	WHERE BookId = @BookId
END