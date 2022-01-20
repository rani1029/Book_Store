Create PROCEDURE Sp_GetAddressesOfUser
  @UserId int
AS
BEGIN
 BEGIN TRY 
    BEGIN TRANSACTION;

	IF(EXISTS(SELECT * FROM Address WHERE UserId=@UserId))
	 begin
	   SELECT * FROM Address WHERE UserId=@UserId;
   	 end
	 else
	 begin
		select 1
	end
       
    COMMIT TRANSACTION;
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

  =======================================================

create PROCEDURE Sp_GetAllAddresses
AS
BEGIN
	 begin
	   SELECT * FROM Address;
   	 end
End


     
