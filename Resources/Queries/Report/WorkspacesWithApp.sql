
DECLARE @AID INT = 0

SELECT @AID = ArtifactID
FROM EDDSDBO.ArtifactGuid WITH (NOLOCK)
WHERE ArtifactGuid = @ApplicationGuid

SELECT C.ArtifactID, C.[Name] FROM EDDSDBO.CaseApplication CA WITH (NOLOCK)
INNER JOIN EDDSDBO.[CASE] C WITH (NOLOCK) ON CA.CaseID = C.ArtifactID
WHERE ApplicationID = @AID