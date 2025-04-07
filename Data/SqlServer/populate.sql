-- Insert 10,000 sample records
USE SignalR;

DELETE FROM dbo.SAMPLE_MESSAGES;

DECLARE @Counter INT = 1;

WHILE @Counter <= 10000
    BEGIN
        INSERT INTO dbo.SAMPLE_MESSAGES (MESSAGE_ID, MESSAGE_DATE)
        VALUES (NEWID(), DATEADD(MINUTE, -@Counter, GETDATE()));
        SET @Counter = @Counter + 1;
    END;
GO