create PROCEDURE Sp_ForgetPassword
(
@EmailId varchar(255),
@result int output
)
AS
BEGIN

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
END 