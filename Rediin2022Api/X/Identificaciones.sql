/* ************************************************** */
/* CREACION DEL ARBOL DE PERMISOS PARA MenuId = 1101     */
/* JERARQUIA ENCONTRADA ALAA                          */
/* ************************************************** */
USE [RediinSisegui2022]
GO

DECLARE @vJerarquiaAdminDoc AS VARCHAR(400);
DECLARE @vJerarquiaSelCol AS VARCHAR(400);
DECLARE @vTblPermisoId TABLE (PermisoId BIGINT);
DECLARE @vTblOpcMenuId TABLE (OpcionXMenuId BIGINT);

/* Insertamos la opción del menú principal */
INSERT INTO @vTblOpcMenuId
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Identificaciones', 'ALAA', 40, '/PriCatalogos/Identificaciones/IdentificacionInicia', '', 'IdentificacionInicia', 0;

DECLARE @vJerarquiaIdentificaciones AS VARCHAR(400);
SELECT @vJerarquiaIdentificaciones = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad Identificacion (Identificaciones) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaIdentificaciones, NULL, 'IdentificacionXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaIdentificaciones, NULL, 'IdentificacionInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaIdentificaciones, NULL, 'IdentificacionActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaIdentificaciones, NULL, 'IdentificacionElimina', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaIdentificaciones + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad Identificacion (Identificaciones) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/11/2022 13:13:10
-- Descripcion: Consulta paginada del catalogo Identificacion (Identificaciones)
-- ==========================================================================================
CREATE PROCEDURE NCIdentificacionesCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilIdentificacionNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCIdentificaciones iden
        WHERE   (@FilIdentificacionNombre IS NULL
                    OR iden.IdentificacionNombre LIKE '%' + @FilIdentificacionNombre + '%')
                AND (@FilActivo IS NULL
                    OR iden.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  iden.IdentificacionId,
                iden.IdentificacionNombre,
                iden.Activo,
                iden.UsuarioIdCreador,
                iden.FechaCreacion,
                iden.UsuarioIdUltMod,
                iden.FechaUltMod
        FROM NCIdentificaciones iden
        WHERE   (@FilIdentificacionNombre IS NULL
                    OR iden.IdentificacionNombre LIKE '%' + @FilIdentificacionNombre + '%')
                AND (@FilActivo IS NULL
                    OR iden.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'IdentificacionId' THEN t.IdentificacionId END ASC,
        CASE WHEN @ColumnaOrden = '-IdentificacionId' THEN t.IdentificacionId END DESC,
        CASE WHEN @ColumnaOrden = 'IdentificacionNombre' THEN t.IdentificacionNombre END ASC,
        CASE WHEN @ColumnaOrden = '-IdentificacionNombre' THEN t.IdentificacionNombre END DESC,
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
-- Fecha:       06/11/2022 13:13:10
-- Descripcion: Mantenimiento del catalogo Identificacion (Identificaciones)
-- ==========================================================================================
CREATE PROCEDURE NCIdentificacionesIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @IdentificacionId AS BIGINT,
 @IdentificacionNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT iden.IdentificacionNombre
                  FROM NCIdentificaciones iden
                  WHERE iden.IdentificacionNombre = @IdentificacionNombre) BEGIN
            SELECT -1; --El campo IdentificacionNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        EXEC PUGeneraConsecutivo 'NCIdentificaciones', 'IdentificacionId', @Consecutivo = @IdentificacionId OUT;

        INSERT INTO NCIdentificaciones
        VALUES (@IdentificacionId,
                @IdentificacionNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT @IdentificacionId; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT iden.IdentificacionNombre
                  FROM NCIdentificaciones iden
                  WHERE iden.IdentificacionNombre = @IdentificacionNombre
                        AND NOT (iden.IdentificacionId = @IdentificacionId)) BEGIN
            SELECT -1; --El campo IdentificacionNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCIdentificaciones
        SET IdentificacionNombre = @IdentificacionNombre,
            Activo               = @Activo,
            UsuarioIdUltMod      = @UsuarioIdSesion,
            FechaUltMod          = @vFecha
        WHERE   IdentificacionId = @IdentificacionId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCIdentificaciones
        WHERE   IdentificacionId = @IdentificacionId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/11/2022 13:13:10
-- Descripcion: Consulta individual del catalogo Identificacion (Identificaciones)
-- ==========================================================================================
CREATE PROCEDURE NCIdentificacionesCI
 @IdentificacionId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  iden.IdentificacionId,
            iden.IdentificacionNombre,
            iden.Activo,
            iden.UsuarioIdCreador,
            iden.FechaCreacion,
            iden.UsuarioIdUltMod,
            iden.FechaUltMod
    FROM NCIdentificaciones iden
    WHERE   iden.IdentificacionId = @IdentificacionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/11/2022 13:13:10
-- Descripcion: Consulta de combo del catalogo Identificacion (Identificaciones)
-- ==========================================================================================
CREATE PROCEDURE NCIdentificacionesCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  IdentificacionId AS Id,
            IdentificacionNombre AS Texto
    FROM NCIdentificaciones
    WHERE   Activo = 1
    ORDER BY IdentificacionNombre;

END
GO
