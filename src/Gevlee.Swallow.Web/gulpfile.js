const { series, src, dest } = require('gulp');
var gulp = require('gulp');
var sourcemaps = require('gulp-sourcemaps');
var mode = require('gulp-mode')();
const sass = require('gulp-sass');

gulp.task('sass', function () {
    return src('Sass/main.scss')
        .pipe(mode.development(sourcemaps.init()))
        .pipe(sass().on('error', sass.logError))
        .pipe(mode.development(sourcemaps.write('.')))
        .pipe(dest('wwwroot/css'));
});

if (process.env.NODE_ENV === 'production') {
    gulp.task('default', series('sass'));
} else {
    gulp.task('default', series('sass'));
}