-- Create the database
CREATE DATABASE SignalR;
GO

-- Use the newly created database
USE SignalR;
GO

-- Create the SAMPLE_MESSAGE table
CREATE TABLE SAMPLE_MESSAGES (
                                 Id INT PRIMARY KEY IDENTITY(1,1),
                                 MESSAGE_ID UNIQUEIDENTIFIER NOT NULL,
                                 MESSAGE_DATE DATETIME NOT NULL
);
GO

