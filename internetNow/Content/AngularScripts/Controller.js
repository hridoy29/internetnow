app.controller("EmpCtrl", function ($scope, $http) {
    $(function () {
        var randomFilesize
        var randomNumber;
        var randomNumberFloat;
        var randomAlphanumaricNumber;

        $("#Start").click(function () {
            if ($('#numericCheck').is(":checked")) {
                funNumaric();
            }
            if ($('#alphanumericCheck').is(":checked")) {
                funAlphaumaric();
            }
            if ($('#floatCheck').is(":checked")) {
                funFloat();
            }
            getFileSize();
        })
        function getFileSize() {
            clearInterval(randomFilesize);
            randomFilesize = setInterval(function () {
                var fileSize = $("#fileSizeId").val() + "KB";
                var params = JSON.stringify({ obj: fileSize });
                $http.post('/Home/FileSize', params).success(function (data) {
                    var iswriteable = data;
                    if (iswriteable == false) {
                        clearInterval(randomNumber);
                        clearInterval(randomAlphanumaricNumber);
                        clearInterval(randomNumberFloat);
                        clearInterval(randomFilesize);
                    }
                }).error(function () {
                    $scope.entryBlock.stop();
                    alertify.log('Unknown server error', 'error', '10000');
                });
            }, 10);
        }
        function funNumaric() {
            clearInterval(randomNumber);
            randomNumber = setInterval(function () {
                var totalValue = Math.floor(Math.random() * 100000000);
                var params = JSON.stringify({ obj: totalValue + '-N' });
                $http.post('/Home/Post', params).success(function (data) {
                }).error(function () {
                    $scope.entryBlock.stop();
                    alertify.log('Unknown server error', 'error', '10000');
                });
                $('#randomNum').val(totalValue);
            }, 10);
        }
        function funAlphaumaric() {
            clearInterval(randomAlphanumaricNumber);
            var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            randomAlphanumaricNumber = setInterval(function () {
                var text = "";
                for (var i = 0; i < 10; i++) {
                    text += possible.charAt(Math.floor(Math.random() * possible.length));
                }
                var params = JSON.stringify({ obj: text + '-A' });
                $http.post('/Home/Post', params).success(function (data) {
                }).error(function () {
                    $scope.entryBlock.stop();
                    alertify.log('Unknown server error', 'error', '10000');
                });
                $('#alphanumericId').val(text);
            })
        }
        function funFloat() {
            clearInterval(randomNumberFloat);
            randomNumberFloat = setInterval(function () {
                var totalValue = Math.random() * (.1 + 100000000) / .12;
                var params = JSON.stringify({ obj: totalValue + '-F' });
                $http.post('/Home/Post', params).success(function (data) {
                }).error(function () {
                    $scope.entryBlock.stop();
                    alertify.log('Unknown server error', 'error', '10000');
                });
                $('#floatId').val(totalValue);
            }, 10);
        }
        $("#Stop").click(function () {
            clearInterval(randomNumber);
            clearInterval(randomAlphanumaricNumber);
            clearInterval(randomNumberFloat);
            clearInterval(randomFilesize);
        })
    })
});  