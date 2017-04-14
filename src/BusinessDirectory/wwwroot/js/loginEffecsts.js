//loginEffects.js
(function () {
    $(".jumbotron").hide().fadeIn(1000, stopClass).addClass("animated bounceInDown");
    function stopClass() {
        $(".jumbotron").removeClass("animated bounce infinite");
    }
})();