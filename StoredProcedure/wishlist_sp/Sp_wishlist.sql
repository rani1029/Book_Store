Create Table  Wishlist(

WishlistId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BookId INT NOT NULL foreign key references books(BookId),
	UserId INT NOT NULL foreign key references [dbo].[UserRegistration]([UserId])
	)

	------------------------------------------------------------------------
Create PROCEDURE SpAddToWishlist
	@UserId INT,
	@BookId INT
AS
BEGIN 
	IF EXISTS(SELECT * FROM [dbo].[Wishlist]WHERE BookId = @BookId AND UserId = @UserId)
		SELECT 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM Books WHERE BookId = @BookId)
		BEGIN
			INSERT INTO Wishlist(UserId,BookId)
			VALUES ( @UserId,@BookId)
		END
		ELSE
			SELECT 2;
	END
END

---------------------------------------------------------------------------------
CREATE PROCEDURE SpDeleteFromWishlist
	@WishlistId INT
AS
BEGIN
		DELETE FROM Wishlist WHERE WishlistId = @WishlistId
END

------------------------------------------------------------------------------
--  To Retreive All Books
Alter procedure SpGetBooksFromWishList                                          
(            
    @UserId varchar(50)        
) 
as           
    
BEGIN TRY                     
   select Books.BookId,Books.BookName,Books.AuthorName,Books.[BookDescription],Books.Price,Books.OriginalPrice,Books.Rating,Books.RatingCount,Books.BookCount,Books.Image,Wishlist.WishlistId,Wishlist.UserId,Wishlist.BookId
		FROM Books
		inner join Wishlist
		on Wishlist.BookId=Books.BookId where Wishlist.UserId=@UserId

END TRY 
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH          
go