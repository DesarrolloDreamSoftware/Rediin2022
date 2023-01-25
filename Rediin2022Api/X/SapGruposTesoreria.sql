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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Grupos de tesoreria', 'ALAAAE', 40, '/PriCatalogos/SapGruposTesoreria/SapGrupoTesoreriaInicia', '', 'SapGrupoTesoreriaInicia', 0;

DECLARE @vJerarquiaSapGruposTesoreria AS VARCHAR(400);
SELECT @vJerarquiaSapGruposTesoreria = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapGrupoTesoreria (SapGruposTesoreria) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapGruposTesoreria, NULL, 'SapGrupoTesoreriaXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapGruposTesoreria, NULL, 'SapGrupoTesoreriaInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapGruposTesoreria, NULL, 'SapGrupoTesoreriaActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapGruposTesoreria, NULL, 'SapGrupoTesoreriaElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapGrupoTesoreria (SapGruposTesoreria) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapGruposTesoreria, NULL, 'SapGrupoTesoreriaExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapGruposTesoreria + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapGrupoTesoreria (SapGruposTesoreria) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 15:15:24
-- Descripcion: Consulta paginada del catalogo SapGrupoTesoreria (SapGruposTesoreria)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposTesoreriaCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapGrupoTesoreriaNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapGruposTesoreria sapgrutes
        WHERE   (@FilSapGrupoTesoreriaNombre IS NULL
                    OR sapgrutes.SapGrupoTesoreriaNombre LIKE '%' + @FilSapGrupoTesoreriaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapgrutes.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapgrutes.SapGrupoTesoreriaId,
                sapgrutes.SapGrupoTesoreriaNombre,
                sapgrutes.Activo,
                sapgrutes.UsuarioIdCreador,
                sapgrutes.FechaCreacion,
                sapgrutes.UsuarioIdUltMod,
                sapgrutes.FechaUltMod
        FROM NCSapGruposTesoreria sapgrutes
        WHERE   (@FilSapGrupoTesoreriaNombre IS NULL
                    OR sapgrutes.SapGrupoTesoreriaNombre LIKE '%' + @FilSapGrupoTesoreriaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapgrutes.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapGrupoTesoreriaId' THEN t.SapGrupoTesoreriaId END ASC,
        CASE WHEN @ColumnaOrden = '-SapGrupoTesoreriaId' THEN t.SapGrupoTesoreriaId END DESC,
        CASE WHEN @ColumnaOrden = 'SapGrupoTesoreriaNombre' THEN t.SapGrupoTesoreriaNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapGrupoTesoreriaNombre' THEN t.SapGrupoTesoreriaNombre END DESC,
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
-- Fecha:       10/01/2023 15:15:24
-- Descripcion: Mantenimiento del catalogo SapGrupoTesoreria (SapGruposTesoreria)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposTesoreriaIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapGrupoTesoreriaId AS VARCHAR(50),
 @SapGrupoTesoreriaNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapGrupoTesoreriaId
                  FROM NCSapGruposTesoreria
                  WHERE SapGrupoTesoreriaId = @SapGrupoTesoreriaId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapgrutes.SapGrupoTesoreriaNombre
                  FROM NCSapGruposTesoreria sapgrutes
                  WHERE sapgrutes.SapGrupoTesoreriaNombre = @SapGrupoTesoreriaNombre) BEGIN
            SELECT -1; --El campo SapGrupoTesoreriaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapGruposTesoreria
        VALUES (@SapGrupoTesoreriaId,
                @SapGrupoTesoreriaNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapgrutes.SapGrupoTesoreriaNombre
                  FROM NCSapGruposTesoreria sapgrutes
                  WHERE sapgrutes.SapGrupoTesoreriaNombre = @SapGrupoTesoreriaNombre
                        AND NOT (sapgrutes.SapGrupoTesoreriaId = @SapGrupoTesoreriaId)) BEGIN
            SELECT -1; --El campo SapGrupoTesoreriaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapGruposTesoreria
        SET SapGrupoTesoreriaNombre = @SapGrupoTesoreriaNombre,
            Activo                  = @Activo,
            UsuarioIdUltMod         = @UsuarioIdSesion,
            FechaUltMod             = @vFecha
        WHERE   SapGrupoTesoreriaId = @SapGrupoTesoreriaId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapGruposTesoreria
        WHERE   SapGrupoTesoreriaId = @SapGrupoTesoreriaId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 15:15:24
-- Descripcion: Consulta individual del catalogo SapGrupoTesoreria (SapGruposTesoreria)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposTesoreriaCI
 @SapGrupoTesoreriaId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapgrutes.SapGrupoTesoreriaId,
            sapgrutes.SapGrupoTesoreriaNombre,
            sapgrutes.Activo,
            sapgrutes.UsuarioIdCreador,
            sapgrutes.FechaCreacion,
            sapgrutes.UsuarioIdUltMod,
            sapgrutes.FechaUltMod
    FROM NCSapGruposTesoreria sapgrutes
    WHERE   sapgrutes.SapGrupoTesoreriaId = @SapGrupoTesoreriaId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 15:15:24
-- Descripcion: Consulta de combo del catalogo SapGrupoTesoreria (SapGruposTesoreria)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposTesoreriaCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapGrupoTesoreriaId AS Id,
            SapGrupoTesoreriaNombre AS Texto
    FROM NCSapGruposTesoreria
    WHERE   Activo = 1
    ORDER BY SapGrupoTesoreriaNombre;

END
GO
