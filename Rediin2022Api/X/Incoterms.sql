/* ************************************************** */
/* CREACION DEL ARBOL DE PERMISOS PARA MenuId = 1101     */
/* JERARQUIA ENCONTRADA ALAAAF                          */
/* ************************************************** */
USE [Sisegui2020]
GO

DECLARE @vJerarquiaAdminDoc AS VARCHAR(400);
DECLARE @vJerarquiaSelCol AS VARCHAR(400);
DECLARE @vTblPermisoId TABLE (PermisoId BIGINT);
DECLARE @vTblOpcMenuId TABLE (OpcionXMenuId BIGINT);

/* Insertamos la opción del menú principal */
INSERT INTO @vTblOpcMenuId
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Incoterms', 'ALAAAF', 10, '/PriCatalogos/Incoterms/IncotermInicia', '', 1, 'IncotermInicia', 0;

DECLARE @vJerarquiaIncoterms AS VARCHAR(400);
SELECT @vJerarquiaIncoterms = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad Incoterm (Incoterms) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaIncoterms, NULL, 'IncotermXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaIncoterms, NULL, 'IncotermInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaIncoterms, NULL, 'IncotermActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaIncoterms, NULL, 'IncotermElimina', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaIncoterms + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad Incoterm (Incoterms) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:15:38
-- Descripcion: Consulta paginada del catalogo Incoterm (Incoterms)
-- ==========================================================================================
CREATE PROCEDURE NCIncotermsCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilIncotermNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCIncoterms inco
        WHERE   (@FilIncotermNombre IS NULL
                    OR inco.IncotermNombre LIKE '%' + @FilIncotermNombre + '%')
                AND (@FilActivo IS NULL
                    OR inco.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  inco.IncotermId,
                inco.IncotermClave,
                inco.IncotermNombre,
                inco.Activo,
                inco.UsuarioIdCreador,
                inco.FechaCreacion,
                inco.UsuarioIdUltMod,
                inco.FechaUltMod
        FROM NCIncoterms inco
        WHERE   (@FilIncotermNombre IS NULL
                    OR inco.IncotermNombre LIKE '%' + @FilIncotermNombre + '%')
                AND (@FilActivo IS NULL
                    OR inco.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'IncotermId' THEN t.IncotermId END ASC,
        CASE WHEN @ColumnaOrden = '-IncotermId' THEN t.IncotermId END DESC,
        CASE WHEN @ColumnaOrden = 'IncotermClave' THEN t.IncotermClave END ASC,
        CASE WHEN @ColumnaOrden = '-IncotermClave' THEN t.IncotermClave END DESC,
        CASE WHEN @ColumnaOrden = 'IncotermNombre' THEN t.IncotermNombre END ASC,
        CASE WHEN @ColumnaOrden = '-IncotermNombre' THEN t.IncotermNombre END DESC,
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
-- Fecha:       19/09/2024 11:15:38
-- Descripcion: Mantenimiento del catalogo Incoterm (Incoterms)
-- ==========================================================================================
CREATE PROCEDURE NCIncotermsIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @IncotermId AS BIGINT,
 @IncotermClave AS VARCHAR(10) = NULL,
 @IncotermNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT inco.IncotermNombre
                  FROM NCIncoterms inco
                  WHERE inco.IncotermNombre = @IncotermNombre) BEGIN
            SELECT -1; --El campo IncotermNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        EXEC PUGeneraConsecutivo 'NCIncoterms', 'IncotermId', @Consecutivo = @IncotermId OUT;

        INSERT INTO NCIncoterms
        VALUES (@IncotermId,
                @IncotermClave,
                @IncotermNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT @IncotermId; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT inco.IncotermNombre
                  FROM NCIncoterms inco
                  WHERE inco.IncotermNombre = @IncotermNombre
                        AND NOT (inco.IncotermId = @IncotermId)) BEGIN
            SELECT -1; --El campo IncotermNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCIncoterms
        SET IncotermClave    = @IncotermClave,
            IncotermNombre   = @IncotermNombre,
            Activo           = @Activo,
            UsuarioIdUltMod  = @UsuarioIdSesion,
            FechaUltMod      = @vFecha
        WHERE   IncotermId = @IncotermId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCIncoterms
        WHERE   IncotermId = @IncotermId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:15:38
-- Descripcion: Consulta individual del catalogo Incoterm (Incoterms)
-- ==========================================================================================
CREATE PROCEDURE NCIncotermsCI
 @IncotermId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  inco.IncotermId,
            inco.IncotermClave,
            inco.IncotermNombre,
            inco.Activo,
            inco.UsuarioIdCreador,
            inco.FechaCreacion,
            inco.UsuarioIdUltMod,
            inco.FechaUltMod
    FROM NCIncoterms inco
    WHERE   inco.IncotermId = @IncotermId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:15:38
-- Descripcion: Consulta de combo del catalogo Incoterm (Incoterms)
-- ==========================================================================================
CREATE PROCEDURE NCIncotermsCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  inco.IncotermId AS Id,
            inco.IncotermNombre AS Texto
    FROM NCIncoterms inco
    WHERE   inco.Activo = 1
    ORDER BY inco.IncotermNombre;

END
GO
