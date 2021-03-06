USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[sp_SignUp]    Script Date: 1/21/2022 2:48:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_SignUp]       
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
		  
    Insert into UserRegistration        
    Values (@Name,@Email,@Password,@Phone)  
		 
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