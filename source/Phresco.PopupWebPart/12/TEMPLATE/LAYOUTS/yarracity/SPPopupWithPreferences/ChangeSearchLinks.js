/*
 * PHR_SharePointResourceManagement
 *
 * Copyright (C) 1999-2013 Photon Infotech Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
﻿/***********************************************************************
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


    $(document).ajaxError(function(e, xhr, settings, exception) {
        $(this).append("Error requesting page [" + xhr.responseText + "]");
    });

    var soapBody = constructReturnSoapBody('Search Improvements Preferences', 'KeepUsingNewSearch');

    $.ajaxSetup({ contentType: "application/soap+xml; charset=utf-8" });

    $.post("/_layouts/UpdateUserPreference.asmx",
        soapBody,
        function(data) {
            var pref = $(data).text();
            if (pref == "True") {
                changeSearchLinks();
            }
        }
    );

});

function changeSearchLinks() {
    $("a:contains('Staff Directory')").attr("href", "http://search.yarra.int/Pages/people.aspx");
    $("a:contains('Search')").attr("href", "http://search.yarra.int/");
    $("#WebPartWPQ3").html($("#WebPartWPQ3").html().replace(/\/searchcenter\/Pages\//g, "http://search.yarra.int/Pages/"));
    $("#WebPartWPQ3").css('background-color', '#fffff4');
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