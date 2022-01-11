ALTER PROCEDURE [dbo].[Sp_ForgetPassword]
(
@EmailId varchar(255),
@result int output
)
AS

BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		If exists(Select * from [dbo].[UserRegistration] where Email=@EmailId)
	    begin
		  select Email from UserRegistration 
		 WHERE Email=@EmailId;
		 set @result=1;
		  end 
		  else
		  begin
		   set @result=0;
		  end
		
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