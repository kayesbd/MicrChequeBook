
   
    //jQuery(document).ready(function () {
      
//jQuery("#menu").kendoMenu({
//    animation: { open: { effects: "fadeIn" } },
//    orientation: 'vertical'
//});

jQuery(function () {
    var pgurl = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
    jQuery("page-sidebar-menu li a ").each(function () {
        if (jQuery(this).attr("href") == pgurl || $(this).attr("href") == '')
            jQuery(this).addClass("active");
        if (jQuery(this).attr("ng-href") == pgurl || $(this).attr("href") == '')
            jQuery(this).addClass("active");
    });
});
