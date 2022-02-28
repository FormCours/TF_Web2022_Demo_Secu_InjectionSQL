CREATE TABLE [dbo].[Member]
(
	[IdMember] INT NOT NULL IDENTITY,
	[Pseudo] VARCHAR(50) NOT NULL,
	[Password] VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Member PRIMARY KEY([IdMember]),
	CONSTRAINT UK_Member_Pseudo UNIQUE([Pseudo]) 
);