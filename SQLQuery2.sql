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

SET IDENTITY_INSERT [Comment] ON
INSERT INTO [Comment] ([ID], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES (1, 1, 1, 'Great Post!', 'This is so relatable. So much great content!', SYSDATETIME());
SET IDENTITY_INSERT [Comment] OFF