app.factory("userState", ["$q", "authService", function ($q, authService) {
    var _extend = function (response) {
        return angular.extend(response.data, {
            // obtém um objeto estático para ser utilizado várias vezes
            hasRole: function (role) {
                if (!!this.roles)
                    return this.roles.indexOf(role) >= 0;
                return false;
            },

            hasAnyRole: function (roles) {
                return !!this.roles.filter(function (role) {
                    return roles.indexOf(role) >= 0;
                }).length;
            },

            isAnonymous: function () {
                return this.anonymous;
            },

            isAuthenticated: function () {
                return !this.anonymous;
            }
        });
    };

    var userStateFactory = {
        getState: function() {
            return authService.getProfile().then(_extend);
        }
    }
    
    return userStateFactory;
}])