---user table

Create table UserRegistration
(
UserId int IDENTITY(1,1) NOT NULL primary key,      
   Name Varchar(20) NOT NULL,   
   Email varchar(100) not null,    
   Password Varchar(20) NOT NULL,
   PhoneNumber bigint )       
  
  select * from UserRegistration


-----add customer stored procedure

Alter procedure sp_AddCustomer       
(      
    @Name VARCHAR(20),    
    @Email varchar(100),     
    @Password VARCHAR(200),      
    @Phone bigint  
)      
As       
Begin  
Declare @Encrypt varbinary(200)
Select @Encrypt = EncryptByPassPhrase('key', @Password )  
    Insert into UserRegistration        
    Values (@Name,@Email,@Encrypt,@Phone)  
	      
End  

---------login stored procedure

 create procedure spLogin
(
@EmailId VARCHAR(50),
@Password VARCHAR(20),
@User int = Null OUTPUT
)
as
Begin
	Begin Try
		IF EXISTS(SELECT * FROM UserRegistration WHERE Email=@EmailId)
		BEGIN
		Declare @Encrypt varbinary(200)
           Select @Encrypt = EncryptByPassPhrase('key', @Password )  

			IF EXISTS(SELECT * FROM UserRegistration WHERE Email=@EmailId AND
			 Password=@Encrypt)
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