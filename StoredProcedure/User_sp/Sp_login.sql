USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 1/21/2022 2:20:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spLogin]
(
@EmailId VARCHAR(50),
@Password VARCHAR(250),
@User int = Null OUTPUT
)
as
Begin
	Begin Try
		IF EXISTS(SELECT * FROM UserRegistration WHERE Email=@EmailId)
		BEGIN
		
		IF EXISTS(SELECT * FROM UserRegistration WHERE
			 Password=@Password)
											 
			BEGIN
				SET @User = 2;
			END
			ELSE
			BEGIN
				SET @User = 1;
			END
		END
		ELSE
		BEGIN
			SET @User = 0;
		END
	End Try
	BEGIN CATCH
	Select ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End