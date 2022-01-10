select * from Books
CREATE PROC spGetParticularBook
	@BookId INT
AS
BEGIN 
	SELECT * FROM [Books]
	WHERE BookId = @BookId
END