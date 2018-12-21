

function nav1(url1) {

        //var hv = $('#st').val();
        //alert(hv);
        

    $.ajax({
            url: url1,
            //data: "{st:"+ JSON.stringify(hv) +"}",
            type: 'post',
            contentType: 'application/json',
            success: function (data) {
                //alert(data);
                $("#adminview1").html(data);
            }
        }
        );
}




