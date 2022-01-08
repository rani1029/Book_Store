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

Alter procedure spLoginDetails  
(
@EmailId VARCHAR(50),
@Password VARCHAR(20),
@result int output
)   
as
Begin
	Begin Try
	begin
	if exists(select * from UserRegistration where Email= @EmailId AND Password=@Password)
	SELECT Email, Password
	FROM UserRegistration;
		 set @result=1;
	end
	
	End Try
	BEGIN CATCH
	Select ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End