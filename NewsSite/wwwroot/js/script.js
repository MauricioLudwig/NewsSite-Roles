$(document).ready(function () {

    retrieveUserEmails();

    // Cached variables
    var recreateUsers = $('#recreateUsers');
    var getUsersAndClaims = $('#getUsersAndClaims');

    var userEmailList = $('#userEmailList');
    var signIn = $('#signIn');

    var accessToOpenArticle = $('#accessToOpenArticle');
    var accessToHiddenArticle = $('#accessToHiddenArticle');
    var accessToHiddenArticleAndEG20 = $('#accessToHiddenArticleAndEG20');
    var allowedToPublishSportsArticle = $('#allowedToPublishSportsArticle');
    var allowedToPublishCultureArticle = $('#allowedToPublishCultureArticle');

    recreateUsers.on('click', function () {
        ajaxCallToConsole('/api/home/resetdb', 'GET');
    });

    getUsersAndClaims.on('click', function () {
    });

    signIn.on('click', function () {
        var viewModel = userEmailList.val();
        ajaxCallToConsole('api/home/signin', 'POST', viewModel);
    });

    accessToOpenArticle.on('click', function () {
    });

    accessToHiddenArticle.on('click', function () {
    });

    accessToHiddenArticleAndEG20.on('click', function () {
    });

    allowedToPublishSportsArticle.on('click', function () {
    });

    allowedToPublishCultureArticle.on('click', function () {
    });

    function retrieveUserEmails() {
        ajaxCallGetEmailList('/api/home/getemaillist', 'GET');
    }

    function ajaxCallToConsole(url, type, viewModel) {

        $.ajax({
            url: url,
            type: type,
            data: { 'viewModel' : viewModel }
        }).done(function (result) {
            console.log('success');
            console.log(result);
        }).fail(function(xhr, status, error){
            console.log('error: ' + xhr.responseText);
        });

    }

    function ajaxCallGetEmailList(url, type, viewModel) {

        $.ajax({
            url: url,
            type: type,
            data: { 'viewModel': viewModel }
        }).done(function (result) {
            $.each(result, function (i, user) {
                console.log(user.email);
                userEmailList.append(`<option id="${user.id}">${user.email}</option>`);
            });
        }).fail(function (xhr, status, error) {
            console.log('error');
        });

    }

});