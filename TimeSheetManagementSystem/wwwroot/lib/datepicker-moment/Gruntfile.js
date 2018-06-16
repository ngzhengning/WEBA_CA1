module.exports = function(grunt) {

  grunt.initConfig({
    cssmin: {
      options: {
        shorthandCompacting: false,
        roundingPrecision: -1
      },
      target: {
        files: {
          'moment-datepicker/moment-datepicker.min.css': ['moment-datepicker/*.css', '!moment-datepicker/*.min.css']
        }
      }
    },
    uglify: {
      options: {
        sourceMap: true,
        sourceMapName: 'moment-datepicker/moment-datepicker.js.map'
      },
      target: {
        files: {
          'moment-datepicker/moment-datepicker.min.js': ['moment-datepicker/*.js', '!moment-datepicker/*.min.js']
        }
      }
    },
    jshint: {
      files: ['Gruntfile.js',
              'moment-datepicker/*.js',
              '!moment-datepicker/*.min.js'],
      options: {
        globals: {
          jQuery: true
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-cssmin');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-jshint');

  grunt.registerTask('default', ['jshint']);

};