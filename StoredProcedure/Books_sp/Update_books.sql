Alter PROC spUpdateBook(
    @BookId INT,
	@BookName VARCHAR(255),
	@AuthorName VARCHAR(255),
	@BookDescription VARCHAR(255),
	@BookImage VARCHAR(255),
	@Quantity INT,
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
			BookName = CASE WHEN @BookName='' THEN BookName ELSE @BookName END,
			AuthorName = CASE WHEN @AuthorName='' THEN AuthorName ELSE @AuthorName END, 
			BookDescription= CASE WHEN @BookDescription='' THEN BookDescription ELSE @BookDescription END, 
			Image =CASE WHEN @BookImage='' THEN Image ELSE @BookImage END, 
			BookCount = @Quantity, 
			Price = CASE WHEN @Price='0' THEN Price ELSE @Price END, 
			OriginalPrice = CASE WHEN @OriginalPrice='0' THEN OriginalPrice ELSE @OriginalPrice END
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