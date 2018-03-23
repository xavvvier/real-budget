
declare @GlobalUserPrice money
SELECT TOP 1 @GlobalUserPrice = TRY_CAST(Value AS money) 
FROM eddsdbo.InstanceSetting WITH (NOLOCK)
WHERE Name='DefaultUserPrice' and Section = 'RealBudget'

SELECT EU.ArtifactID, EU.FullName as UserName, COALESCE(UP.PricePerHour, @GlobalUserPrice, 0) as PricePerHour
FROM ExtendedUser EU WITH (NOLOCK)
LEFT JOIN eddsdbo.UserPrice UP WITH (NOLOCK) ON EU.ArtifactID = UP.UserID
