/*

CREATE DATABASE FOR CITYINFO


*/


CREATE DATABASE CityInfo;
GO


USE CityInfo;
GO


CREATE TABLE [City](
	[CityId] int not null Identity,
	[CityName] varchar(20) not null,
	[CityDescription] varchar(500),
	
	CONSTRAINT [Pk_City] PRIMARY KEY ([CityId])
	
);
GO


CREATE TABLE [PointOfInterest](
	[PointId] int not null Identity,
	[PointName] varchar(20) not null,
	[PointDescription] varchar(500),
	[PointWikipediaLink] varchar(500),
	[CityId] int not null,
	
	CONSTRAINT [PK_PointOfInterest] PRIMARY KEY ([PointId]),
	CONSTRAINT [Fk_PointOfInterest_City_CityId] FOREIGN KEY ([CityId]) REFERENCES [City]([CityId]) ON DELETE CASCADE
);
GO