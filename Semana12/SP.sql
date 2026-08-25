

CREATE PROCEDURE SP_ObtenerOInsertarCategoria
    @Nombre NVARCHAR(15),
    @Descripcion NVARCHAR(100)
AS
BEGIN
    DECLARE @IdResultado INT;

    IF EXISTS (SELECT 1 FROM Categories WHERE CategoryName = @Nombre)
    BEGIN
        SELECT @IdResultado = CategoryID 
        FROM Categories 
        WHERE CategoryName = @Nombre;
    END
    ELSE
    BEGIN
        INSERT INTO Categories (CategoryName, Description) 
        VALUES (@Nombre, @Descripcion);
        
        SET @IdResultado = SCOPE_IDENTITY();
    END

    SELECT @IdResultado AS IDGenerado;
END
GO

select * from [dbo].[Categories]

