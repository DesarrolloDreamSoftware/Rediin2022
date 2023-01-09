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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Grupo de cuentas', 'ALAAAE', 30, '/PriCatalogos/SapGrupoCuentas/SapGrupoCuentaInicia', '', 'SapGrupoCuentaInicia', 0;

DECLARE @vJerarquiaSapGrupoCuentas AS VARCHAR(400);
SELECT @vJerarquiaSapGrupoCuentas = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapGrupoCuenta (SapGrupoCuentas) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapGrupoCuentas, NULL, 'SapGrupoCuentaXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapGrupoCuentas, NULL, 'SapGrupoCuentaInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapGrupoCuentas, NULL, 'SapGrupoCuentaActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapGrupoCuentas, NULL, 'SapGrupoCuentaElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapGrupoCuenta (SapGrupoCuentas) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapGrupoCuentas, NULL, 'SapGrupoCuentaExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapGrupoCuentas + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapGrupoCuenta (SapGrupoCuentas) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 18:30:35
-- Descripcion: Consulta paginada del catalogo SapGrupoCuenta (SapGrupoCuentas)
-- ==========================================================================================
CREATE PROCEDURE NCSapGrupoCuentasCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapGrupoCuentaNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapGrupoCuentas sapgrucue
        WHERE   (@FilSapGrupoCuentaNombre IS NULL
                    OR sapgrucue.SapGrupoCuentaNombre LIKE '%' + @FilSapGrupoCuentaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapgrucue.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapgrucue.SapGrupoCuentaId,
                sapgrucue.SapGrupoCuentaNombre,
                sapgrucue.Activo,
                sapgrucue.UsuarioIdCreador,
                sapgrucue.FechaCreacion,
                sapgrucue.UsuarioIdUltMod,
                sapgrucue.FechaUltMod
        FROM NCSapGrupoCuentas sapgrucue
        WHERE   (@FilSapGrupoCuentaNombre IS NULL
                    OR sapgrucue.SapGrupoCuentaNombre LIKE '%' + @FilSapGrupoCuentaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapgrucue.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapGrupoCuentaId' THEN t.SapGrupoCuentaId END ASC,
        CASE WHEN @ColumnaOrden = '-SapGrupoCuentaId' THEN t.SapGrupoCuentaId END DESC,
        CASE WHEN @ColumnaOrden = 'SapGrupoCuentaNombre' THEN t.SapGrupoCuentaNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapGrupoCuentaNombre' THEN t.SapGrupoCuentaNombre END DESC,
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
-- Fecha:       09/01/2023 18:30:35
-- Descripcion: Mantenimiento del catalogo SapGrupoCuenta (SapGrupoCuentas)
-- ==========================================================================================
CREATE PROCEDURE NCSapGrupoCuentasIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapGrupoCuentaId AS VARCHAR(50),
 @SapGrupoCuentaNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapGrupoCuentaId
                  FROM NCSapGrupoCuentas
                  WHERE SapGrupoCuentaId = @SapGrupoCuentaId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapgrucue.SapGrupoCuentaNombre
                  FROM NCSapGrupoCuentas sapgrucue
                  WHERE sapgrucue.SapGrupoCuentaNombre = @SapGrupoCuentaNombre) BEGIN
            SELECT -1; --El campo SapGrupoCuentaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapGrupoCuentas
        VALUES (@SapGrupoCuentaId,
                @SapGrupoCuentaNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapgrucue.SapGrupoCuentaNombre
                  FROM NCSapGrupoCuentas sapgrucue
                  WHERE sapgrucue.SapGrupoCuentaNombre = @SapGrupoCuentaNombre
                        AND NOT (sapgrucue.SapGrupoCuentaId = @SapGrupoCuentaId)) BEGIN
            SELECT -1; --El campo SapGrupoCuentaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapGrupoCuentas
        SET SapGrupoCuentaNombre = @SapGrupoCuentaNombre,
            Activo               = @Activo,
            UsuarioIdUltMod      = @UsuarioIdSesion,
            FechaUltMod          = @vFecha
        WHERE   SapGrupoCuentaId = @SapGrupoCuentaId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapGrupoCuentas
        WHERE   SapGrupoCuentaId = @SapGrupoCuentaId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 18:30:35
-- Descripcion: Consulta individual del catalogo SapGrupoCuenta (SapGrupoCuentas)
-- ==========================================================================================
CREATE PROCEDURE NCSapGrupoCuentasCI
 @SapGrupoCuentaId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapgrucue.SapGrupoCuentaId,
            sapgrucue.SapGrupoCuentaNombre,
            sapgrucue.Activo,
            sapgrucue.UsuarioIdCreador,
            sapgrucue.FechaCreacion,
            sapgrucue.UsuarioIdUltMod,
            sapgrucue.FechaUltMod
    FROM NCSapGrupoCuentas sapgrucue
    WHERE   sapgrucue.SapGrupoCuentaId = @SapGrupoCuentaId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 18:30:35
-- Descripcion: Consulta de combo del catalogo SapGrupoCuenta (SapGrupoCuentas)
-- ==========================================================================================
CREATE PROCEDURE NCSapGrupoCuentasCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapGrupoCuentaId AS Id,
            SapGrupoCuentaNombre AS Texto
    FROM NCSapGrupoCuentas
    WHERE   Activo = 1
    ORDER BY SapGrupoCuentaNombre;

END
GO
