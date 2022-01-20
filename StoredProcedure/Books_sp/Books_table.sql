
create table Books
(
	BookId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BookName VARCHAR(255) NOT NULL,
	AuthorName VARCHAR(255) NOT NULL,
	BookDescription VARCHAR(255) NOT NULL,
	Image VARCHAR(255) NOT NULL,
	BookCount INT NOT NULL,
	Price INT NOT NULL,
	OriginalPrice INT NOT NULL,
	Rating INT,
	RatingCount INT
);

