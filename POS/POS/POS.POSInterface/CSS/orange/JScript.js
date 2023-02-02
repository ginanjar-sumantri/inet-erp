//NB: Numeric with Enter.
function NumericWithEnter() {
    //alert(event.keyCode);
    var _result = false;


    if (event.altKey || event.ctrlKey || event.shiftKey) {

    } else if (/^[0-9]+$/.test(event.value)) {
    } else {
        if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 9 || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 13) {
            _result = true;
        }
        else {
            _result = false;
        }
    }

    return _result;
}

function formatangka(objek) {
    a = objek.value;
    b = a.replace(/[^\d]/g, "");
    c = "";
    panjang = b.length;
    j = 0;
    for (i = panjang; i > 0; i--) {
        j = j + 1;

        c = b.substr(i - 1, 1) + c;
    }
    objek.value = c;
}

function formatangkawithplus(objek) {
    isi = objek.value;
    if (isNaN(isi.charAt(isi.length - 1)))
        if (isi.charAt(isi.length - 1) != "+")
        objek.value = "";
}

function ChangeFormat(_prmTextBox) {
    _prmTextBox.value = FormatCurrency(_prmTextBox.value);
}

function ChangeFormat2(_prmTextBox, _prmDecimalPlace) {
    _prmTextBox.value = FormatCurrency2(_prmTextBox.value, _prmDecimalPlace.value);
}

function GetCurrency(_prmCurrency) {
    var _result;

    var _decimal = 100;

    _prmCurrency = _prmCurrency.toString().replace(/\$|\,/g, '');

    _result = Math.round(_prmCurrency * _decimal) / _decimal

    return _result;
}

function GetCurrency2(_prmCurrency, _prmDecimalPlace) {
    var _result;



    if (isNaN(_prmDecimalPlace) || _prmDecimalPlace == "") {
        _prmDecimalPlace = "100";
    }

    var _decimal = _prmDecimalPlace;

    _prmCurrency = _prmCurrency.toString().replace(/\$|\,/g, '');

    _result = Math.round(_prmCurrency * _decimal) / _decimal

    return _result;
}

//function window.confirm(_prmMessage)
//{
//    //4132
////    execScript('n = msgbox("' + _prmMessage + '", "4", "Confirmation")', "vbscript");
////    return(n == 6);
//}

function FormatCurrency2(_prmCurrency, _prmDecimalPlace) {
    var _result;

    if (isNaN(_prmDecimalPlace) || _prmDecimalPlace == "") {
        _prmDecimalPlace = "100";
    }

    var _decimal = _prmDecimalPlace;

    _prmCurrency = GetCurrency2(_prmCurrency, _decimal);

    if (isNaN(_prmCurrency)) {
        _prmCurrency = "0";
    }

    var _sign = (_prmCurrency == (_prmCurrency = Math.abs(_prmCurrency)));
    _prmCurrency = Math.floor(_prmCurrency * _decimal + 0.50000000001);

    var _cents = _prmCurrency % _decimal;

    _prmCurrency = Math.floor(_prmCurrency / _decimal).toString();

    _cents = LeftPad(_cents, _prmDecimalPlace.length - 1, "0");

    for (var i = 0; i < Math.floor((_prmCurrency.length - (1 + i)) / 3); i++) {
        _prmCurrency = _prmCurrency.substring(0, _prmCurrency.length - (4 * i + 3)) + ',' + _prmCurrency.substring(_prmCurrency.length - (4 * i + 3));
    }

    _result = (((_sign) ? '' : '-') + _prmCurrency + '.' + _cents);

    return _result;
}

function LeftPad(num, totalChars, padWith) {
    num = num + "";
    padWith = (padWith) ? padWith : "0";
    if (num.length < totalChars) {
        while (num.length < totalChars) {
            num = padWith + num;
        }
    } else { }
    if (num.length > totalChars) { //if padWith was a multiple character string and num was overpadded
        num = num.substring((num.length - totalChars), totalChars);
    } else { }
    return num;
}

function FormatCurrency(_prmCurrency) {
    var _result;

    var _decimal = 100;

    _prmCurrency = GetCurrency(_prmCurrency);

    if (isNaN(_prmCurrency)) {
        _prmCurrency = "0";
    }

    var _sign = (_prmCurrency == (_prmCurrency = Math.abs(_prmCurrency)));
    _prmCurrency = Math.floor(_prmCurrency * _decimal + 0.50000000001);
    var _cents = _prmCurrency % _decimal;
    _prmCurrency = Math.floor(_prmCurrency / _decimal).toString();

    if (_cents < 10) {
        _cents = "0" + _cents;
    }

    for (var i = 0; i < Math.floor((_prmCurrency.length - (1 + i)) / 3); i++) {
        _prmCurrency = _prmCurrency.substring(0, _prmCurrency.length - (4 * i + 3)) + ',' + _prmCurrency.substring(_prmCurrency.length - (4 * i + 3));
    }

    _result = (((_sign) ? '' : '-') + _prmCurrency + '.' + _cents);

    return _result;
}

//NB: Numeric with Backspace and Tab.
function NumericWithTab() {
    var _result = false;

    if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 9 || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 46 || event.keyCode == 37 || event.keyCode == 39) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

//NB: Numeric with Backspace.
function Numeric() {
    var _result = false;

    if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 9 || (event.keyCode >= 96 && event.keyCode <= 105)) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

//NB: Numeric with Backspace and dot.
function NumericWithDot(_prmNumber) {
    if (event.keyCode == 110 || event.keyCode == 190 || (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 9 || (event.keyCode >= 96 && event.keyCode <= 105)) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

//NB: Numeric with Backspace,tab, and dot.
function NumericWithDotAndTab() {
    var _result = false;

    if (event.keyCode == 110 || event.keyCode == 8 || event.keyCode == 190 || (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 9 || (event.keyCode >= 96 && event.keyCode <= 105)) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

//NB: All Alphabets with Backspace & space.
function Alphabetic() {
    var _result = false;

    if ((event.keyCode >= 97 && event.keyCode <= 122) || (event.keyCode >= 65 && event.keyCode <= 90) || event.keyCode == 8 || event.keyCode == 32 || event.keyCode == 9) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

//NB: AlphaNumeric with Backspace.
function AlphaNumeric() {
    var _result = false;

    if ((event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode >= 97 && event.keyCode <= 122) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 9) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

//NB: AlphaNumeric with Backspace & space.
function AlphaNumericWithSpace() {
    var _result = false;

    if ((event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode >= 97 && event.keyCode <= 122) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || event.keyCode == 32 || event.keyCode == 9) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result;
}

function IsContains(_prmHidden, _prmSeparator, _prmValue) {
    var _result = false;
    var _split = _prmHidden.value.toString().split(_prmSeparator);

    for (j = 0; j < _split.length; j++) {
        if (_split[j] == _prmValue) {
            _result = true;
        }
    }

    return _result;
}

function CheckBox_ClickAll(_prmCheckBoxAll, _prmHidden, _prmCheckBoxPrefix, _prmCheckBoxSuffix, _prmTempHidden, _prmNoPadLeft) {
    var _tempMiddle = 0;
    var _middle;
    var _all;
    var _element;
    var _tempHidden;
    var _loop = true;

    _tempHidden = document.getElementById(_prmTempHidden);

    if (_prmNoPadLeft == null && _prmNoPadLeft != 'true' && _prmNoPadLeft != 'false') {
        _prmNoPadLeft = 'false';
    }

    while (_loop == true) {
        if (_tempMiddle < 10 && _prmNoPadLeft == 'false') {
            _middle = "0" + _tempMiddle;
        }
        else {
            _middle = _tempMiddle;
        }

        _all = _prmCheckBoxPrefix + _middle + _prmCheckBoxSuffix;
        _element = document.getElementById(_all);

        if (_element == null) {
            _loop = false;
        }
        else {
            if (_prmCheckBoxAll.checked == true) {
                _element.checked = true;

                var _splitTempHidden = _tempHidden.value.toString().split(',');

                for (i = 0; i < _splitTempHidden.length; i++) {
                    if (IsContains(_prmHidden, ',', _splitTempHidden[i]) == false) {
                        if (_prmHidden.value == "") {
                            _prmHidden.value = _splitTempHidden[i];
                        }
                        else {
                            _prmHidden.value += ',' + _splitTempHidden[i];
                        }
                    }
                }
                //_prmHidden.value = _tempHidden.value;
            }
            else if (_prmCheckBoxAll.checked == false) {
                _element.checked = false;

                var _splitTempHidden = _tempHidden.value.toString().split(',');

                for (i = 0; i < _splitTempHidden.length; i++) {
                    if (IsContains(_prmHidden, ',', _splitTempHidden[i]) == true) {
                        if (_prmHidden.value == _splitTempHidden[i]) {
                            _prmHidden.value = (_prmHidden.value).replace(_splitTempHidden[i], "");
                        }
                        else {
                            var _splitPrmHidden = _prmHidden.value.toString().split(',');

                            if (_splitPrmHidden[0] == _splitTempHidden[i]) {
                                _prmHidden.value = (_prmHidden.value).replace(_splitTempHidden[i] + ",", "");
                            }
                            else {
                                _prmHidden.value = (_prmHidden.value).replace("," + _splitTempHidden[i], "");
                            }
                        }
                    }
                }
                //_prmHidden.value = "";
            }
        }

        _tempMiddle += 1;
    }
}

function CheckBox_ClickAll_2Hidden(_prmCheckBoxAll, _prmHidden2, _prmHidden3, _prmCheckBoxPrefix, _prmCheckBoxSuffix, _prmTempHidden, _prmNoPadLeft) {
    var _tempMiddle = 0;
    var _middle;
    var _all;
    var _element;
    var _tempHidden;
    var _loop = true;

    _tempHidden = document.getElementById(_prmTempHidden);

    if (_prmNoPadLeft == null && _prmNoPadLeft != 'true' && _prmNoPadLeft != 'false') {
        _prmNoPadLeft = 'false';
    }

    while (_loop == true) {
        if (_tempMiddle < 10 && _prmNoPadLeft == 'false') {
            _middle = "0" + _tempMiddle;
        }
        else {
            _middle = _tempMiddle;
        }

        _all = _prmCheckBoxPrefix + _middle + _prmCheckBoxSuffix;
        _element = document.getElementById(_all);

        if (_element == null) {
            _loop = false;
        }
        else {
            if (_prmCheckBoxAll.checked == true) {
                _element.checked = true;

                var _splitTempHidden = _tempHidden.value.toString().split(',');

                for (i = 0; i < _splitTempHidden.length; i++) {
                    if (IsContains(_prmHidden3, ',', _splitTempHidden[i]) == false) {
                        if (_prmHidden3.value == "") {
                            _prmHidden3.value = _splitTempHidden[i];
                        }
                        else {
                            _prmHidden3.value += ',' + _splitTempHidden[i];
                        }
                    }
                }
                //_prmHidden.value = _tempHidden.value;
            }
            else if (_prmCheckBoxAll.checked == false) {
                _element.checked = false;

                var _splitTempHidden = _tempHidden.value.toString().split(',');

                for (i = 0; i < _splitTempHidden.length; i++) {
                    //if (IsContains(_prmHidden2, ',', _splitTempHidden[i]) == true) {
                    //                        if (_prmHidden2.value == _splitTempHidden[i]) {
                    //                            _prmHidden2.value = (_prmHidden2.value).replace(_splitTempHidden[i], "");
                    //                        }
                    //                        else {
                    //                            var _splitPrmHidden = _prmHidden2.value.toString().split(',');

                    //                            if (_splitPrmHidden[0] == _splitTempHidden[i]) {
                    //                                _prmHidden2.value = (_prmHidden2.value).replace(_splitTempHidden[i] + ",", "");
                    //                            }
                    //                            else {
                    //                                _prmHidden2.value = (_prmHidden2.value).replace("," + _splitTempHidden[i], "");
                    //                            }
                    //                        }
                    if (IsContains(_prmHidden2, ',', _splitTempHidden[i]) == false) {
                        if (_prmHidden2.value == "") {
                            _prmHidden2.value = _splitTempHidden[i];
                        }
                        else {
                            _prmHidden2.value += ',' + _splitTempHidden[i];
                        }
                    }
                    //}
                }
                //_prmHidden.value = "";
                _prmHidden3.value = "";
            }
        }

        _tempMiddle += 1;
    }
}

function IsCheckedAll(_prmCheckBoxPrefix, _prmCheckBoxSuffix, _prmNoPadLeft) {
    var _tempMiddle = 0;
    var _middle;
    var _element;
    var _loop = true;
    var _result = 1;

    if (_prmNoPadLeft == null && _prmNoPadLeft != 'true' && _prmNoPadLeft != 'false') {
        _prmNoPadLeft = 'false';
    }

    while (_loop == true) {
        if (_tempMiddle < 10 && _prmNoPadLeft == 'false') {
            _middle = "0" + _tempMiddle;
        }
        else {
            _middle = _tempMiddle;
        }

        _all = _prmCheckBoxPrefix + _middle + _prmCheckBoxSuffix;
        _element = document.getElementById(_all);

        if (_element == null) {
            _loop = false;
        }
        else {
            if (_element.checked == false) {
                _result = 0;
                _loop = false;
            }

        }

        _tempMiddle += 1;
    }

    return _result;
}

function AskYouFirst() {
    var _result = false;

    if (confirm("Are you sure want to delete?") == true) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result
}

function Confirmation(_prmMessage) {
    var _result = false;

    if (confirm(_prmMessage) == true) {
        _result = true;
    }
    else {
        _result = false;
    }

    return _result
}

function AskYouFirstToSave(_prmHiddenField) {
    if (confirm("Do you want to save?") == true) {
        _prmHiddenField.value = "y";
    }
    else {
        _prmHiddenField.value = "n";
    }
}

function CheckBox_Click(_prmHidden, _prmCheckBox, _prmCheckBoxValue, _prmCheckBoxPrefix, _prmCheckBoxSuffix, _prmCheckBoxAll, _prmNoPadLeft) {
    var _checkBoxAll = document.getElementById(_prmCheckBoxAll);

    if (_prmNoPadLeft == null && _prmNoPadLeft != 'true' && _prmNoPadLeft != 'false') {
        _prmNoPadLeft = 'false';
    }

    if (IsCheckedAll(_prmCheckBoxPrefix, _prmCheckBoxSuffix, _prmNoPadLeft) == 1) {
        _checkBoxAll.checked = true;
    } else {
        _checkBoxAll.checked = false;
    }

    if (_prmCheckBox.checked == true) {
        if (_prmHidden.value == "") {
            _prmHidden.value = _prmCheckBoxValue;
        }
        else {
            _prmHidden.value += ',' + _prmCheckBoxValue;
        }
    }
    else {
        var _str = _prmHidden.value;
        var _strSplit = _str.split(",");

        if (_prmHidden.value.length == 1 || _strSplit[0] == _prmCheckBoxValue) {
            _prmHidden.value = (_prmHidden.value).replace(_prmCheckBoxValue, "");
        }
        else {
            _prmHidden.value = (_prmHidden.value).replace("," + _prmCheckBoxValue, "");
        }
    }

    if (_prmHidden.value.substring(0, 1) == ",") {
        _prmHidden.value = _prmHidden.value.substring(1, _prmHidden.value.length);
    }
}

function CheckBoxByHidden_Click(_prmHidden1, _prmHidden2, _prmHidden3, _prmCheckBox, _prmCheckBoxValue, _prmCheckBoxPrefix, _prmCheckBoxSuffix, _prmCheckBoxAll) {
    var _result = false;
    var _result2 = false;

    var _checkBoxAll = document.getElementById(_prmCheckBoxAll);

    if (IsCheckedAll(_prmCheckBoxPrefix, _prmCheckBoxSuffix) == 1) {
        _checkBoxAll.checked = true;
    } else {
        _checkBoxAll.checked = false;
    }

    if (_prmCheckBox.checked == true) {
        var indexHidden1 = _prmHidden1.value.indexOf(_prmCheckBoxValue);

        if (indexHidden1 == -1) {
            if (_prmHidden3.value == "") {
                _prmHidden3.value = _prmCheckBoxValue;
            }
            else if (_prmHidden3.value != "") {
                _prmHidden3.value += ',' + _prmCheckBoxValue;
            }
        }
        else {
            var indexHidden2 = _prmHidden2.value.indexOf(_prmCheckBoxValue);

            if (indexHidden2 != -1) {
                if (indexHidden2 == 0) {
                    _prmHidden2.value = (_prmHidden2.value).replace(_prmCheckBoxValue, "");
                }
                else {
                    _prmHidden2.value = (_prmHidden2.value).replace("," + _prmCheckBoxValue, "");
                }
            }

        }
    }
    else {
        var indexHidden1 = _prmHidden1.value.indexOf(_prmCheckBoxValue);

        if (indexHidden1 != -1) {
            if (_prmHidden2.value == "") {
                _prmHidden2.value = _prmCheckBoxValue;
            }
            else if (_prmHidden2.value != "") {
                _prmHidden2.value += ',' + _prmCheckBoxValue;
            }
        }
        else {
            var indexHidden3 = _prmHidden3.value.indexOf(_prmCheckBoxValue);

            if (indexHidden3 != -1) {
                if (indexHidden3 == 0) {
                    _prmHidden3.value = (_prmHidden3.value).replace(_prmCheckBoxValue, "");
                }
                else {
                    _prmHidden3.value = (_prmHidden3.value).replace("," + _prmCheckBoxValue, "");
                }
            }
        }
    }
}

function ShowFormAddDetail(_prmUrl, _prmWidth, _prmheight, _prmResizable, _prmMenubar) {
    window.open(_prmUrl, "mywindow", "width=" + _prmWidth + ", height=" + _prmheight + ", resizable=" + _prmResizable + ", menubar=" + _prmMenubar);
}

function ShowPopUp(_prmUrl, _prmWidth, _prmheight, _prmResizable, _prmMenubar) {
    window.open(_prmUrl, "mywindow", "width=" + _prmWidth + ", height=" + _prmheight + ", resizable=" + _prmResizable + ", menubar=" + _prmMenubar);
}


function DropDownValidation(source, arguments) {
    if (arguments.Value != "null") {
        arguments.IsValid = true;
    }
    else {
        arguments.IsValid = false;
    }
}


//function PlanShipLoadingValidationDDL(source, arguments)
//{
//    var loading = document.getElementById("ctl00_DefaultBodyContentPlaceHolder_AmountTextBoxt");
//    
//    if(loading.value != "0"  && arguments.Value == "null"){
//        arguments.IsValid = false;        
//    }else{
//       arguments.IsValid = true;        
//    }
//}

//function PlanShipLoadingValidationTextBox(source, arguments)
//{
//    var loading = document.getElementById("ctl00_DefaultBodyContentPlaceHolder_AmountTextBoxt");
//    
//    if(loading.value != "0"  && arguments.Value == "0"){
//        arguments.IsValid = false;        
//    }else{
//       arguments.IsValid = true;        
//    }
//}

//function PlanShipLoadingValidationDateTime(source, arguments)
//{
//    var loading = document.getElementById("ctl00_DefaultBodyContentPlaceHolder_AmountTextBoxt");
//    
//    if(loading.value != "0"  && arguments.value == ""){
//        arguments.IsValid = false;        
//    }else{
//       arguments.IsValid = true;        
//    }
//}

function CloseWindow() {
    window.close();
}


function CloseAndRefreshParent() {
    opener.location.reload(true);
    self.close();
}

function ListAllCheckBox_check(checkbox, listbox) {
    for (var i = 0; i < listbox.length; i++) {
        listbox.options[i].selected = !listbox.options[i].selected;
    }
}

function trim(stringToTrim) {
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}

function ConfirmFillDescription(_prmDescriptionHiddenField, _prmTitle) {
    var _result = false;
    var _descriptionHiddenField = document.getElementById(_prmDescriptionHiddenField);

    var _description = prompt(_prmTitle, "");

    if (_description != null) {
        if (trim(_description) != "" && _description.length <= 500) {
            _descriptionHiddenField.value = _description;
            _result = true;
        }
        else {
            _result = false;
        }
    }
    else {
        _result = false;
    }

    return _result
}

function textCounter(field, cntfield, maxlimit) {
    if (field.value.length > maxlimit) {
        field.value = field.value.substring(0, maxlimit);
        field.value.length = maxlimit;
    }
    else if (field.value.length <= maxlimit) {
        cntfield.value = maxlimit - field.value.length;
    }
}

function textCounterWithOutCounterTextBox(field, maxlimit) {
    if (field.value.length > maxlimit) {
        field.value = field.value.substring(0, maxlimit);
        field.value.length = maxlimit;
    }
}