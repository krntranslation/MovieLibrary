(function($){
    function processForm( e ){
        var dict = {
        	Title : this["title"].value,
            DirectorName: this["directorName"].value,
            Genre: this["genre"].value
        };

        $.ajax({
            url: 'https://localhost:44352/api/movie',
            dataType: 'json',
            type: 'post',
            contentType: 'application/json',
            data: JSON.stringify(dict),
            success: function( data, textStatus, jQxhr ){
                $('#response pre').html( data );
            },
            error: function( jqXhr, textStatus, errorThrown ){
                console.log( errorThrown );
            }
        });

        e.preventDefault();
    }

    $('#my-form').submit(processForm);
    
})(jQuery);
(function($){
    function getRequest( e ){
        var dict = {
        	Title : this["title"].value,
            DirectorName: this["directorName"].value,
            Genre: this["genre"].value
        };

        $.ajax({
            url: 'https://localhost:44352/api/movie',
            dataType: 'json',
            type: 'get',
            contentType: 'application/json',
            data: JSON.stringify(dict),
            success: function( data, textStatus, jQxhr ){
                $('#response pre').append($("<tr")
                .append($("<td>").append(this.title))
                .append($("<td>").append(this.directorName))
                .append($("<td>").append(this.genre)))
            },
            error: function( jqXhr, textStatus, errorThrown ){
                console.log( errorThrown );
            }
        });

        e.preventDefault();
    }

    $('#my-get').submit(getRequest);
    
})(jQuery);