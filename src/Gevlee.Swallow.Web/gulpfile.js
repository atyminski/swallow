const { series, src, dest, task } = require('gulp');
var gulp = require('gulp');
var sourcemaps = require('gulp-sourcemaps');
var mode = require('gulp-mode')();
var merge = require("merge-stream");
var rimraf = require("rimraf");
const sass = require('gulp-sass');


var paths = {
    webroot: "./wwwroot/",
    node_modules: "./node_modules/"
};

paths.libDest = paths.webroot + 'lib/';

gulp.task('libs:clean', function (cb) {
    rimraf(paths.libDest, cb);
});

gulp.task('libs:copy', function () {
    var bootstrap = merge(
        gulp.src(paths.node_modules + 'bootstrap/dist/css/bootstrap.min.css')
            .pipe(gulp.dest(paths.libDest + 'bootstrap/css')),
        gulp.src(paths.node_modules + 'bootstrap/dist/js/bootstrap.min.js')
            .pipe(gulp.dest(paths.libDest + 'bootstrap/js'))
    )

    var openIconic = merge(
        gulp.src(paths.node_modules + 'open-iconic/font/css/open-iconic-bootstrap.min.css')
            .pipe(gulp.dest(paths.libDest + 'open-iconic/css')),
        gulp.src(paths.node_modules + 'open-iconic/font/fonts/*.*')
            .pipe(gulp.dest(paths.libDest + 'open-iconic/font'))
    )

    return merge(bootstrap, openIconic);
});

gulp.task('libs', series('libs:clean', 'libs:copy'));


gulp.task('compile:sass', function () {
    return src('Sass/main.scss')
        .pipe(mode.development(sourcemaps.init()))
        .pipe(sass().on('error', sass.logError))
        .pipe(mode.development(sourcemaps.write('.')))
        .pipe(dest(paths.webroot + 'css'));
});

gulp.task('compile', series('compile:sass'));

//if (process.env.NODE_ENV === 'production') {
//    gulp.task('default', series('libs', 'compile'));
//} else {
//    gulp.task('default', series('libs', 'compile'));
//}

gulp.task('default', series('libs', 'compile'));