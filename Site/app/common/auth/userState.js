app.factory("userState", ["$q", "authService", function ($q, authService) {
    return authService.getProfile().then(function (response) {
        return angular.extend(response.data, {
            hasRole: function (role) {
                return this.roles.indexOf(role) >= 0;
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
    });
}])