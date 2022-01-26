function CarouselDemoCtrl($scope) {
    $scope.myInterval = 5000;
    var slides = $scope.slides = [];
    $scope.addSlide = function () {
        var newWidth = 600 + slides.length;
        slides.push({
            image: 'http://localhost:4147/Content/Imgages/banner1.png'
        });
        slides.push({
            image: 'http://localhost:4147/Content/Imgages/banner2.png'
        });
        slides.push({
            image: 'http://localhost:4147/Content/Imgages/banner3.png'
        });
    };
    for (var i = 0; i < 4; i++) {
        $scope.addSlide();
    }
}