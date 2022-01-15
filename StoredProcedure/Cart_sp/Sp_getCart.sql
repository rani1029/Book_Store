Alter procedure Sp_GetCartDetails                                          
(            
    @UserId varchar(50)
	        
) 
as
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
		 
		IF (XACT_STATE()) = -1  
		BEGIN  
			PRINT 'The transaction is in an uncommittable state.' +  
					'Rolling back transaction.'  
			ROLLBACK TRANSACTION;  
		END;  
        
		-- Test if the transaction is committable.  
		IF (XACT_STATE()) = 1  
		BEGIN  
			PRINT 'The transaction is committable.' +  
				'Committing transaction.'  
			COMMIT TRANSACTION;     
		END;  
	END CATCH  
	END       
go