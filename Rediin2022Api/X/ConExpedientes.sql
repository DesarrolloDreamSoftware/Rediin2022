/* ************************************************** */
/* CREACION DEL ARBOL DE PERMISOS PARA MenuId = 1101     */
/* JERARQUIA ENCONTRADA ALAB                          */
/* ************************************************** */
USE [Sisegui2020]
GO

DECLARE @vJerarquiaAdminDoc AS VARCHAR(400);
DECLARE @vJerarquiaSelCol AS VARCHAR(400);
DECLARE @vTblPermisoId TABLE (PermisoId BIGINT);
DECLARE @vTblOpcMenuId TABLE (OpcionXMenuId BIGINT);

/* Insertamos la opción del menú principal */
INSERT INTO @vTblOpcMenuId
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Front digital', 'ALAB', 30, '/PriOperacion/ConExpedientes/ConExpProcOperativoInicia', '', 'ConExpProcOperativoInicia', 0;

DECLARE @vJerarquiaEnc AS VARCHAR(400);
SELECT @vJerarquiaEnc = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad ConExpProcOperativo (Enc) */
    /* **************************************************************************************************** */

    /* Permisos de acciones que van a la entidad ConExpProcOperativo (Enc) */
    DELETE @vTblPermisoId;
    INSERT INTO @vTblPermisoId
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar exp', @vJerarquiaEnc, NULL, 'ConExpedienteInicia', 0, 0, 1;

    DECLARE @vJerarquiaExp AS VARCHAR(400);
    SELECT @vJerarquiaExp = Jerarquia
    FROM PTPermisos
    WHERE MenuId = 1101 AND PermisoId = (SELECT TOP 1 PermisoId FROM @vTblPermisoId);

        /* **************************************************************************************************** */
        /* Permisos para la entidad ConExpediente (Exp) */
        /* **************************************************************************************************** */
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaExp, NULL, 'ConExpedienteXId', 0, 0, 1;
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaExp, NULL, 'ConExpedienteInserta', 0, 0, 1;
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaExp, NULL, 'ConExpedienteActualiza', 0, 0, 1;
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaExp, NULL, 'ConExpedienteElimina', 0, 0, 1;
        /* Acciones personalizadas para la entidad ConExpediente (Exp) */
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Cambio estatus uno', @vJerarquiaExp, NULL, 'ConExpedienteCambioEstatusUno', 0, 0, 1;
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Cambio estatus dos', @vJerarquiaExp, NULL, 'ConExpedienteCambioEstatusDos', 0, 0, 1;

        /* Permisos de acciones que van a la entidad ConExpediente (Exp) */
        DELETE @vTblPermisoId;
        INSERT INTO @vTblPermisoId
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar objs', @vJerarquiaExp, NULL, 'ConExpedienteObjetoInicia', 0, 0, 1;

        DECLARE @vJerarquiaObjs AS VARCHAR(400);
        SELECT @vJerarquiaObjs = Jerarquia
        FROM PTPermisos
        WHERE MenuId = 1101 AND PermisoId = (SELECT TOP 1 PermisoId FROM @vTblPermisoId);

        /* Permisos de acciones que van a la entidad ConExpediente (Exp) */
        DELETE @vTblPermisoId;
        INSERT INTO @vTblPermisoId
        EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar expe esta', @vJerarquiaExp, NULL, 'ExpedienteEstatuInicia', 0, 0, 1;

        DECLARE @vJerarquiaExpeEsta AS VARCHAR(400);
        SELECT @vJerarquiaExpeEsta = Jerarquia
        FROM PTPermisos
        WHERE MenuId = 1101 AND PermisoId = (SELECT TOP 1 PermisoId FROM @vTblPermisoId);

            /* **************************************************************************************************** */
            /* Permisos para la entidad ConExpedienteObjeto (Objs) */
            /* **************************************************************************************************** */
            EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaObjs, NULL, 'ConExpedienteObjetoXId', 0, 0, 1;
            EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaObjs, NULL, 'ConExpedienteObjetoInserta', 0, 0, 1;
            EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaObjs, NULL, 'ConExpedienteObjetoElimina', 0, 0, 1;
            /* Acciones personalizadas para la entidad ConExpedienteObjeto (Objs) */
            EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Agregar archivo', @vJerarquiaObjs, NULL, 'ConExpedienteObjetoAgregarArchivo', 0, 0, 1;

            /* **************************************************************************************************** */
            /* Permisos para la entidad ExpedienteEstatu (ExpeEsta) */
            /* **************************************************************************************************** */



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
WHERE   Jerarquia LIKE @vJerarquiaEnc + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad ConExpProcOperativo (Enc) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Consulta paginada del negocio ConExpedientes - ConExpProcOperativo (Enc)
-- ==========================================================================================
CREATE PROCEDURE NTExpEncCP
 @EstablecimientoId AS BIGINT,
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilProcesoOperativoNombre AS VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCProcesosOperativos proope
        WHERE   proope.Activo = 1
                AND proope.EstablecimientoId = @EstablecimientoId
                AND (@FilProcesoOperativoNombre IS NULL
                    OR proope.ProcesoOperativoNombre LIKE '%' + @FilProcesoOperativoNombre + '%'
                    OR CAST(proope.ProcesoOperativoId AS VARCHAR) = @FilProcesoOperativoNombre)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  proope.EstablecimientoId,
                proope.ProcesoOperativoId,
                proope.ProcesoOperativoNombre,
                proope.Orden,
                proope.ControlEstatus,
                proope.Activo,
                proope.UsuarioIdCreador,
                proope.FechaCreacion,
                proope.UsuarioIdUltMod,
                proope.FechaUltMod
        FROM NCProcesosOperativos proope
        WHERE   proope.Activo = 1
                AND proope.EstablecimientoId = @EstablecimientoId
                AND (@FilProcesoOperativoNombre IS NULL
                    OR proope.ProcesoOperativoNombre LIKE '%' + @FilProcesoOperativoNombre + '%'
                    OR CAST(proope.ProcesoOperativoId AS VARCHAR) = @FilProcesoOperativoNombre)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'ProcesoOperativoId' THEN t.ProcesoOperativoId END ASC,
        CASE WHEN @ColumnaOrden = '-ProcesoOperativoId' THEN t.ProcesoOperativoId END DESC,
        CASE WHEN @ColumnaOrden = 'ProcesoOperativoNombre' THEN t.ProcesoOperativoNombre END ASC,
        CASE WHEN @ColumnaOrden = '-ProcesoOperativoNombre' THEN t.ProcesoOperativoNombre END DESC,
        CASE WHEN @ColumnaOrden = 'Orden' THEN t.Orden END ASC,
        CASE WHEN @ColumnaOrden = '-Orden' THEN t.Orden END DESC
    OFFSET @LinIni - 1 ROWS
    FETCH NEXT @TamPag ROWS ONLY

END
GO
/* ******************************************************************************** */
/* Script para la entidad ConExpediente (Exp) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Consulta paginada del negocio ConExpedientes - ConExpediente (Exp)
-- ==========================================================================================
CREATE PROCEDURE NTExpExpCP
 @ProcesoOperativoId AS BIGINT,
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilExpedienteId AS BIGINT = NULL,
 @FilProcesoOperativoEstId AS BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NTExpedientes expe
        LEFT JOIN NCProcesosOperativosEst proopeest
            ON  proopeest.ProcesoOperativoEstId = expe.ProcesoOperativoEstId
        WHERE   expe.ProcesoOperativoId = @ProcesoOperativoId
                AND (@FilExpedienteId IS NULL
                    OR expe.ExpedienteId = @FilExpedienteId)
                AND (@FilProcesoOperativoEstId IS NULL
                    OR expe.ProcesoOperativoEstId = @FilProcesoOperativoEstId)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  expe.ProcesoOperativoId,
                expe.ExpedienteId,
                expe.ProcesoOperativoEstId,
                expe.UsuarioIdCreador,
                expe.FechaCreacion,
                expe.UsuarioIdUltMod,
                expe.FechaUltMod,
                proopeest.EstatusNombre,
                proopeest.PermiteModificar
        FROM NTExpedientes expe
        LEFT JOIN NCProcesosOperativosEst proopeest
            ON  proopeest.ProcesoOperativoEstId = expe.ProcesoOperativoEstId
        WHERE   expe.ProcesoOperativoId = @ProcesoOperativoId
                AND (@FilExpedienteId IS NULL
                    OR expe.ExpedienteId = @FilExpedienteId)
                AND (@FilProcesoOperativoEstId IS NULL
                    OR expe.ProcesoOperativoEstId = @FilProcesoOperativoEstId)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'ExpedienteId' THEN t.ExpedienteId END ASC,
        CASE WHEN @ColumnaOrden = '-ExpedienteId' THEN t.ExpedienteId END DESC,
        CASE WHEN @ColumnaOrden = 'EstatusNombre' THEN t.EstatusNombre END ASC,
        CASE WHEN @ColumnaOrden = '-EstatusNombre' THEN t.EstatusNombre END DESC
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
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Mantenimiento del negocio ConExpedientes - ConExpediente (Exp)
-- ==========================================================================================
CREATE PROCEDURE NTExpExpIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @ProcesoOperativoId AS BIGINT = NULL,
 @ExpedienteId AS BIGINT,
 @ProcesoOperativoEstId AS BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        EXEC PUGeneraConsecutivo 'NTExpedientes', 'ExpedienteId', @Consecutivo = @ExpedienteId OUT;

        BEGIN TRANSACTION
        BEGIN TRY

            INSERT INTO NTExpedientes
            VALUES (@ProcesoOperativoId,
                    @ExpedienteId,
                    ProcesoOperativoEstId,
                    @UsuarioIdSesion,
                    @vFecha,
                    @UsuarioIdSesion,
                    @vFecha);

            SELECT @ExpedienteId; --Ok

            INSERT INTO NTExpedientesObjetos
            VALUES (@ExpedienteId,
                    ExpedienteObjetoId,
                    ProcesoOperativoObjetoId,
                    '', --ArchivoNombre
                    '', --Ruta
                    1, --Activo
                    @UsuarioIdSesion,
                    @vFecha,
                    @UsuarioIdSesion,
                    @vFecha);

            SELECT @ExpedienteId; --Ok

            COMMIT TRANSACTION
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

             EXEC PUEnviaError
        END CATCH


    END ELSE IF @AccionBD = 2 BEGIN

        UPDATE NTExpedientes
        SET ProcesoOperativoId = @ProcesoOperativoId,
            UsuarioIdUltMod    = @UsuarioIdSesion,
            FechaUltMod        = @vFecha
        WHERE   ExpedienteId = @ExpedienteId;

    END ELSE IF @AccionBD = 3 BEGIN

        BEGIN TRANSACTION
        BEGIN TRY

            DELETE NTExpedientesValores
            WHERE   ExpedienteId = @ExpedienteId;

            DELETE NTExpedientesObjetos
            WHERE   ExpedienteId = @ExpedienteId;

            DELETE NTExpedientes
            WHERE   ExpedienteId = @ExpedienteId;

            COMMIT TRANSACTION
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

             EXEC PUEnviaError
        END CATCH


    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Accion personalizada del negocio ConExpedientes - ConExpediente (Exp)
-- ==========================================================================================
CREATE PROCEDURE NTExpExpCambioEstatusUno
 @UsuarioIdSesion AS BIGINT,
 --Adicionales
 @ExpedienteId AS BIGINT,
 @ProcesoOperativoEstId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    BEGIN TRANSACTION
    BEGIN TRY

        UPDATE NTExpedientes
        SET ProcesoOperativoEstId = @ProcesoOperativoEstId,
            UsuarioIdUltMod       = @UsuarioIdSesion,
            FechaUltMod           = @vFecha
        WHERE   ExpedienteId = @ExpedienteId;

        INSERT INTO NTExpedientesEstatus
        VALUES (@ExpedienteId,
                ExpedienteEstatusId,
                @ProcesoOperativoEstId,
                @UsuarioIdSesion,
                @vFecha);

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

         EXEC PUEnviaError
    END CATCH


END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Accion personalizada del negocio ConExpedientes - ConExpediente (Exp)
-- ==========================================================================================
CREATE PROCEDURE NTExpExpCambioEstatusDos
 @UsuarioIdSesion AS BIGINT,
 --Adicionales
 @ExpedienteId AS BIGINT,
 @ProcesoOperativoEstId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    BEGIN TRANSACTION
    BEGIN TRY

        UPDATE NTExpedientes
        SET ProcesoOperativoEstId = @ProcesoOperativoEstId,
            UsuarioIdUltMod       = @UsuarioIdSesion,
            FechaUltMod           = @vFecha
        WHERE   ExpedienteId = @ExpedienteId;

        INSERT INTO NTExpedientesEstatus
        VALUES (@ExpedienteId,
                ExpedienteEstatusId,
                @ProcesoOperativoEstId,
                @UsuarioIdSesion,
                @vFecha);

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

         EXEC PUEnviaError
    END CATCH


END
GO
/* ******************************************************************************** */
/* Script para la entidad ConExpedienteObjeto (Objs) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Consulta paginada del negocio ConExpedientes - ConExpedienteObjeto (Objs)
-- ==========================================================================================
CREATE PROCEDURE NTExpObjsCP
 @ExpedienteId AS BIGINT,
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NTExpedientesObjetos expobj
        INNER JOIN NCProcesosOperativosObjetos proopeobj
            ON  proopeobj.ProcesoOperativoObjetoId = expobj.ProcesoOperativoObjetoId
        WHERE   expobj.Activo = 1
                AND expobj.ExpedienteId = @ExpedienteId

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  expobj.ExpedienteId,
                expobj.ExpedienteObjetoId,
                expobj.ProcesoOperativoObjetoId,
                expobj.ArchivoNombre,
                expobj.Ruta,
                expobj.Activo,
                expobj.UsuarioIdCreador,
                expobj.FechaCreacion,
                expobj.UsuarioIdUltMod,
                expobj.FechaUltMod,
                proopeobj.ProcesoOperativoObjetoNombre,
                proopeobj.Cantidad,
                proopeobj.Orden,
                CAST('' AS VARCHAR(30)) AS ArchivoVencido,
                CAST('' AS VARCHAR(30)) AS Archivo,
                CAST('' AS VARCHAR(120)) AS NombreArchivo
        FROM NTExpedientesObjetos expobj
        INNER JOIN NCProcesosOperativosObjetos proopeobj
            ON  proopeobj.ProcesoOperativoObjetoId = expobj.ProcesoOperativoObjetoId
        WHERE   expobj.Activo = 1
                AND expobj.ExpedienteId = @ExpedienteId
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'FechaUltMod' THEN t.FechaUltMod END ASC,
        CASE WHEN @ColumnaOrden = '-FechaUltMod' THEN t.FechaUltMod END DESC,
        CASE WHEN @ColumnaOrden = 'Orden' THEN t.Orden END ASC,
        CASE WHEN @ColumnaOrden = '-Orden' THEN t.Orden END DESC,
        CASE WHEN @ColumnaOrden = 'NombreArchivo' THEN t.NombreArchivo END ASC,
        CASE WHEN @ColumnaOrden = '-NombreArchivo' THEN t.NombreArchivo END DESC
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
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Mantenimiento del negocio ConExpedientes - ConExpedienteObjeto (Objs)
-- ==========================================================================================
CREATE PROCEDURE NTExpObjsIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @ExpedienteId AS BIGINT,
 @ExpedienteObjetoId AS BIGINT,
 @ProcesoOperativoObjetoId AS BIGINT = NULL,
 @ArchivoNombre AS VARCHAR(200) = NULL,
 @Ruta AS VARCHAR(300) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();
    DECLARE @vWhere AS VARCHAR(2000) = 'ExpedienteId = ' + CAST(@ExpedienteId AS VARCHAR);

    IF @AccionBD = 1 BEGIN

        EXEC PUGeneraConsecutivoXGrupo 'NTExpedientesObjetos', 'ExpedienteObjetoId', @vWhere, @Consecutivo = @ExpedienteObjetoId OUT;

        INSERT INTO NTExpedientesObjetos
        VALUES (@ExpedienteId,
                @ExpedienteObjetoId,
                @ProcesoOperativoObjetoId,
                @ArchivoNombre,
                '', --Ruta
                1, --Activo
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT @ExpedienteObjetoId; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NTExpedientesObjetos
        WHERE   ExpedienteId = @ExpedienteId
                AND ExpedienteObjetoId = @ExpedienteObjetoId;

    END

END
GO
/* ******************************************************************************** */
/* Script para la entidad ExpedienteEstatu (ExpeEsta) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       06/09/2022 16:32:37
-- Descripcion: Consulta paginada del negocio ConExpedientes - ExpedienteEstatu (ExpeEsta)
-- ==========================================================================================
CREATE PROCEDURE NTExpExpeEstaCP
 @ExpedienteId AS BIGINT,
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NTExpedientesEstatus expest
        INNER JOIN NCProcesosOperativosEst proopeest
            ON  proopeest.ProcesoOperativoEstId = expest.ProcesoOperativoEstId
        INNER JOIN PTUsuarios usua
            ON  usua.UsuarioId = expest.UsuarioIdCreador
        WHERE   expest.ExpedienteId = @ExpedienteId

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  expest.ExpedienteId,
                expest.ExpedienteEstatusId,
                expest.ProcesoOperativoEstId,
                expest.UsuarioIdCreador,
                expest.FechaCreacion,
                proopeest.EstatusNombre,
                .Nombre,
                .ApellidoPaterno,
                .ApellidoMaterno
        FROM NTExpedientesEstatus expest
        INNER JOIN NCProcesosOperativosEst proopeest
            ON  proopeest.ProcesoOperativoEstId = expest.ProcesoOperativoEstId
        INNER JOIN PTUsuarios usua
            ON  usua.UsuarioId = expest.UsuarioIdCreador
        WHERE   expest.ExpedienteId = @ExpedienteId
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'FechaCreacion' THEN t.FechaCreacion END ASC,
        CASE WHEN @ColumnaOrden = '-FechaCreacion' THEN t.FechaCreacion END DESC,
        CASE WHEN @ColumnaOrden = 'EstatusNombre' THEN t.EstatusNombre END ASC,
        CASE WHEN @ColumnaOrden = '-EstatusNombre' THEN t.EstatusNombre END DESC,
        CASE WHEN @ColumnaOrden = 'Nombre' THEN t.Nombre END ASC,
        CASE WHEN @ColumnaOrden = '-Nombre' THEN t.Nombre END DESC,
        CASE WHEN @ColumnaOrden = 'ApellidoPaterno' THEN t.ApellidoPaterno END ASC,
        CASE WHEN @ColumnaOrden = '-ApellidoPaterno' THEN t.ApellidoPaterno END DESC,
        CASE WHEN @ColumnaOrden = 'ApellidoMaterno' THEN t.ApellidoMaterno END ASC,
        CASE WHEN @ColumnaOrden = '-ApellidoMaterno' THEN t.ApellidoMaterno END DESC
    OFFSET @LinIni - 1 ROWS
    FETCH NEXT @TamPag ROWS ONLY

END
GO
