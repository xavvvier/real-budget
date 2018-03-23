SELECT @TABEXISTS = COUNT(*)
FROM [EDDSDBO].[Tab]  WITH (NOLOCK)
WHERE ExternalLink like '%/c4ef15b9-516f-4c2d-9566-ea1fc2a617bd/%' AND [Name] = 'Real Budget'

IF (@TABEXISTS = 0)
BEGIN
	SET @WsID = (Select TOP 1 ArtifactID From [EDDSDBO].[Artifact](nolock)  where ArtifactTypeID = 1)

	INSERT INTO EDDSDBO.Artifact (ArtifactTypeID, ParentArtifactID, AccessControlListID, AccessControlListIsInherited, CreatedOn, LastModifiedOn, LastModifiedBy, CreatedBy,
	ContainerID, Keywords, Notes, DeleteFlag, TextIdentifier)
	VALUES(23, @WsID ,1,1, GETDATE(), GETDATE(), 777, 777 ,@WsID, '', '', 0, 'Real Budget');

	SET @ArtifactIDChild = (SELECT CAST(scope_identity() AS int))
	
	INSERT INTO EDDSDBO.[Tab] (ArtifactID, Name, DisplayOrder, ExternalLink, IsDefault, Visible) 
	values(@ArtifactIDChild,'Real Budget', 0,'%applicationPath%/CustomPages/c4ef15b9-516f-4c2d-9566-ea1fc2a617bd/',0, 1)

	INSERT INTO EDDSDBO.[GroupTab] VALUES(20, @ArtifactIDChild)

	INSERT INTO [EDDSDBO].[ArtifactAncestry] VALUES(@ArtifactIDChild, @WsID)

	INSERT INTO [EDDSDBO].[ArtifactAncestry] VALUES(@ArtifactIDChild, 20)

	UPDATE Artifact SET ParentArtifactID = @ArtifactID WHERE ArtifactID = @ArtifactIDChild
END

