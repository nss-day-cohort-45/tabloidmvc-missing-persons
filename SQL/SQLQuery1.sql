USE [TabloidMVC]
GO
SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
	[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
VALUES (2, 'Turtle', 'Author', 'Turt', 'turt@example.com', SYSDATETIME(), NULL, 2);
SET IDENTITY_INSERT [UserProfile] OFF

SELECT 
    c.Id AS CommentId, 
    c.Subject, 
    c.Content, 
    c.CreateDateTime,
    p.Id AS PostId, p.Title,
    u.Id AS UserId, u.DisplayName
FROM Comment c
    LEFT JOIN Post p ON c.PostId = p.id
    LEFT JOIN UserProfile u ON c.UserProfileId = u.id;

USE [TabloidMVC]
GO
SET IDENTITY_INSERT [Comment] ON
INSERT INTO [Comment] ([ID], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES (5, 1, 2, 'This is a new comment!', 'To see if it shows in all of the comments', SYSDATETIME());
SET IDENTITY_INSERT [Comment] OFF