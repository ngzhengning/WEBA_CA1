CREATE TRIGGER [dbo].[AspNetUsers_Insert]
       ON [dbo].[AspNetUsers]
       AFTER INSERT
AS
BEGIN
       DECLARE @loginUserName varchar(10)
	   DECLARE @fullName varchar(100)
	   DECLARE @email varchar(50)
       SELECT @loginUserName = UserName FROM inserted
	   SELECT @email = Email FROM inserted
	   SELECT @fullName = FullName FROM inserted
       IF (EXISTS(SELECT LoginUserName FROM UserInfo
                            WHERE LoginUserName = @loginUserName))
              BEGIN
                     UPDATE UserInfo
                     SET UserInfo.FullName = inserted.FullName
					 FROM inserted
                     WHERE LoginUserName = @loginUserName
              END

       IF (NOT EXISTS(SELECT loginUserName FROM UserInfo
                            WHERE LoginUserName = @loginUserName))
              BEGIN
                     INSERT INTO UserInfo
                     (FullName, LoginUserName, Email)
					 
                     VALUES (@fullName, @loginUserName, @email)
              END
END





