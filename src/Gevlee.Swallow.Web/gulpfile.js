const { series, src, dest } = require('gulp');
var gulp = require('gulp');
const sass = require('gulp-sass');

gulp.task('sass', function () {
    return src('Sass/main.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(dest('wwwroot/css'));
});

if (process.env.NODE_ENV === 'production') {
    gulp.task('default', series('sass'));
} else {
    gulp.task('default', series('sass'));
}