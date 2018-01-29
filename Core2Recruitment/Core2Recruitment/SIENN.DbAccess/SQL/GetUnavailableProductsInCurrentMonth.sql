SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE GetUnavailableProductsInCurrentMonth
AS
BEGIN
	SET NOCOUNT ON;

    SELECT P.Id, P.Code, P.Description, P.DeliveryDate, P.IsAvailable, P.Price, P.TypeId, P.UnitId
	FROM Products P
		LEFT JOIN Types T
			ON T.Id = P.TypeId
		LEFT JOIN Units U
			ON U.Id = P.UnitId
	WHERE IsAvailable = 0 AND MONTH(DeliveryDate)=MONTH(GETDATE())
END
GO
