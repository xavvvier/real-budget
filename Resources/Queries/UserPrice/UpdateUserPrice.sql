
IF EXISTS (SELECT TOP 1 1 FROM eddsdbo.UserPrice WHERE UserID = @UserId)
BEGIN
    UPDATE eddsdbo.UserPrice SET PricePerHour = @PricePerHour WHERE UserID = @UserId
END
ELSE
BEGIN
   INSERT eddsdbo.UserPrice(UserID, PricePerHour) 
   VALUES (@UserId, @PricePerHour)
END
