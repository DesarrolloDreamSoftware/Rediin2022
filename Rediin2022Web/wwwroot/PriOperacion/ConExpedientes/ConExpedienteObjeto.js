function ViewDocument(i) {
	$("#loader").fadeIn();
	var data = { indice: i };
	$.ajax({
		type: "POST",
		url: '/ConExpedientes/ConExpedienteObjetoDescarga2',
		data: data,
		dataType: "text"
	}).done(function (data) {
		var folderName = file.replace(".", "_");
		$("#content").empty();
		for (var i = 1; i <= data; i++) {
			$("#content").append("<img src='Content/" + folderName + "/page-" + i + ".png'/>");
		}
		$("#loader").fadeOut();
	})
}