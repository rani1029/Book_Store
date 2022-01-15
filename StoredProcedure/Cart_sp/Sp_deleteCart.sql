CREATE PROC Sp_DeleteCart
	@CartId int
	
AS
BEGIN
	Begin Try
	Begin Transaction
	IF (EXISTS(SELECT * FROM Cart WHERE CartId = @CartId))
		BEGIN
			DELETE FROM Cart
			WHERE CartId = @CartId;
		END
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END