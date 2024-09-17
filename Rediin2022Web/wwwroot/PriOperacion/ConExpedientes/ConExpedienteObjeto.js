function ViewDocument(indice) {
	$("#loader").fadeIn();
	var data = { indice: indice };
	$.ajax({
		type: "POST",
		url: '/PriOperacion/ConExpedientes/ConExpedienteObjetoDescarga2',
		data: data,
		dataType: "text"
	}).done(function (data) {
		$("#content").empty();
		for (var i = 1; i <= data; i++) {
			$("#content").append("<img style='width:100%' src='/PriOperacion/ConExpedientes/ConExpedienteObjetoDescargaImg?indice=" + indice + "&pagina=" + i + "'/>");
		}
		$("#loader").fadeOut();
	})
}