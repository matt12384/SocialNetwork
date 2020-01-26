var gulp = require('gulp'),
    plumber = require('gulp-plumber');

gulp.task('css', function () {
    var postcss = require('gulp-postcss'),
        tailwindcss = require('tailwindcss'),
        sass = require('gulp-sass');

    return gulp.src('./Assets/scss/site.scss')
        .pipe(plumber())
        .pipe(sass())
        .pipe(postcss([
            tailwindcss('tailwind.config.js'),
            require('autoprefixer')
        ]))
        .pipe(gulp.dest('wwwroot/css/'));
});

gulp.task('watch', gulp.series('css', function () {
    gulp.watch([
        './tailwind.config.js',
        './Pages/**/*.cshtml',
        './Pages/**/*.razor',
        './Shared/**/*.razor',
        './Assets/scss/**/*.scss'
    ],
    gulp.series('css'));
}));

gulp.task('default', gulp.series('css', function () {
    var csso = require('gulp-csso'),
        rename = require('gulp-rename');

    return gulp.src('./wwwroot/css/site.css')
        .pipe(plumber())
        .pipe(csso())
        .pipe(rename('site.min.css'))
        .pipe(gulp.dest('./wwwroot/css'));
}));