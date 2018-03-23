UPDATE EDDSDBO.InstanceSetting
SET Name = 'GroupSyncMode'
WHERE Section COLLATE SQL_Latin1_General_CP1_CS_AS = 'kCura.UGS' AND Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'SyncType'
DECLARE @SettingName NVARCHAR(36) = 'TestCamilo'
IF NOT EXISTS ( SELECT 1 FROM EDDSDBO.InstanceSetting WHERE Name = @SettingName )
BEGIN
	INSERT INTO EDDSDBO.ARTIFACT (ArtifactTypeID, ParentArtifactID, AccessControlListID, AccessControlListIsInherited, CreatedOn, LastModifiedOn, LastModifiedBy, CreatedBy, TextIdentifier, ContainerID, Keywords, Notes, DeleteFlag)
	VALUES (42,62,1,1,GETUTCDATE(),GETUTCDATE(),9,9,@SettingName,62,'','',0)
	DECLARE @AID INT = SCOPE_IDENTITY()
	INSERT INTO EDDSDBO.InstanceSetting (Section, Name, MachineName, Value, Description, InitialValue, ArtifactID)
	VALUES ('Real.Budget', @SettingName, '', 0, 'Useful for Real Buget', '', @AID)
END