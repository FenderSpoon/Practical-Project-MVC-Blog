﻿CREATE TABLE [dbo].[Table]
(
	[ImageId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ImageSize] INT NOT NULL, 
    [FileName] NVARCHAR(200) NOT NULL, 
    [ImageData] VARBINARY(MAX) NOT NULL
)
