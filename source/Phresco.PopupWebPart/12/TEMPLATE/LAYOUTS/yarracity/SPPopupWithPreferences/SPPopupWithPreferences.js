/***********************************************************************
    Copyright 2011 Mark Powney

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
**************************************************************************/

$(document).ready(function() {


    $("#userPopup").ajaxError(function(e, xhr, settings, exception) {
        $(this).append("Error requesting page [" + xhr.responseText + "]");
        $("#userPopup").show();
        alert(exception);
    });


    var soapBody = constructReturnSoapBody('Search Improvements Preferences', 'DontShowPopup');

    $.ajaxSetup({ contentType: "application/soap+xml; charset=utf-8" });

    $.post("/_layouts/UpdateUserPreference.asmx",
        soapBody,
        function(data) {
            var pref = $(data).text();
            if (pref != "True") {

                $("#SRSB").append($("#userPopup"));
                $("#userPopup").delay(3000).fadeIn(1000);
            }
        }
    );

});

function dontAskMeAboutSearchAgain() {

    var soapBody = constructUpdateSoapBody('Search Improvements Preferences', 'DontShowPopup', 'true');

    $.post("/_layouts/UpdateUserPreference.asmx",
        soapBody,
        function(data) {
            closePopup();
        }
    );
}

function applyNewSearchPermanently() {

    var soapBodyPopup = constructUpdateSoapBody('Search Improvements Preferences', 'DontShowPopup', 'true');
    var soapBodyKeepUsing = constructUpdateSoapBody('Search Improvements Preferences', 'KeepUsingNewSearch', 'true');

    $.post("/_layouts/UpdateUserPreference.asmx",
        soapBodyPopup,
        function(data) {
            $.post("/_layouts/UpdateUserPreference.asmx",
            soapBodyKeepUsing,
            function(data) {
                changeSearchURLs();
                closePopup();
            });
        });
}

function closePopup() {
    $("#userPopup").hide();
}
function changeSearchURLs() {
    $("a:contains('Staff Directory')").attr("href", "http://search.yarra.int/Pages/people.aspx");
    $("a:contains('Search')").attr("href", "http://search.yarra.int/");
    $("#WebPartWPQ3").html($("#WebPartWPQ3").html().replace(/\/searchcenter\/Pages\//g, "http://search.yarra.int/Pages/"));
    $("#WebPartWPQ3").css('background-color', '#fffff4');
}


function constructUpdateSoapBody(listName, fieldName, preferenceValue) {
    var soapBody = '<?xml version="1.0" encoding="utf-8"?>\n'
            + '<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\n'
            + '  <soap12:Body>\n'
            + '    <UpdatePreference xmlns="http://intranet.yarra.int/">\n'
            + '      <listName>' + listName + '</listName>\n'
            + '      <fieldName>' + fieldName + '</fieldName>\n'
            + '      <preferenceValue>' + preferenceValue + '</preferenceValue>\n'
            + '    </UpdatePreference>\n'
            + '  </soap12:Body>\n'
            + '</soap12:Envelope>\n';

    return soapBody;
}

function constructReturnSoapBody(listName, fieldName) {
    var soapBody = '<?xml version="1.0" encoding="utf-8"?>\n'
            + '<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\n'
            + '  <soap12:Body>\n'
            + '    <ReturnPreference xmlns="http://intranet.yarra.int/">\n'
            + '      <listName>' + listName + '</listName>\n'
            + '      <fieldName>' + fieldName + '</fieldName>\n'
            + '    </ReturnPreference>\n'
            + '  </soap12:Body>\n'
            + '</soap12:Envelope>\n';

    return soapBody;
}