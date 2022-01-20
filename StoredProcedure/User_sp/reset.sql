alter PROCEDURE Sp_ResetPassword
(
@EmailId varchar(255),
@NewPassword varchar(50),
@result int output
)
AS
BEGIN

       If exists(Select * from [dbo].[UserRegistration] where Email=@EmailId)
	    begin
		  UPDATE UserRegistration
          SET 
		   Password=@NewPassword
		 WHERE Email=@EmailId;
		 set @result=1;
		  end 
		  else
		  begin
		   set @result=0;
		  end
END 