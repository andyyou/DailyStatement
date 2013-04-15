
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AndyYou
-- Create date: 2013/04/15
-- Description:	The DailyStatement system that employee's daily records need 'WINTRISS' record every day.
--				so, system will help everyone create a record everyday and alert user need to modify.
-- =============================================
IF OBJECT_ID('insert_wintriss_everyday') IS NOT NULL
	DROP Procedure insert_wintriss_everyday
GO
CREATE PROCEDURE insert_wintriss_everyday
AS
	Declare @empid int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert Into [dbo].[DailyInfoes]([WorkCategoryId],[Project_ProjectId],[WorkContent],[CreateDate],[WorkingHours]) 
Values (10,2,'N/A',(getdate()),0);
	SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
END
GO
