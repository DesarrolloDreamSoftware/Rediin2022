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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Cuentas asociadas', 'ALAAAE', 20, '/PriCatalogos/SapCuentasAsociadas/SapCuentaAsociadaInicia', '', 'SapCuentaAsociadaInicia', 0;

DECLARE @vJerarquiaSapCuentasAsociadas AS VARCHAR(400);
SELECT @vJerarquiaSapCuentasAsociadas = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapCuentaAsociada (SapCuentasAsociadas) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapCuentasAsociadas, NULL, 'SapCuentaAsociadaXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapCuentasAsociadas, NULL, 'SapCuentaAsociadaInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapCuentasAsociadas, NULL, 'SapCuentaAsociadaActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapCuentasAsociadas, NULL, 'SapCuentaAsociadaElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapCuentaAsociada (SapCuentasAsociadas) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapCuentasAsociadas, NULL, 'SapCuentaAsociadaExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapCuentasAsociadas + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapCuentaAsociada (SapCuentasAsociadas) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:52:03
-- Descripcion: Consulta paginada del catalogo SapCuentaAsociada (SapCuentasAsociadas)
-- ==========================================================================================
CREATE PROCEDURE NCSapCuentasAsociadasCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapCuentaAsociadaNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapCuentasAsociadas sapcueaso
        WHERE   (@FilSapCuentaAsociadaNombre IS NULL
                    OR sapcueaso.SapCuentaAsociadaNombre LIKE '%' + @FilSapCuentaAsociadaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapcueaso.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapcueaso.SapCuentaAsociadaId,
                sapcueaso.SapCuentaAsociadaNombre,
                sapcueaso.Activo,
                sapcueaso.UsuarioIdCreador,
                sapcueaso.FechaCreacion,
                sapcueaso.UsuarioIdUltMod,
                sapcueaso.FechaUltMod
        FROM NCSapCuentasAsociadas sapcueaso
        WHERE   (@FilSapCuentaAsociadaNombre IS NULL
                    OR sapcueaso.SapCuentaAsociadaNombre LIKE '%' + @FilSapCuentaAsociadaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapcueaso.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapCuentaAsociadaId' THEN t.SapCuentaAsociadaId END ASC,
        CASE WHEN @ColumnaOrden = '-SapCuentaAsociadaId' THEN t.SapCuentaAsociadaId END DESC,
        CASE WHEN @ColumnaOrden = 'SapCuentaAsociadaNombre' THEN t.SapCuentaAsociadaNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapCuentaAsociadaNombre' THEN t.SapCuentaAsociadaNombre END DESC,
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
-- Fecha:       09/01/2023 17:52:04
-- Descripcion: Mantenimiento del catalogo SapCuentaAsociada (SapCuentasAsociadas)
-- ==========================================================================================
CREATE PROCEDURE NCSapCuentasAsociadasIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapCuentaAsociadaId AS VARCHAR(50),
 @SapCuentaAsociadaNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapCuentaAsociadaId
                  FROM NCSapCuentasAsociadas
                  WHERE SapCuentaAsociadaId = @SapCuentaAsociadaId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapcueaso.SapCuentaAsociadaNombre
                  FROM NCSapCuentasAsociadas sapcueaso
                  WHERE sapcueaso.SapCuentaAsociadaNombre = @SapCuentaAsociadaNombre) BEGIN
            SELECT -1; --El campo SapCuentaAsociadaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapCuentasAsociadas
        VALUES (@SapCuentaAsociadaId,
                @SapCuentaAsociadaNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapcueaso.SapCuentaAsociadaNombre
                  FROM NCSapCuentasAsociadas sapcueaso
                  WHERE sapcueaso.SapCuentaAsociadaNombre = @SapCuentaAsociadaNombre
                        AND NOT (sapcueaso.SapCuentaAsociadaId = @SapCuentaAsociadaId)) BEGIN
            SELECT -1; --El campo SapCuentaAsociadaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapCuentasAsociadas
        SET SapCuentaAsociadaNombre = @SapCuentaAsociadaNombre,
            Activo                  = @Activo,
            UsuarioIdUltMod         = @UsuarioIdSesion,
            FechaUltMod             = @vFecha
        WHERE   SapCuentaAsociadaId = @SapCuentaAsociadaId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapCuentasAsociadas
        WHERE   SapCuentaAsociadaId = @SapCuentaAsociadaId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:52:04
-- Descripcion: Consulta individual del catalogo SapCuentaAsociada (SapCuentasAsociadas)
-- ==========================================================================================
CREATE PROCEDURE NCSapCuentasAsociadasCI
 @SapCuentaAsociadaId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapcueaso.SapCuentaAsociadaId,
            sapcueaso.SapCuentaAsociadaNombre,
            sapcueaso.Activo,
            sapcueaso.UsuarioIdCreador,
            sapcueaso.FechaCreacion,
            sapcueaso.UsuarioIdUltMod,
            sapcueaso.FechaUltMod
    FROM NCSapCuentasAsociadas sapcueaso
    WHERE   sapcueaso.SapCuentaAsociadaId = @SapCuentaAsociadaId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       09/01/2023 17:52:04
-- Descripcion: Consulta de combo del catalogo SapCuentaAsociada (SapCuentasAsociadas)
-- ==========================================================================================
CREATE PROCEDURE NCSapCuentasAsociadasCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapCuentaAsociadaId AS Id,
            SapCuentaAsociadaNombre AS Texto
    FROM NCSapCuentasAsociadas
    WHERE   Activo = 1
    ORDER BY SapCuentaAsociadaNombre;

END
GO
