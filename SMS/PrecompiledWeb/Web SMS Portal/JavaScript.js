function CheckAllClick(tempHidden, HiddenAllId, checkAllBox) {
    var checkall_status = checkAllBox.checked;
    tempHidden.value = checkall_status ? HiddenAllId.value : "";
    $(":checkbox").each(function() {
        this.checked = checkall_status;
    });
}
function CheckBox_Click(tempHidden, currChecked, idRow, checkAllBox, checkAllIdHidden) {
    if (currChecked.checked) {
        if (tempHidden.value != "")
            tempHidden.value += "," + idRow;
        else
            tempHidden.value = idRow;
    } else {
        var resultHidden = "";
        splitHidden = tempHidden.value.split(",");
        if (splitHidden.length > 1) {
            for (i = 0; i < splitHidden.length; i++) {
                if (idRow != splitHidden[i])
                    resultHidden += "," + splitHidden[i];
            }
            tempHidden.value = resultHidden.substring(1);
        }
        else
            tempHidden.value = "";
    }
    checkAllBox.checked = (checkAllIdHidden.value.split(",").length == tempHidden.value.split(",").length) && (tempHidden.value != "");
}