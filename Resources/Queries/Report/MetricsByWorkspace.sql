DECLARE @CodeTypeID int
select top 1 @CodeTypeID = CodeTypeID  from 
eddsdbo.ArtifactGuid WITH(NOLOCK)
JOIN eddsdbo.Field WITH(NOLOCK) ON ArtifactGuid.ArtifactID = Field.ArtifactID
where ArtifactGuid = '75865E6D-E5DF-46BC-AD26-1479B87EFE13'

DECLARE @SQL NVARCHAR(MAX) = N'
;WITH CTE AS(
	select [User], AL.Document, ArtifactGuid, Seconds
	from eddsdbo.ActivityLog AL WITH(NOLOCK)
	JOIN ZCodeArtifact_' + CAST(@CodeTypeID as nvarchar(20)) + N' Z WITH(NOLOCK) ON Z.AssociatedArtifactID = AL.ArtifactID
	JOIN EDDSDBO.ArtifactGuid CG WITH(NOLOCK) ON Z.CodeArtifactID = CG.ArtifactID
	WHERE AL.DateTime BETWEEN @DATE1 AND @DATE2
), CTE_VIEWS AS
(
	select [User], COUNT(Document) Views, COUNT(DISTINCT Document) DistinctViews
	from CTE 
	where ArtifactGuid = ''169E13CC-7885-4C44-9846-DF537536DDDA''
	GRoup by [User]
), CTE_EDITS AS 
(
	select [User], COUNT(Document) Edits, COUNT(DISTINCT Document) DistinctEdits, SUM(Seconds) as Seconds
	from CTE 
	where ArtifactGuid = ''5D94419E-B513-422E-BEED-E5881747D5E1''
	GRoup by [User]
)
SELECT UA.UserID, UA.FullName as UserName, V.Views, V.DistinctViews, ISNULL(E.Edits, 0) as Edits, ISNULL(E.DistinctEdits, 0) as DistinctEdits, ISNULL(Seconds, 0) as Seconds
FROM CTE_VIEWS V
LEFT JOIN CTE_EDITS E ON V.[User] = E.[User]
JOIN EDDSDBO.AuditUser UA ON V.[User] = UA.UserID
'

EXEC SP_EXECUTESQL @SQL, N'@DATE1 smalldatetime ,@DATE2 smalldatetime', @DATE1 = @DATE1, @DATE2 = @DATE2
