function ConfiguraTipo(xCtrlTipo) {
    var vCtrlDecimales = document.getElementById('Decimales');
    var vCtrlConLongitud = document.getElementById('ConLongitud');

    document.getElementById('Decimales').style.display = SeDespliega(xCtrlTipo.value == 4);
    document.getElementById('ConLongitud').style.display = SeDespliega(xCtrlTipo.value == 1);

    //if (xCtrlTipo.value != 4) {
    //    vCtrlDecimales.readOnly = true;
    //    vCtrlDecimales.className = vCtrlDecimales.className.replace('estiloInputOpc', 'estiloInputDeshabilitado');
    //}
    //else {
    //    vCtrlDecimales.readOnly = false;
    //    vCtrlDecimales.className = vCtrlDecimales.className.replace('estiloInputDeshabilitado', 'estiloInputOpc');
    //}

    //if (xCtrlTipo.value != 1) {
    //    vCtrlConLongitud.readOnly = true;
    //    vCtrlConLongitud.className = vCtrlDecimales.className.replace('estiloInputOpc', 'estiloInputDeshabilitado');
    //}
    //else {
    //    vCtrlConLongitud.readOnly = false;
    //    vCtrlConLongitud.className = vCtrlDecimales.className.replace('estiloInputDeshabilitado', 'estiloInputOpc');
    //}

    document.getElementById('CapRangoIniTexto').style.display = SeDespliega(xCtrlTipo.value == 1);
    document.getElementById('CapRangoIniEntero').style.display = SeDespliega(xCtrlTipo.value == 3);
    document.getElementById('CapRangoIniImporte').style.display = SeDespliega(xCtrlTipo.value == 4);
    document.getElementById('CapRangoIniFecha').style.display = SeDespliega(xCtrlTipo.value == 5 || xCtrlTipo.value == 6);
    if (document.getElementById('PCapRangoIniFechaBtn') != undefined)
        document.getElementById('PCapRangoIniFechaBtn').style.display = SeDespliega(xCtrlTipo.value == 5 || xCtrlTipo.value == 6);

    document.getElementById('CapRangoFinTexto').style.display = SeDespliega(xCtrlTipo.value == 1);
    document.getElementById('CapRangoFinEntero').style.display = SeDespliega(xCtrlTipo.value == 3);
    document.getElementById('CapRangoFinImporte').style.display = SeDespliega(xCtrlTipo.value == 4);
    document.getElementById('CapRangoFinFecha').style.display = SeDespliega(xCtrlTipo.value == 5 || xCtrlTipo.value == 6);
    if (document.getElementById('PCapRangoFinFechaBtn') != undefined)
        document.getElementById('PCapRangoFinFechaBtn').style.display = SeDespliega(xCtrlTipo.value == 5 || xCtrlTipo.value == 6);
}
function EstableceCapRangoIni(xCtrl) {
    var vCtrlCapRangoIni = document.getElementById('CapRangoIni');
    vCtrlCapRangoIni.value = xCtrl.value;
}
function EstableceCapRangoFin(xCtrl) {
    var vCtrlCapRangoFin = document.getElementById('CapRangoFin');
    vCtrlCapRangoFin.value = xCtrl.value;
}
function SeDespliega(valor) {
    if (valor) {
        return null;
    }
    else {
        return "none";
    }
}