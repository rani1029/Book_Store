---SignUp stored procedure

Create procedure sp_SignUp       
(      
    @Name VARCHAR(20),    
    @Email varchar(100),     
    @Password VARCHAR(200),      
    @Phone bigint  
) 
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		Declare @Encrypt varbinary(200)
        Select @Encrypt = EncryptByPassPhrase('key', @Password )  
    Insert into UserRegistration        
    Values (@Name,@Email,@Encrypt,@Phone)  
		 
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