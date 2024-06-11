-- SQLite
-- select * from Clients
-- SELECT 
--     c.Id, 
--     c.Name, 
--     COALESCE(SUM(co.Amount), 0) AS TotalContribution
-- FROM 
--     Clients c
-- LEFT JOIN 
--     Contributions co ON c.Id = co.ClientId
-- -- WHERE 
-- --     c.Id = 1
-- GROUP BY 
--     c.Id
-- select * from Contributions;

-- SELECT *
-- FROM Clients c 
-- LEFT JOIN Contributions co ON c.Id = co.ClientId
-- -- GROUP BY c.Id, c.Name
-- order by c.Id

-- SELECT 
--             c.Id,
--             c.Name,
--             SUM(co.Amount) AS TotalContribution,
--             GROUP_CONCAT(co.Id) AS ContributionIds
--         FROM 
--             Clients c
--         LEFT JOIN 
--             Contributions co ON c.Id = co.ClientId
--         -- WHERE 
--         --     c.Id = @ClientId
--         GROUP BY 
--             c.Id, c.Name

 SELECT 
    c.Id,
    c.Name,
    COALESCE(SUM(co.Amount), 0) AS TotalContribution,
    CASE WHEN COUNT(co.Id) > 0 THEN 
        JSON_GROUP_ARRAY(JSON_OBJECT('Id', co.Id, 'Amount', co.Amount, 'ClientId', co.ClientId, 'DateCreated', co.DateCreated))
    ELSE 
        -- JSON_ARRAY()
        null
    END 
    AS Contributions
FROM 
    Clients c
LEFT JOIN 
    Contributions co ON c.Id = co.ClientId
GROUP BY 
    c.Id, c.Name