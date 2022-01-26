Gits.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                if (inputValue == undefined) return '';
                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

Gits.directive('mobileNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                if (inputValue == undefined) return '';
                var transformedInput = inputValue.replace(/[^0-9+-]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

Gits.directive("limitTo", function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitTo);
            angular.element(elem).on("keydown", function (event) {
                var key = event.keyCode || event.charCode;
                if (key == 8 || key == 46) {
                    return true;
                } else if (this.value.length == limit) {
                    return false;
                }                
                 
            });
        }
    }
});

Gits.directive('mobNumber', function () {
    return {
        replace: true,
        restrict: 'E',
        require: 'ngModel',
        template: "<input type='text' class='form-control'  limit-to='15'  mobile-number='mobile-number'required />",
        link: function (scope, element, attrs, ngModel) {
            var read = function () {
                var inputValue = element.val();
                ngModel.$setViewValue(inputValue);
            }
            element.intlTelInput({
               
            });
            element.on('focus blur keyup change', function () {
                scope.$apply(read);
            });
            read();
        }
    }
});

Gits.directive('manDatory', function () {
    return {
        link: function (scope, element) {
            element.append("<span>*</span>");
        }        
    }
});

Gits.directive('alphanumericOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {

            modelCtrl.$parsers.push(function (inputValue) {

                if (inputValue == undefined) return '';
                var transformedInput = inputValue.replace(/[^0-9a-zA-Z\s]/g, '');
               
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

Gits.directive('moneyOnly', function () {
    'use strict';

    var NUMBER_REGEXP = /^\s*(\-|\+)?(\d+|(\d*(\.\d*)))\s*$/;
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function(scope, el, attrs, ngModelCtrl) {
            var min = parseFloat(attrs.min || 0);
            var precision = parseFloat(attrs.precision || 2);
            var lastValidValue;

            function round(num) {
                var d = Math.pow(10, precision);
                return Math.round(num * d) / d;
            }

            function formatPrecision(value) {
                return parseFloat(value).toFixed(precision);
            }

            function formatViewValue(value) {
                return ngModelCtrl.$isEmpty(value) ? '' : '' + value;
            }

            ngModelCtrl.$parsers.push(function (value) {

                if (angular.isUndefined(value)) {
                    value = '';
                }

                // Handle leading decimal point, like ".5"
                if (value.indexOf('.') === 0) {
                    value = '0' + value;
                }

                // Allow "-" inputs only when min < 0
                if (value.indexOf('-') === 0) {
                    if (min >= 0) {
                        value = null;
                        ngModelCtrl.$setViewValue('');
                        ngModelCtrl.$render();
                    } else if (value === '-') {
                        value = '';
                    }
                }

                var empty = ngModelCtrl.$isEmpty(value);
                if ((empty || NUMBER_REGEXP.test(value)) && (value.length <= attrs.moneyLength)) {
                    lastValidValue = (value === '')
                      ? null
                      : (empty ? value : parseFloat(value));
                } else {
                    // Render the last valid input in the field


                    ngModelCtrl.$setViewValue(formatViewValue(lastValidValue));
                    ngModelCtrl.$render();
                }



                ngModelCtrl.$setValidity('number', true);
                return lastValidValue;
            });
            ngModelCtrl.$formatters.push(formatViewValue);

            var minValidator = function (value) {
                if (!ngModelCtrl.$isEmpty(value) && value < min) {
                    ngModelCtrl.$setValidity('min', false);
                    return undefined;
                } else {
                    ngModelCtrl.$setValidity('min', true);
                    return value;
                }
            };
            ngModelCtrl.$parsers.push(minValidator);
            ngModelCtrl.$formatters.push(minValidator);

            if (attrs.max) {
                var max = parseFloat(attrs.max);
                var maxValidator = function (value) {
                    if (!ngModelCtrl.$isEmpty(value) && value > max) {
                        ngModelCtrl.$setValidity('max', false);
                        return undefined;
                    } else {
                        ngModelCtrl.$setValidity('max', true);
                        return value;
                    }
                };

                ngModelCtrl.$parsers.push(maxValidator);
                ngModelCtrl.$formatters.push(maxValidator);
            }

            // Round off
            if (precision > -1) {
                ngModelCtrl.$parsers.push(function (value) {
                    return value ? round(value) : value;
                });
                ngModelCtrl.$formatters.push(function (value) {
                    return value ? formatPrecision(value) : value;
                });
            }

            el.bind('blur', function () {
                var value = ngModelCtrl.$modelValue;
                if (value) {
                    ngModelCtrl.$viewValue = formatPrecision(value);
                    ngModelCtrl.$render();
                }
            });
        }
    };
});

Gits.directive('npNumeric', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                var clean = val.replace(/[^0-9]+/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

Gits.directive('npRequired', function () {
    return {
        template: "<div>Password Fields are not equal!</div>",
        replace: false,
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            var clean = val.replace(/[^0-9]+/g, '');
            if (val !== clean) {
                ngModelCtrl.$setViewValue(clean);
                ngModelCtrl.$render();
            }
            return clean;
        }
    };
});

Gits.directive('npmask', function () {

    return {
        require: 'ngModel',
        link: function ($scope, element, attrs, controller) {
            //debugger;
            controller.$render = function () {
                var value = controller.$viewValue || '';
                element.val(value);
                return element.mask($scope.npmask);
            };
            controller.$parsers.push(function (value) {
                var isValid;
                console.log('parsing', value);
                isValid = element.data('mask-isvalid');

                controller.$setValidity('mask', isValid);
                return element.mask();
            });
            return element.bind('keyup', function () {
                return $scope.$apply(function () {
                    return controller.$setViewValue(element.mask());
                });
            });
        }
    };
});

Gits.directive('npAddress', function () {

    return {
        restrict: 'ECMA',
        require: '?ngModel',
        replace: true,
        template: '<div>' +
                "<label>Address Line 1:</label><input type='text' ng-model='AddressLine1' /><br />" +
                "<label>Address Line 2:</label><input type='text' ng-model='AddressLine2' /><br />" +
                "<label>City:</label><input type='text' ng-model='CityId' /><br />" +
                "<label>State:</label><select ng-options='st as st.StateName for st in States' ng-model='StateId'  /><br />" +
                "<label>City:</label><input type='text' ng-model='CityId' /><br />" +
                + "</div>",
        link: function (scope, element, attrs) {
        }
    };
});

Gits.directive("validationSummary", function () {
    return {
        restrict: "A",
        require: "^form",

        template: "<div ng-show='showMe'><div class='alert alert-warning'><a class='close' ng-click='toggleValidationSummary()'>×</a><h4 class='alert-heading'>Warning!</h4><li ng-repeat='(expression,message) in validationMessagesArray'>{{message}}</li></ul></div></div>",
        link: function ($scope, element, attributes, controller) {

            $scope.validationMessages = {};
            $scope.validationMessagesArray = [];
            $scope.showMe = false;

            // Hooks up a watch using [ng-show] expression
            controller.watchValidation = function (expression, message) {

                // watch the return value from the scope.$eval(expression)
                $scope.$watch(function () { return $scope.$eval(expression); }, function (isVisible) {

                    // check if the validation message exists
                    var containsMessage = $scope.validationMessages.hasOwnProperty(expression);

                    // if the validation message doesn't exist and it should be visible, add it to the list
                    if (!containsMessage && isVisible) {
                        $scope.validationMessages[expression] = message;
                        $scope.validationMessagesArray.push($scope.validationMessages[expression]);

                        if ($scope.validationMessagesArray !== 'undefined' && $scope.validationMessagesArray.length > 0) {
                            $scope.showMe = true;
                        }
                        else {
                            $scope.showMe = false;
                        }
                    }

                    // if the validation message does exist and it shouldn't be visible, delete it
                    if (containsMessage && !isVisible) {
                        //delete $scope.validationMessages[expression];
                        var index = $scope.validationMessagesArray.indexOf($scope.validationMessages[expression]);

                        $scope.validationMessagesArray.splice(index, 1);
                        delete $scope.validationMessages[expression];
                        if ($scope.validationMessagesArray !== 'undefined' && $scope.validationMessagesArray.length > 0) {
                            $scope.showMe = true;
                        }
                        else {
                            $scope.showMe = false;
                        }

                    }
                });
            };
        }
    };
});

Gits.directive("validationMessage", function () {
    return {
        restrict: "A",
        require: "^form",
        link: function (scope, element, attributes, controller) {
            // the ng-show expression used to determine message visibility
            var visibilityExpression = attributes.ngShow;

            // the validation message
            var message = element.text();

            // adds a watch to the validation message using the expression from the ng-show attribute
            controller.watchValidation(visibilityExpression, message);
        }
    };
});

Gits.directive('uniqueEmail', ['$http', function ($http) {
    var toId;
    return {
        require: 'ngModel',
        link: function (scope, elem, attr, ctrl) {
            //when the scope changes, check the name.
            scope.$watch(attr.ngModel, function (value) {
                // if there was a previous attempt, stop it.
                if (toId) clearTimeout(toId);

                // start a new attempt with a delay to keep it from
                // getting too "chatty".
                toId = setTimeout(function () {
                    // call to some API that returns { isValid: true } or { isValid: false }
                    $http.get('/api/ApiAdmin/IsEmailExist?email=' + value).success(function (data) {

                        //set the validity of the field
                        if (data == "true") {
                            ctrl.$setValidity('uniqueEmail', false);
                        } else if (data == "false") {
                            ctrl.$setValidity('uniqueEmail', true);
                        }
                    }).error(function (data, status, headers, config) {
                        console.log("something wrong");
                    });
                }, 200);
            });
        }
    };
}]);

Gits.directive('uniqueUsername', ['$http', function ($http) {
    var toId;
    return {
        require: 'ngModel',
        link: function (scope, elem, attr, ctrl) {
            //when the scope changes, check the name.
            scope.$watch(attr.ngModel, function (value) {
                // if there was a previous attempt, stop it.
                if (toId) clearTimeout(toId);

                // start a new attempt with a delay to keep it from
                // getting too "chatty".
                toId = setTimeout(function () {
                    // call to some API that returns { isValid: true } or { isValid: false }
                    $http.get('/api/ApiAdmin/IsUsernameExist?username=' + value).success(function (data) {

                        //set the validity of the field
                        if (data == "true") {
                            ctrl.$setValidity('uniqueUsername', false);
                        } else if (data == "false") {
                            ctrl.$setValidity('uniqueUsername', true);
                        }
                    }).error(function (data, status, headers, config) {
                        console.log("something wrong");
                    });
                }, 200);
            });
        }
    };
}]);

Gits.directive('uniqueRolename', ['$http', function ($http) {
    var toId;
    return {
        require: 'ngModel',
        link: function (scope, elem, attr, ctrl) {
            //when the scope changes, check the name.
            scope.$watch(attr.ngModel, function (value) {
                // if there was a previous attempt, stop it.
                if (toId) clearTimeout(toId);

                // start a new attempt with a delay to keep it from
                // getting too "chatty".
                toId = setTimeout(function () {
                    // call to some API that returns { isValid: true } or { isValid: false }
                    $http.get('/api/ApiAdmin/IsRolenameExist?rolename=' + value).success(function (data) {

                        //set the validity of the field
                        if (data == "true") {
                            ctrl.$setValidity('uniqueRolename', false);
                        } else if (data == "false") {
                            ctrl.$setValidity('uniqueRolename', true);
                        }
                    }).error(function (data, status, headers, config) {
                        console.log("something wrong");
                    });
                }, 200);
            });
        }
    };
}]);

Gits.directive('focusMe',['$timeout', function ($timeout) {
    return {
        scope: { trigger: '@focusMe' },
        link: function (scope, element) {
            scope.$watch('trigger', function (value) {
                if (value === "true") {
                    // console.log('trigger',value);
                    $timeout(function () {
                        element[0].focus();
                    });
                }
            });
        }
    };
}]);

Gits.directive('passwordValidate', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {

                scope.pwdValidLength = (viewValue && viewValue.length >= 10 ? 'valid' : undefined);
                scope.pwdHasUpperCaseLetter = (viewValue && /[A-Z]/.test(viewValue)) ? 'valid' : undefined;
                scope.pwdHasLowerCaseLetter = (viewValue && /[a-z]/.test(viewValue)) ? 'valid' : undefined;
                scope.pwdHasNumber = (viewValue && /\d/.test(viewValue)) ? 'valid' : undefined;
                scope.pwdHasSpecialCharacter = (viewValue && /[?=.*`~!@#$%^&*()_+={}|\:;<>?,.]/.test(viewValue)) ? 'valid' : undefined;

                if (scope.pwdValidLength && scope.pwdHasUpperCaseLetter && scope.pwdHasLowerCaseLetter && scope.pwdHasSpecialCharacter && scope.pwdHasNumber) {
                    ctrl.$setValidity('pwd', true);
                    return viewValue;
                } else {
                    ctrl.$setValidity('pwd', false);
                    return false;
                }
            });
        }
    };
});

Gits.directive("passwordVerify", function () {
    return {
        require: "ngModel",
        scope: {
            passwordVerify: '='
        },
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(function () {
                var combined;

                if (scope.passwordVerify || ctrl.$viewValue) {
                    combined = scope.passwordVerify + '_' + ctrl.$viewValue;
                }
                return combined;
            }, function (value) {
                if (value) {
                    ctrl.$parsers.unshift(function (viewValue) {
                        //console.log(viewValue);
                        var origin = scope.passwordVerify;
                        if (origin !== viewValue) {
                            ctrl.$setValidity("passwordVerify", false);
                            return false;
                        } else {
                            ctrl.$setValidity("passwordVerify", true);
                            return viewValue;
                        }
                    });
                }
            });
        }
    };
});

Gits.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault();
                });
            }
        }
    };
});

