app.factory("userState", ["authService", function (authService) {
    var userState = {};

    var clearUserState = function () {
        for (var prop in userState) {
            if (userState.hasOwnProperty(prop)) {
                delete userState[prop];
            }
        }
    };

    var fetchUserProfile = function () {
        return authService.getProfile().then(function (response) {
            clearUserState();
            return angular.extend(userState, response.data, {

                refresh: fetchUserProfile,

                hasRole: function (role) {
                    return userState.roles.indexOf(role) >= 0;
                },

                hasAnyRole: function (roles) {
                    return !!userState.roles.filter(function (role) {
                        return roles.indexOf(role) >= 0;
                    }).length;
                },

                isAnonymous: function () {
                    return userState.anonymous;
                },

                isAuthenticated: function () {
                    return !userState.anonymous;
                }

            });
        });
    };

    return fetchUserProfile();
}])