$(document).ready(function () {

    getUserEmails();

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
        console.log('Recreating users from db');
        $.when(ajaxCallToConsole('/api/account/resetdb', 'GET')).then(getUserEmails());
    });

    getUsersAndClaims.on('click', function () {
    });

    signIn.on('click', function () {
        var viewModel = userEmailList.val();
        ajaxCallToConsole('api/account/signin', 'POST', viewModel);
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

    function getUserEmails() {

        console.log('Populating dropdownlist');
        $('#spinner').show();

        $.ajax({
            url: '/api/home/getuseremails',
            type: 'GET'
        }).done(function (result) {
            userEmailList.empty();
            $.each(result, function (i, user) {
                userEmailList.append(`<option id="${user.id}">${user.email}</option>`);
            });
            $('#spinner').hide();
        }).fail(function (xhr, status, error) {
            console.log('error');
            $('#spinner').hide();
        });

    }

});