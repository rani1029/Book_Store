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