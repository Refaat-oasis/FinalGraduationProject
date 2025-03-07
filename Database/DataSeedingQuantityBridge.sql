SET NOCOUNT ON;

DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    INSERT INTO QuantityBridge (
        price,
        returnID,
        purchaseID,
        quantity,
        requisiteID,
        paperID,
        inkID,
        suppliesID,
        physicalCountID
    )
    VALUES (
        ROUND((RAND(CHECKSUM(NEWID())) * 100) + 1, 2),  -- Random price between 1.00 and 100.00
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 1,          -- Random returnID between 1 and 30
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 1,          -- Random purchaseID between 1 and 30
        FLOOR(RAND(CHECKSUM(NEWID())) * 100) + 1,         -- Random quantity between 1 and 100
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 1,          -- Random requisiteID between 1 and 30
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 1,          -- Random paperID between 1 and 30
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 1,          -- Random inkID between 1 and 30
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 18,         -- Random suppliesID between 18 and 47
        FLOOR(RAND(CHECKSUM(NEWID())) * 30) + 1           -- Random physicalCountID between 1 and 30
    );

    SET @i = @i + 1;
END;

SET NOCOUNT OFF;

PRINT '30 records inserted into QuantityBridge table successfully!';
