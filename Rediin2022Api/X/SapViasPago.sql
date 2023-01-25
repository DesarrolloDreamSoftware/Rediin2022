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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Vías de pago', 'ALAAAE', 100, '/PriCatalogos/SapViasPago/SapViaPagoInicia', '', 'SapViaPagoInicia', 0;

DECLARE @vJerarquiaSapViasPago AS VARCHAR(400);
SELECT @vJerarquiaSapViasPago = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapViaPago (SapViasPago) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapViasPago, NULL, 'SapViaPagoXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapViasPago, NULL, 'SapViaPagoInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapViasPago, NULL, 'SapViaPagoActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapViasPago, NULL, 'SapViaPagoElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapViaPago (SapViasPago) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapViasPago, NULL, 'SapViaPagoExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapViasPago + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapViaPago (SapViasPago) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:35:46
-- Descripcion: Consulta paginada del catalogo SapViaPago (SapViasPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapViasPagoCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapViaPagoNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapViasPago sapviapag
        WHERE   (@FilSapViaPagoNombre IS NULL
                    OR sapviapag.SapViaPagoNombre LIKE '%' + @FilSapViaPagoNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapviapag.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapviapag.SapViaPagoId,
                sapviapag.SapViaPagoNombre,
                sapviapag.Activo,
                sapviapag.UsuarioIdCreador,
                sapviapag.FechaCreacion,
                sapviapag.UsuarioIdUltMod,
                sapviapag.FechaUltMod
        FROM NCSapViasPago sapviapag
        WHERE   (@FilSapViaPagoNombre IS NULL
                    OR sapviapag.SapViaPagoNombre LIKE '%' + @FilSapViaPagoNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapviapag.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapViaPagoId' THEN t.SapViaPagoId END ASC,
        CASE WHEN @ColumnaOrden = '-SapViaPagoId' THEN t.SapViaPagoId END DESC,
        CASE WHEN @ColumnaOrden = 'SapViaPagoNombre' THEN t.SapViaPagoNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapViaPagoNombre' THEN t.SapViaPagoNombre END DESC,
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
-- Fecha:       10/01/2023 16:35:46
-- Descripcion: Mantenimiento del catalogo SapViaPago (SapViasPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapViasPagoIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapViaPagoId AS VARCHAR(50),
 @SapViaPagoNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapViaPagoId
                  FROM NCSapViasPago
                  WHERE SapViaPagoId = @SapViaPagoId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapviapag.SapViaPagoNombre
                  FROM NCSapViasPago sapviapag
                  WHERE sapviapag.SapViaPagoNombre = @SapViaPagoNombre) BEGIN
            SELECT -1; --El campo SapViaPagoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapViasPago
        VALUES (@SapViaPagoId,
                @SapViaPagoNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapviapag.SapViaPagoNombre
                  FROM NCSapViasPago sapviapag
                  WHERE sapviapag.SapViaPagoNombre = @SapViaPagoNombre
                        AND NOT (sapviapag.SapViaPagoId = @SapViaPagoId)) BEGIN
            SELECT -1; --El campo SapViaPagoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapViasPago
        SET SapViaPagoNombre = @SapViaPagoNombre,
            Activo           = @Activo,
            UsuarioIdUltMod  = @UsuarioIdSesion,
            FechaUltMod      = @vFecha
        WHERE   SapViaPagoId = @SapViaPagoId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapViasPago
        WHERE   SapViaPagoId = @SapViaPagoId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:35:46
-- Descripcion: Consulta individual del catalogo SapViaPago (SapViasPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapViasPagoCI
 @SapViaPagoId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapviapag.SapViaPagoId,
            sapviapag.SapViaPagoNombre,
            sapviapag.Activo,
            sapviapag.UsuarioIdCreador,
            sapviapag.FechaCreacion,
            sapviapag.UsuarioIdUltMod,
            sapviapag.FechaUltMod
    FROM NCSapViasPago sapviapag
    WHERE   sapviapag.SapViaPagoId = @SapViaPagoId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:35:46
-- Descripcion: Consulta de combo del catalogo SapViaPago (SapViasPago)
-- ==========================================================================================
CREATE PROCEDURE NCSapViasPagoCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapViaPagoId AS Id,
            SapViaPagoNombre AS Texto
    FROM NCSapViasPago
    WHERE   Activo = 1
    ORDER BY SapViaPagoNombre;

END
GO
