create PROCEDURE SpDeleteBook
  @BookId int
AS
BEGIN
 Begin try
     IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
	 begin
	   delete from Books WHERE BookId=@BookId;
   	 end
 End try
 Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;    
 End catch
End