USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[spGetCart]    Script Date: 1/19/2022 3:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetCart]
	@UserId INT
AS
BEGIN
	Begin Try
	Begin Transaction
	IF EXISTS(SELECT * FROM Cart WHERE UserId=@UserId)
	  begin                 
   select Books.BookId,Books.BookName,Books.AuthorName,Books.[BookDescription],Books.Price,Books.OriginalPrice,Books.Rating,Books.RatingCount,Books.BookCount,Books.Image,
   Cart.CartId,Cart.BookId,Cart.UserId,Cart.BookQuantity
		FROM Books
		inner join Cart
		on Cart.BookId=Books.BookId where Cart.UserId=@UserId
	COMMIT TRANSACTION;
	end
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END