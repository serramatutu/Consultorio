angular.module('myApp').directive('hasPermission', function(permissions) {  
  return {
    link: function(scope, element, attrs) {
      if(typeof attrs.hasPermission !== 'string') {
        throw 'hasPermission deve ser string'
      }

      var value = attrs.hasPermission.trim();
      var notPermissionFlag = value[0] === '!';
      if(notPermissionFlag) {
        value = value.slice(1).trim();
      }

      function toggleVisibilityBasedOnPermission() {
        var hasPermission = permissions.hasPermission(value);
        if(hasPermission && !notPermissionFlag || !hasPermission && notPermissionFlag) {
          element[0].style.display = 'block';
        }
        else {
          element[0].style.display = 'none';
        }
      }

      toggleVisibilityBasedOnPermission();
      scope.$on('permissionsChanged', toggleVisibilityBasedOnPermission);
    }
  };
});