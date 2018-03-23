DECLARE @TABEXISTS INT = 0

declare @WsID INT 
declare @ArtifactID INT 
declare @ArtifactIDChild INT

SELECT @TABEXISTS = COUNT(*)
FROM [EDDSDBO].[Tab]  WITH (NOLOCK)
WHERE [Name] = 'Real Budget'

IF (@TABEXISTS = 0)
BEGIN
	SET @WsID = (Select TOP 1 ArtifactID From [EDDSDBO].[Artifact](nolock)  where ArtifactTypeID = 1)

	INSERT INTO EDDSDBO.Artifact (ArtifactTypeID, ParentArtifactID, AccessControlListID, AccessControlListIsInherited, CreatedOn, LastModifiedOn, LastModifiedBy, CreatedBy,
	ContainerID, Keywords, Notes, DeleteFlag, TextIdentifier)
	VALUES(23, @WsID ,1,1, GETDATE(), GETDATE(), 777, 777 ,@WsID, '', '', 0, 'Real Budget');

	SET @ArtifactID = (SELECT CAST(scope_identity() AS int))

	INSERT INTO EDDSDBO.[Tab] (ArtifactID, Name, DisplayOrder, ExternalLink, IsDefault, Visible) 
	values(@ArtifactID,'Real Budget', 0, '%applicationPath%/CustomPages/c4ef15b9-516f-4c2d-9566-ea1fc2a617bd/',0, 1)

	INSERT INTO EDDSDBO.[GroupTab] VALUES(20, @ArtifactID)

	INSERT INTO [EDDSDBO].[ArtifactAncestry] VALUES(@ArtifactID, @WsID)

END
