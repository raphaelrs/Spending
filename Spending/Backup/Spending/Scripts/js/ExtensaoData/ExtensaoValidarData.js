//$().ready(function () {

//    // fix date validation for chrome
//    jQuery.extend(jQuery.validator.methods, {
//        date: function (value, element) {
//            var isChrome = window.chrome;
//            // make correction for chrome
//            if (isChrome) {
//                var d = new Date();
//                return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
//            }
//                // leave default behavior
//            else {
//                return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
//            }
//        }
//    });

//    // set default date for pt-br
//    $.datepicker.setDefaults($.datepicker.regional["pt-BR"]);

//    // setup date picker
//    $(".campoDeData").datepicker();

//    // validate date form 
//    //$("#dateForm").validate({
//    //    rules: {
//    //        datepicker: {
//    //            required: true,
//    //            date: true
//    //        }
//    //    }
//    //});
//});