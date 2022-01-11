USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBook]    Script Date: 1/11/2022 8:22:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[spUpdateBook](
    @BookId INT,
	@BookName VARCHAR(255),
	@AuthorName VARCHAR(255),
	@BookDescription VARCHAR(255),
	@BookImage VARCHAR(255),
	@BookCount INT,
	@Price INT,
	@OriginalPrice INT,
	@book INT = NULL OUTPUT
	)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		IF EXISTS(SELECT * FROM [Books] WHERE BookId = @BookId )
	BEGIN
		SET @book = @BookId
		UPDATE [Books]
		SET
			BookName = @BookName,
			AuthorName = @AuthorName, 
			BookDescription = @BookDescription, 
			Image = @BookImage, 
			BookCount = @BookCount, 
			Price = @Price, 
			OriginalPrice = @OriginalPrice
		WHERE
			BookId = @BookId;
	END
	ELSE
	BEGIN
		SET @book =NULL;
	END
		-- if insert succeeds, commit the transaction
		COMMIT TRANSACTION;  
	END TRY
	BEGIN CATCH
		-- report exception----
		
		-- Test if the transaction is uncommittable.  
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
END; 