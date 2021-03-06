﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Queries {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Sql {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Sql() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Queries.Sql", typeof(Sql).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @TABEXISTS INT = 0
        ///
        ///declare @WsID INT 
        ///declare @ArtifactID INT 
        ///declare @ArtifactIDChild INT
        ///
        ///SELECT @TABEXISTS = COUNT(*)
        ///FROM [EDDSDBO].[Tab]  WITH (NOLOCK)
        ///WHERE [Name] = &apos;Real Budget&apos;
        ///
        ///IF (@TABEXISTS = 0)
        ///BEGIN
        ///	SET @WsID = (Select TOP 1 ArtifactID From [EDDSDBO].[Artifact](nolock)  where ArtifactTypeID = 1)
        ///
        ///	INSERT INTO EDDSDBO.Artifact (ArtifactTypeID, ParentArtifactID, AccessControlListID, AccessControlListIsInherited, CreatedOn, LastModifiedOn, LastModifiedBy, CreatedBy,
        ///	Con [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateInstanceLevelTab {
            get {
                return ResourceManager.GetString("CreateInstanceLevelTab", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///IF OBJECT_ID(&apos;[eddsdbo].[UserPrice]&apos;) IS NULL
        ///BEGIN
        ///	CREATE TABLE [eddsdbo].[UserPrice](
        ///		[UserID] [int] NOT NULL,
        ///		[PricePerHour] [money] NOT NULL,
        ///	 CONSTRAINT [PK_UserPrice] PRIMARY KEY CLUSTERED 
        ///	(
        ///		[UserID] ASC
        ///	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ///	) ON [PRIMARY]
        ///END
        ///
        ///.
        /// </summary>
        internal static string CreateUserPriceTable {
            get {
                return ResourceManager.GetString("CreateUserPriceTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///declare @GlobalUserPrice money
        ///SELECT TOP 1 @GlobalUserPrice = TRY_CAST(Value AS money) 
        ///FROM eddsdbo.InstanceSetting WITH (NOLOCK)
        ///WHERE Name=&apos;DefaultUserPrice&apos; and Section = &apos;RealBudget&apos;
        ///
        ///SELECT EU.ArtifactID, EU.FullName as UserName, COALESCE(UP.PricePerHour, @GlobalUserPrice, 0) as PricePerHour
        ///FROM ExtendedUser EU WITH (NOLOCK)
        ///LEFT JOIN eddsdbo.UserPrice UP WITH (NOLOCK) ON EU.ArtifactID = UP.UserID
        ///.
        /// </summary>
        internal static string GetAll {
            get {
                return ResourceManager.GetString("GetAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE EDDSDBO.InstanceSetting
        ///SET Name = &apos;GroupSyncMode&apos;
        ///WHERE Section COLLATE SQL_Latin1_General_CP1_CS_AS = &apos;kCura.UGS&apos; AND Name COLLATE SQL_Latin1_General_CP1_CS_AS = &apos;SyncType&apos;
        ///DECLARE @SettingName NVARCHAR(36) = &apos;TestCamilo&apos;
        ///IF NOT EXISTS ( SELECT 1 FROM EDDSDBO.InstanceSetting WHERE Name = @SettingName )
        ///BEGIN
        ///	INSERT INTO EDDSDBO.ARTIFACT (ArtifactTypeID, ParentArtifactID, AccessControlListID, AccessControlListIsInherited, CreatedOn, LastModifiedOn, LastModifiedBy, CreatedBy, TextIdentifier, C [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string InstanceSetting {
            get {
                return ResourceManager.GetString("InstanceSetting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @CodeTypeID int
        ///select top 1 @CodeTypeID = CodeTypeID  from 
        ///eddsdbo.ArtifactGuid WITH(NOLOCK)
        ///JOIN eddsdbo.Field WITH(NOLOCK) ON ArtifactGuid.ArtifactID = Field.ArtifactID
        ///where ArtifactGuid = &apos;75865E6D-E5DF-46BC-AD26-1479B87EFE13&apos;
        ///
        ///DECLARE @SQL NVARCHAR(MAX) = N&apos;
        ///;WITH CTE AS(
        ///	select [User], AL.Document, ArtifactGuid, Seconds
        ///	from eddsdbo.ActivityLog AL WITH(NOLOCK)
        ///	JOIN ZCodeArtifact_&apos; + CAST(@CodeTypeID as nvarchar(20)) + N&apos; Z WITH(NOLOCK) ON Z.AssociatedArtifactID = AL.ArtifactID
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MetricsByWorkspace {
            get {
                return ResourceManager.GetString("MetricsByWorkspace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///	select [User], isnull(Sum(Seconds), 0) as TotalSeconds
        ///	from eddsdbo.ActivityLog AL WITH(NOLOCK)
        ///	group by [User]
        ///
        ///.
        /// </summary>
        internal static string TotalTimeByUSer {
            get {
                return ResourceManager.GetString("TotalTimeByUSer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///IF EXISTS (SELECT TOP 1 1 FROM eddsdbo.UserPrice WHERE UserID = @UserId)
        ///BEGIN
        ///    UPDATE eddsdbo.UserPrice SET PricePerHour = @PricePerHour WHERE UserID = @UserId
        ///END
        ///ELSE
        ///BEGIN
        ///   INSERT eddsdbo.UserPrice(UserID, PricePerHour) 
        ///   VALUES (@UserId, @PricePerHour)
        ///END
        ///.
        /// </summary>
        internal static string UpdateUserPrice {
            get {
                return ResourceManager.GetString("UpdateUserPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///DECLARE @AID INT = 0
        ///
        ///SELECT @AID = ArtifactID
        ///FROM EDDSDBO.ArtifactGuid WITH (NOLOCK)
        ///WHERE ArtifactGuid = @ApplicationGuid
        ///
        ///SELECT C.ArtifactID, C.[Name] FROM EDDSDBO.CaseApplication CA WITH (NOLOCK)
        ///INNER JOIN EDDSDBO.[CASE] C WITH (NOLOCK) ON CA.CaseID = C.ArtifactID
        ///WHERE ApplicationID = @AID.
        /// </summary>
        internal static string WorkspacesWithApp {
            get {
                return ResourceManager.GetString("WorkspacesWithApp", resourceCulture);
            }
        }
    }
}
