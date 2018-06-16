CREATE FUNCTION [dbo].[ConvertMinutesToHHMM]
(
    @inMinutes int 
)
RETURNS nvarchar(30)

AS
BEGIN
declare @hhmm nvarchar(20)

SET @hhmm = 
    CASE WHEN @inMinutes >= 60 THEN
        (SELECT FORMAT((@inMinutes / 60) , '00')+  ':'  + 
                CASE WHEN (@inMinutes % 60) > 0 THEN
                    FORMAT(@inMinutes % 60,'00') 
                ELSE
                    '00'
                END)
    ELSE 
        FORMAT(@inMinutes % 60,'00' )
    END

return @hhmm
END


