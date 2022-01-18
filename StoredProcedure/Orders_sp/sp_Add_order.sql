Alter procedure Sp_PlaceOrder
 @BookId int,
 @UserId int,
 @CartId int,
 @AddressId int,
 @quantity int,
 @result int=null output
 AS
BEGIN
  BEGIN TRY
  BEGIN TRANSACTION;
	     begin
		 IF EXISTS( select * from Cart where CartId=@CartId)
		 BEGIN
		 DECLARE @unitPrice INT;
			DECLARE @availableStock INT;
			SELECT @unitPrice = Price, @availableStock = [BookCount] FROM [Books] WHERE BookId = @BookId;

		   insert into Orders (BookId,UserId,AddressId,CartId,OrderValue,BookQuantity)
		                    values (@BookId,@UserId,@AddressId,@CartId,@unitPrice*@quantity,@quantity);
							UPDATE [Books]
			SET
				[BookCount] = (@availableStock- @quantity)
			WHERE
				BookId = @BookId;
				DELETE FROM [CART] WHERE BookId = @BookId AND UserId = @UserId;
		   set @result=1
   Commit Transaction
    		 
	 END
		  ELSE
		    begin
			SET @result = 2;
			end
		 END 		
  END TRY
  begin catch
      set @result=0;
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
 END catch
 END