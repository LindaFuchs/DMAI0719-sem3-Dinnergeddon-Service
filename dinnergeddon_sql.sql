/* Accounts */
CREATE TABLE [dbo].[Accounts]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(MAX) NOT NULL, 
	[Email] VARCHAR(MAX) NOT NULL,
    [PasswordHash] VARCHAR(MAX) NULL, 
    [SecurityStamp] VARCHAR(MAX) NULL,
    [EmailConfirmed] BIT DEFAULT 0 NULL
)

/* Roles */

CREATE TABLE [dbo].[Roles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

/* Roles assigned to accounts */
CREATE TABLE [dbo].[AccountRoles] (
    [AccountId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AccountRoles] PRIMARY KEY CLUSTERED ([AccountId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AccountRoles_dbo.Roles_Id] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AccountRoles_dbo.Accounts_Id] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]) ON DELETE CASCADE
);