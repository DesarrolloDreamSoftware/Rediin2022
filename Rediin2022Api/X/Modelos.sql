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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Modelos', 'ALAAAF', 10, '/PriCatalogos/Modelos/ModeloInicia', '', 1, 'ModeloInicia', 0;

DECLARE @vJerarquiaModelos AS VARCHAR(400);
SELECT @vJerarquiaModelos = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad Modelo (Modelos) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaModelos, NULL, 'ModeloXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaModelos, NULL, 'ModeloInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaModelos, NULL, 'ModeloActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaModelos, NULL, 'ModeloElimina', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaModelos + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad Modelo (Modelos) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:04:58
-- Descripcion: Consulta paginada del catalogo Modelo (Modelos)
-- ==========================================================================================
CREATE PROCEDURE NCModelosCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilModeloNombre AS VARCHAR(120) = NULL,
 @FilTipoCapturaId AS SMALLINT = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCModelos mode
        WHERE   (@FilModeloNombre IS NULL
                    OR mode.ModeloNombre LIKE '%' + @FilModeloNombre + '%')
                AND (@FilTipoCapturaId IS NULL
                    OR mode.TipoCapturaId = @FilTipoCapturaId)
                AND (@FilActivo IS NULL
                    OR mode.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  mode.ModeloId,
                mode.ModeloNombre,
                mode.TipoCapturaId,
                mode.Activo,
                mode.UsuarioIdCreador,
                mode.FechaCreacion,
                mode.UsuarioIdUltMod,
                mode.FechaUltMod
        FROM NCModelos mode
        WHERE   (@FilModeloNombre IS NULL
                    OR mode.ModeloNombre LIKE '%' + @FilModeloNombre + '%')
                AND (@FilTipoCapturaId IS NULL
                    OR mode.TipoCapturaId = @FilTipoCapturaId)
                AND (@FilActivo IS NULL
                    OR mode.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'ModeloId' THEN t.ModeloId END ASC,
        CASE WHEN @ColumnaOrden = '-ModeloId' THEN t.ModeloId END DESC,
        CASE WHEN @ColumnaOrden = 'ModeloNombre' THEN t.ModeloNombre END ASC,
        CASE WHEN @ColumnaOrden = '-ModeloNombre' THEN t.ModeloNombre END DESC,
        CASE WHEN @ColumnaOrden = 'TipoCapturaId' THEN t.TipoCapturaId END ASC,
        CASE WHEN @ColumnaOrden = '-TipoCapturaId' THEN t.TipoCapturaId END DESC,
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
-- Fecha:       19/09/2024 11:04:58
-- Descripcion: Mantenimiento del catalogo Modelo (Modelos)
-- ==========================================================================================
CREATE PROCEDURE NCModelosIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @ModeloId AS BIGINT,
 @ModeloNombre AS VARCHAR(120) = NULL,
 @TipoCapturaId AS SMALLINT = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT ModeloId
                  FROM NCModelos
                  WHERE ModeloId = @ModeloId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT mode.ModeloNombre
                  FROM NCModelos mode
                  WHERE mode.ModeloNombre = @ModeloNombre) BEGIN
            SELECT -1; --El campo ModeloNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCModelos
        VALUES (@ModeloId,
                @ModeloNombre,
                @TipoCapturaId,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT mode.ModeloNombre
                  FROM NCModelos mode
                  WHERE mode.ModeloNombre = @ModeloNombre
                        AND NOT (mode.ModeloId = @ModeloId)) BEGIN
            SELECT -1; --El campo ModeloNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCModelos
        SET ModeloNombre     = @ModeloNombre,
            TipoCapturaId    = @TipoCapturaId,
            Activo           = @Activo,
            UsuarioIdUltMod  = @UsuarioIdSesion,
            FechaUltMod      = @vFecha
        WHERE   ModeloId = @ModeloId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCModelos
        WHERE   ModeloId = @ModeloId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:04:58
-- Descripcion: Consulta individual del catalogo Modelo (Modelos)
-- ==========================================================================================
CREATE PROCEDURE NCModelosCI
 @ModeloId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  mode.ModeloId,
            mode.ModeloNombre,
            mode.TipoCapturaId,
            mode.Activo,
            mode.UsuarioIdCreador,
            mode.FechaCreacion,
            mode.UsuarioIdUltMod,
            mode.FechaUltMod
    FROM NCModelos mode
    WHERE   mode.ModeloId = @ModeloId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:04:58
-- Descripcion: Consulta de combo del catalogo Modelo (Modelos)
-- ==========================================================================================
CREATE PROCEDURE NCModelosCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  mode.ModeloId AS Id,
            mode.ModeloNombre AS Texto
    FROM NCModelos mode
    WHERE   mode.Activo = 1
    ORDER BY mode.ModeloNombre;

END
GO
