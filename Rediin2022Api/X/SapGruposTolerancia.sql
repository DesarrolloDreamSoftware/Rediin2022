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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Grupos de tolerancia', 'ALAAAE', 50, '/PriCatalogos/SapGruposTolerancia/SapGrupoToleranciaInicia', '', 'SapGrupoToleranciaInicia', 0;

DECLARE @vJerarquiaSapGruposTolerancia AS VARCHAR(400);
SELECT @vJerarquiaSapGruposTolerancia = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapGrupoTolerancia (SapGruposTolerancia) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapGruposTolerancia, NULL, 'SapGrupoToleranciaXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapGruposTolerancia, NULL, 'SapGrupoToleranciaInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapGruposTolerancia, NULL, 'SapGrupoToleranciaActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapGruposTolerancia, NULL, 'SapGrupoToleranciaElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapGrupoTolerancia (SapGruposTolerancia) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapGruposTolerancia, NULL, 'SapGrupoToleranciaExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapGruposTolerancia + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapGrupoTolerancia (SapGruposTolerancia) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 15:29:45
-- Descripcion: Consulta paginada del catalogo SapGrupoTolerancia (SapGruposTolerancia)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposToleranciaCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapGrupoToleranciaNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapGruposTolerancia sapgrutol
        WHERE   (@FilSapGrupoToleranciaNombre IS NULL
                    OR sapgrutol.SapGrupoToleranciaNombre LIKE '%' + @FilSapGrupoToleranciaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapgrutol.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapgrutol.SapGrupoToleranciaId,
                sapgrutol.SapGrupoToleranciaNombre,
                sapgrutol.Activo,
                sapgrutol.UsuarioIdCreador,
                sapgrutol.FechaCreacion,
                sapgrutol.UsuarioIdUltMod,
                sapgrutol.FechaUltMod
        FROM NCSapGruposTolerancia sapgrutol
        WHERE   (@FilSapGrupoToleranciaNombre IS NULL
                    OR sapgrutol.SapGrupoToleranciaNombre LIKE '%' + @FilSapGrupoToleranciaNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapgrutol.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapGrupoToleranciaId' THEN t.SapGrupoToleranciaId END ASC,
        CASE WHEN @ColumnaOrden = '-SapGrupoToleranciaId' THEN t.SapGrupoToleranciaId END DESC,
        CASE WHEN @ColumnaOrden = 'SapGrupoToleranciaNombre' THEN t.SapGrupoToleranciaNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapGrupoToleranciaNombre' THEN t.SapGrupoToleranciaNombre END DESC,
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
-- Fecha:       10/01/2023 15:29:45
-- Descripcion: Mantenimiento del catalogo SapGrupoTolerancia (SapGruposTolerancia)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposToleranciaIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapGrupoToleranciaId AS VARCHAR(50),
 @SapGrupoToleranciaNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapGrupoToleranciaId
                  FROM NCSapGruposTolerancia
                  WHERE SapGrupoToleranciaId = @SapGrupoToleranciaId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapgrutol.SapGrupoToleranciaNombre
                  FROM NCSapGruposTolerancia sapgrutol
                  WHERE sapgrutol.SapGrupoToleranciaNombre = @SapGrupoToleranciaNombre) BEGIN
            SELECT -1; --El campo SapGrupoToleranciaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapGruposTolerancia
        VALUES (@SapGrupoToleranciaId,
                @SapGrupoToleranciaNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapgrutol.SapGrupoToleranciaNombre
                  FROM NCSapGruposTolerancia sapgrutol
                  WHERE sapgrutol.SapGrupoToleranciaNombre = @SapGrupoToleranciaNombre
                        AND NOT (sapgrutol.SapGrupoToleranciaId = @SapGrupoToleranciaId)) BEGIN
            SELECT -1; --El campo SapGrupoToleranciaNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapGruposTolerancia
        SET SapGrupoToleranciaId     = @SapGrupoToleranciaId,
            SapGrupoToleranciaNombre = @SapGrupoToleranciaNombre,
            Activo                   = @Activo,
            UsuarioIdUltMod          = @UsuarioIdSesion,
            FechaUltMod              = @vFecha
        WHERE   ;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapGruposTolerancia
        WHERE   SapGrupoToleranciaId = @SapGrupoToleranciaId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 15:29:45
-- Descripcion: Consulta individual del catalogo SapGrupoTolerancia (SapGruposTolerancia)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposToleranciaCI
 @SapGrupoToleranciaId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapgrutol.SapGrupoToleranciaId,
            sapgrutol.SapGrupoToleranciaNombre,
            sapgrutol.Activo,
            sapgrutol.UsuarioIdCreador,
            sapgrutol.FechaCreacion,
            sapgrutol.UsuarioIdUltMod,
            sapgrutol.FechaUltMod
    FROM NCSapGruposTolerancia sapgrutol
    WHERE   sapgrutol.SapGrupoToleranciaId = @SapGrupoToleranciaId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 15:29:45
-- Descripcion: Consulta de combo del catalogo SapGrupoTolerancia (SapGruposTolerancia)
-- ==========================================================================================
CREATE PROCEDURE NCSapGruposToleranciaCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapGrupoToleranciaId AS Id,
            SapGrupoToleranciaNombre AS Texto
    FROM NCSapGruposTolerancia
    WHERE   Activo = 1
    ORDER BY SapGrupoToleranciaNombre;

END
GO
