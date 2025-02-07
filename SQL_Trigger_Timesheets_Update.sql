USE [MiniProjDb]
GO
/****** Object:  Trigger [dbo].[trg_Timesheets_Update]    Script Date: 6/29/2024 2:43:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER TRIGGER [dbo].[trg_Timesheets_Update]
   ON  [dbo].[Timesheets] 
   AFTER UPDATE
AS 
BEGIN
	IF EXISTS (SELECT 1 FROM inserted i INNER JOIN deleted d ON i.TimesheetID = d.TimesheetID WHERE i.Status <> d.Status)
	BEGIN
        INSERT INTO DataAudit (DataId, LastData, CurrentData, Status, [Table], [Column], Created)
        SELECT 
            d.TimesheetID,
            d.Status,
            i.Status,
            'UPDATE',
            'Timesheets',
            'Status',
            GETDATE()
        FROM 
            inserted i
            INNER JOIN deleted d ON i.TimesheetID = d.TimesheetID
        WHERE 
            i.Status <> d.Status;
    END;
END;
