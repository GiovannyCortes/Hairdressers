function ScheduleBuilder(scheduleCounter, startTime = "08:30", endTime = "14:30", checkedDays = [true, true, true, true, true, false, false], scheduleRowId = 0) {
    if ($('div[name^="schedule_"]').length <= 14) {
        // Crear el nuevo elemento div y agregar las clases correspondientes
        var newSchedule = $("<div>").addClass("form-schedule").attr("name", "schedule_" + scheduleCounter).attr("id", "schedule_" + scheduleCounter);

        // Crear los elementos dentro del nuevo elemento div
        var timesDiv = $("<div>").addClass("form-schedule-times");
        var startLabel = $("<label>").addClass("no_select").text("Apertura");
        var startInput = $("<input>").addClass("form-startTime form-time").attr("type", "time").attr("value", startTime);
        var endInput = $("<input>").addClass("form-endTime form-time").attr("type", "time").attr("value", endTime);
        var endLabel = $("<label>").addClass("no_select").text("Cierre");
        timesDiv.append(startLabel, startInput, endInput, endLabel);

        var daysDiv = $("<div>").addClass("form-schedule-days");
        var daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
        var daysOfWeekES = ["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"];
        for (var i = 0; i < daysOfWeek.length; i++) {
            var day = daysOfWeek[i];
            var dayES = daysOfWeekES[i];
            var checkboxDiv = $("<div>").addClass("checkbox");
            var checkboxInput = $("<input>").attr("type", "checkbox").attr("id", day + "_" + scheduleCounter).addClass("checkbox-flip").attr("value", day);
            checkboxInput.prop("checked", checkedDays[i]);
            var checkboxLabel = $("<label>").attr("for", day + "_" + scheduleCounter).html("<span></span>" + dayES.substr(0, 1));
            checkboxDiv.append(checkboxInput, checkboxLabel);
            daysDiv.append(checkboxDiv);
        }

        // Agregar los elementos al nuevo elemento div y agregar el nuevo elemento al contenedor
        var closeIcon = $("<i>").addClass("fa-sharp fa-solid fa-circle-xmark close_icon").data("parent-box-id", scheduleCounter);
        closeIcon.data("scheduleRowId", scheduleRowId);
        closeIcon.on("click", function () {
            var current_scheduleRowId = $(this).data("scheduleRowId");
            if (current_scheduleRowId !== 0) {
                Swal.fire({
                    title: "¿Está segur@ de querer eliminar esta fila?",
                    text: "Este registro está guardado y asociado a su horario actual. Su eliminación es irreversible",
                    icon: 'warning',
                    showcancelButton: true,
                    cancelButtonText: 'Cancelar',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Sí, eliminar registro'
                }).then((result) => {
                    if (result.isConfirmed()) {
                        $.post("/Hairdresser/DeleteScheduleRow", { scheduleRow_id: current_scheduleRowId }, function (data) {
                            Swal.fire({
                                title: "Registro eliminado",
                                icon: 'success',
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'De acuerdo'
                            });
                        });
                    }
                })
            }

            // ==================== Dibujo ====================
            var parentBoxId = $(this).data("parent-box-id");
            $("#schedule_" + parentBoxId).remove();
            if ($('div[name^="schedule_"]').length == 13) {
                $("#form-schedule-add").show();
            }
        });

        newSchedule.append(timesDiv, closeIcon, daysDiv);
        $("#schedules_container").append(newSchedule);

        if ($('div[name^="schedule_"]').length >= 14) {
            $("#form-schedule-add").hide();
        }
    }
};


//function ScheduleBuilder(scheduleCounter) {
//    if ($('div[name^="schedule_"]').length <= 14) {
//        // Crear el nuevo elemento div y agregar las clases correspondientes
//        var newSchedule = $("<div>").addClass("form-schedule").attr("name", "schedule_" + scheduleCounter).attr("id", "schedule_" + scheduleCounter);
        
//        // Crear los elementos dentro del nuevo elemento div
//        var timesDiv = $("<div>").addClass("form-schedule-times");
//        var startLabel = $("<label>").addClass("no_select").text("Apertura");
//        var startInput = $("<input>").addClass("form-startTime form-time").attr("type", "time").attr("value", "08:30");
//        var endInput = $("<input>").addClass("form-endTime form-time").attr("type", "time").attr("value", "14:30");
//        var endLabel = $("<label>").addClass("no_select").text("Cierre");
//        timesDiv.append(startLabel, startInput, endInput, endLabel);

//        var daysDiv = $("<div>").addClass("form-schedule-days");
//        var daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
//        var daysOfWeekES = ["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"];
//        for (var i = 0; i < daysOfWeek.length; i++) {
//            var day = daysOfWeek[i];
//            var dayES = daysOfWeekES[i];
//            var checkboxDiv = $("<div>").addClass("checkbox");
//            var checkboxInput = $("<input>").attr("type", "checkbox").attr("id", day + "_" + scheduleCounter).addClass("checkbox-flip").attr("value", day);
//            checkboxInput.prop("checked", (i <= 4));
//            var checkboxLabel = $("<label>").attr("for", day + "_" + scheduleCounter).html("<span></span>" + dayES.substr(0, 1));
//            checkboxDiv.append(checkboxInput, checkboxLabel);
//            daysDiv.append(checkboxDiv);
//        }

//        // Agregar los elementos al nuevo elemento div y agregar el nuevo elemento al contenedor
//        var closeIcon = $("<i>").addClass("fa-sharp fa-solid fa-circle-xmark close_icon").data("parent-box-id", scheduleCounter);
//        closeIcon.on("click", function () {
//            var parentBoxId = $(this).data("parent-box-id");
//            $("#schedule_" + parentBoxId).remove();
//            if ($('div[name^="schedule_"]').length == 13) {
//                $("#form-schedule-add").show();
//            }
//        });

//        newSchedule.append(timesDiv, closeIcon, daysDiv);
//        $("#schedules_container").append(newSchedule);

//        if ($('div[name^="schedule_"]').length >= 14) {
//            $("#form-schedule-add").hide();
//        }

//    }
//};