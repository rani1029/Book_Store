Create PROCEDURE Sp_AddAddress (
@Address varchar(200),
@City varchar(50),
@State varchar(50),
@TypeId int,
@UserId int )

AS
 BEGIN
  BEGIN TRY
      IF (EXISTS(SELECT * FROM UserRegistration WHERE UserId = @UserId))
	Begin
	Insert into Address (UserId,Address,City,State,AddressTypeId )
		values (@UserId,@Address,@City,@State,@TypeId);
	End
  END TRY

  BEGIN CATCH
  END CATCH
 END;
 
