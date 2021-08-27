var gulp = require('gulp');
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");
var uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/",
    nodeModules: "./node_modules/"
};

paths.bootstrapCss = paths.nodeModules + "bootstrap/dist/css/bootstrap.css";

paths.vendorCssFiles = [paths.bootstrapCss, paths.webroot + "css/*.css"]

paths.vendorCssFileName = "vendor.min.css";

paths.destinationCssFolder = paths.webroot + "styles/";

gulp.task("minify-vendor-css", function () {
    return gulp.src(paths.vendorCssFiles)
        .pipe(concat(paths.vendorCssFileName))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.destinationCssFolder));
});



paths.bootstrapjs = paths.nodeModules + "bootstrap/dist/js/bootstrap.js";

paths.jqueryjs = paths.nodeModules + "jquery/dist/jquery.js";

paths.vendorJsFiles = [
    paths.nodeModules +  "jquery/dist/jquery.min.js",
    paths.nodeModules +  "jquery-validation/dist/jquery.validate.min.js",
    paths.nodeModules +  "jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js",
    paths.jqueryjs,  paths.nodeModules + "bootstrap/dist/js/bootstrap.bundle.min.js",
   paths.bootstrapjs, paths.webroot + "js/*.js",
];

paths.vendorJsFileName = "vendor.min.js";

paths.destinationJsFolder = paths.webroot + "scripts/";

gulp.task("minify-vendor-js", function () {
    return gulp.src(paths.vendorJsFiles)
        .pipe(concat(paths.vendorJsFileName))
        .pipe(uglify())
        .pipe(gulp.dest(paths.destinationJsFolder));
});

gulp.task("watcher", function () {
    gulp.watch(paths.vendorJsFiles, gulp.series("minify-vendor-js"));
    gulp.watch(paths.vendorCssFiles, gulp.series("minify-vendor-css"));
});