CREATE PROC Sp_UpdateQuantity
	@CartId int,
	@BookQuantity int
AS
BEGIN
	Begin Try
	Begin Transaction
	IF (EXISTS(SELECT * FROM Cart WHERE CartId = @CartId))
		BEGIN
			UPDATE Cart
			SET BookQuantity = @BookQuantity
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