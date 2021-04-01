USE [TabloidMVC]
GO
SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
	[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
VALUES (2, 'Turtle', 'Author', 'Turt', 'turt@example.com', SYSDATETIME(), NULL, 2);
SET IDENTITY_INSERT [UserProfile] OFF