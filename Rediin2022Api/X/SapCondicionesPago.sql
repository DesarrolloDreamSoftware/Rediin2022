/* ************************************************** */
/* CREACION DEL ARBOL DE PERMISOS PARA MenuId = 1101     */
/* JERARQUIA ENCONTRADA ALAAAE                          */
/* ************************************************** */
USE [RediinSisegui2022]
GO

DECLARE @vJerarquiaAdminDoc AS VARCHAR(400);
DECLARE @vJerarquiaSelCol AS VARCHAR(400);
DECLARE @vTblPermisoId TABLE (PermisoId BIGINT);
DECLARE @vTblOpcMenuId TABLE (OpcionXMenuId BIGINT);

/* Insertamos la opción del menú principal */
INSERT INTO @vTblOpcMenuId
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Condiciones de pago', 'ALAAAE', 10, '/PriCatalogos/SapCondicionesPago/SapCondicionPagoInicia', '', 'SapCondicionPagoInicia', 0;

DECLARE @vJerarquiaSapCondicionesPago AS VARCHAR(400);
SELECT @vJerarquiaSapCondicionesPago = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapCondicionPago (SapCondicionesPago) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapCondicionesPago, NULL, 'SapCondicionPagoXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapCondicionesPago, NULL, 'SapCondicionPagoInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapCondicionesPago, NULL, 'SapCondicionPagoActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapCondicionesPago, NULL, 'SapCondicionPagoElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapCondicionPago (SapCondicionesPago) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapCondicionesPago, NULL, 'SapCondicionPagoExporta', 0, 0, 1;

/* *************************************************** */
/* Agraga los permisos para el usuario administrador   */
/* *************************************************** */
INSERT INTO PTPermisosXPerfil
SELECT  1, --PerfilId
        1101, --MenuId
        p.PermisoId,
        0, --RequiereAutorizacion
        0, --PuedeAutorizar
        3, --NivelConsultaId
        1, --TipoAudicion,
        1, --UsuarioCreador,
        GETDATE(), --FechaCreacion
        1, --UsuarioUltMod,
        GETDATE() --FechaUltMod
FROM PTPermisos p
LEFT JOIN PTPermisosXPerfil pxp
    ON pxp.PerfilId = 1
       AND pxp.MenuId = p.MenuId
       AND pxp.PermisoId = p.PermisoId
WHERE   Jerarquia LIKE @vJerarquiaSapCondicionesPago + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapCondicionPago (SapCondicionesPago) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:22:53
-- Descripcion: Consulta paginada del catalogo SapCondicionPago (SapCondicionesPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapCondicionesPagoCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapCondicionPagoNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapCondicionesPago sapconpag
        WHERE   (@FilSapCondicionPagoNombre IS NULL
                    OR sapconpag.SapCondicionPagoNombre LIKE '%' + @FilSapCondicionPagoNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapconpag.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapconpag.SapCondicionPagoId,
                sapconpag.SapCondicionPagoNombre,
                sapconpag.Activo,
                sapconpag.UsuarioIdCreador,
                sapconpag.FechaCreacion,
                sapconpag.UsuarioIdUltMod,
                sapconpag.FechaUltMod
        FROM NCSapCondicionesPago sapconpag
        WHERE   (@FilSapCondicionPagoNombre IS NULL
                    OR sapconpag.SapCondicionPagoNombre LIKE '%' + @FilSapCondicionPagoNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapconpag.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapCondicionPagoId' THEN t.SapCondicionPagoId END ASC,
        CASE WHEN @ColumnaOrden = '-SapCondicionPagoId' THEN t.SapCondicionPagoId END DESC,
        CASE WHEN @ColumnaOrden = 'SapCondicionPagoNombre' THEN t.SapCondicionPagoNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapCondicionPagoNombre' THEN t.SapCondicionPagoNombre END DESC,
        CASE WHEN @ColumnaOrden = 'Activo' THEN t.Activo END ASC,
        CASE WHEN @ColumnaOrden = '-Activo' THEN t.Activo END DESC
    OFFSET @LinIni - 1 ROWS
    FETCH NEXT @TamPag ROWS ONLY

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:22:53
-- Descripcion: Mantenimiento del catalogo SapCondicionPago (SapCondicionesPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapCondicionesPagoIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapCondicionPagoId AS VARCHAR(50),
 @SapCondicionPagoNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapCondicionPagoId
                  FROM NCSapCondicionesPago
                  WHERE SapCondicionPagoId = @SapCondicionPagoId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapconpag.SapCondicionPagoNombre
                  FROM NCSapCondicionesPago sapconpag
                  WHERE sapconpag.SapCondicionPagoNombre = @SapCondicionPagoNombre) BEGIN
            SELECT -1; --El campo SapCondicionPagoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapCondicionesPago
        VALUES (@SapCondicionPagoId,
                @SapCondicionPagoNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapconpag.SapCondicionPagoNombre
                  FROM NCSapCondicionesPago sapconpag
                  WHERE sapconpag.SapCondicionPagoNombre = @SapCondicionPagoNombre
                        AND NOT (sapconpag.SapCondicionPagoId = @SapCondicionPagoId)) BEGIN
            SELECT -1; --El campo SapCondicionPagoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapCondicionesPago
        SET SapCondicionPagoNombre = @SapCondicionPagoNombre,
            Activo                 = @Activo,
            UsuarioIdUltMod        = @UsuarioIdSesion,
            FechaUltMod            = @vFecha
        WHERE   SapCondicionPagoId = @SapCondicionPagoId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapCondicionesPago
        WHERE   SapCondicionPagoId = @SapCondicionPagoId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:22:53
-- Descripcion: Consulta individual del catalogo SapCondicionPago (SapCondicionesPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapCondicionesPagoCI
 @SapCondicionPagoId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapconpag.SapCondicionPagoId,
            sapconpag.SapCondicionPagoNombre,
            sapconpag.Activo,
            sapconpag.UsuarioIdCreador,
            sapconpag.FechaCreacion,
            sapconpag.UsuarioIdUltMod,
            sapconpag.FechaUltMod
    FROM NCSapCondicionesPago sapconpag
    WHERE   sapconpag.SapCondicionPagoId = @SapCondicionPagoId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:22:53
-- Descripcion: Consulta de combo del catalogo SapCondicionPago (SapCondicionesPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapCondicionesPagoCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapCondicionPagoId AS Id,
            SapCondicionPagoNombre AS Texto
    FROM NCSapCondicionesPago
    WHERE   Activo = 1
    ORDER BY SapCondicionPagoNombre;

END
GO
