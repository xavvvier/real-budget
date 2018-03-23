
	select [User], isnull(Sum(Seconds), 0) as TotalSeconds
	from eddsdbo.ActivityLog AL WITH(NOLOCK)
	group by [User]

