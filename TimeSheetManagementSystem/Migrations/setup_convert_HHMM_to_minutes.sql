CREATE FUNCTION [dbo].[ConvertHHMMToMinutes] (@inHHMM varchar(10))
  RETURNS INT
AS BEGIN
  DECLARE @calculatedMinutes INT
  DECLARE @time DATETIME = @inHHMM
  SET @calculatedMinutes =  ((DATEPART(HOUR, @time)*60) + (DATEPART(MINUTE, @time)))
  RETURN @calculatedMinutes
END


