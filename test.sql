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

SELECT *
FROM Clients c 
LEFT JOIN Contributions co ON c.Id = co.ClientId
-- GROUP BY c.Id, c.Name
order by c.Id