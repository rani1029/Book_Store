Create PROCEDURE Sp_UpdateAddress
(
@AddressId int,
@Address varchar(200),
@City varchar(100),
@State varchar(100),
@TypeId int	)

AS
BEGIN
 BEGIN TRY 
    BEGIN TRANSACTION;
       If (exists(Select * from Address where AddressId=@AddressId))
		begin
			UPDATE Address
			SET 
			Address= @Address, 
			City = @City,
			State=@State,
			AddressTypeId=@TypeId 
				WHERE AddressId=@AddressID;
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
