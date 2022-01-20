create table FeedbackTable
(
         FeedbackId int not null identity (1,1) primary key,
		 UserId INT NOT NULL
		 FOREIGN KEY  REFERENCES [dbo].[UserRegistration](UserId),
	     BookId INT NOT NULL
		 FOREIGN KEY  REFERENCES Books(BookId),
		 Comments Varchar(max),
		 Ratings int		 
);

Create procedure Sp_AddFeedback(
	@UserId INT,
	@BookId INT,
	@Comments Varchar(250),
	@Ratings int)		
As 
Declare @AverageRating int;
Begin
	IF (EXISTS(SELECT * FROM FeedbackTable WHERE BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		IF (EXISTS(SELECT * FROM [dbo].[Books] WHERE BookId = @BookId))
		Begin
			Begin try
				Begin transaction
					Insert into FeedbackTable (UserId,BookId,Comments,Ratings )
						values (@UserId,@BookId,@Comments,@Ratings);		
					select @AverageRating=AVG(Ratings) from FeedbackTable where BookId = @BookId;
					Update Books set Rating=@AverageRating, RatingCount=RatingCount+1 where BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
End
