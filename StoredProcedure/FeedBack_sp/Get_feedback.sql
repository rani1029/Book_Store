Create PROC Sp_GetFeedback
	@BookId INT
AS
BEGIN
	select 
		FeedbackTable.UserId,FeedbackTable.Comments,FeedbackTable.Ratings,UserRegistration.Name
		FROM [dbo].[UserRegistration]
		inner join FeedbackTable
		on FeedbackTable.UserId=[dbo].[UserRegistration].UserId
		where BookId=@BookId
END