function parse_query_string(query) {
    var vars = query.split("&");
    var query_string = {};
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        var key = decodeURIComponent(pair[0]);
        var value = decodeURIComponent(pair[1]);
        // If first entry with this name
        if (typeof query_string[key] === "undefined") {
            query_string[key] = decodeURIComponent(value);
            // If second entry with this name
        } else if (typeof query_string[key] === "string") {
            var arr = [query_string[key], decodeURIComponent(value)];
            query_string[key] = arr;
            // If third or later entry with this name
        } else {
            query_string[key].push(decodeURIComponent(value));
        }
    }
    return query_string;
}

//var query_string = "?a=1&b=3&c=p";
////var query_string = window.location.href;
 
//if (query_string.indexOf('?') > -1)
//{
//    var quryStr = query_string.substr(query_string.indexOf('?')+1);
//    var parsedQueryStr = parse_query_string(quryStr);
//   alert(quryStr)//
//    alert(parse_query_string(quryStr).c);
//}

 