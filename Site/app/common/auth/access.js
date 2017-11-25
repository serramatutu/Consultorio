app.factory("Access", ["$q", "userState", function ($q, userState) {
    var Access = {

        OK: 200,

        // não sabe quem você é. fazer login primeiro
        UNAUTHORIZED: 401,

        // sabemos quem você é e não pode :)
        FORBIDDEN: 403,

        // se o usuário está em um determinado papel
        hasRole: function (role) {
            return userState.then(function (auth) {
                if (auth.hasRole(role)) {
                    return Access.OK;
                } else if (auth.isAnonymous()) {
                    return $q.reject(Access.UNAUTHORIZED);
                } else {
                    return $q.reject(Access.FORBIDDEN);
                }
            });
        },

        hasAnyRole: function (roles) {
            return userState.then(function (auth) {
                if (auth.hasAnyRole(roles)) {
                    return Access.OK;
                } else if (auth.isAnonymous()) {
                    return $q.reject(Access.UNAUTHORIZED);
                } else {
                    return $q.reject(Access.FORBIDDEN);
                }
            });
        },

        isAnonymous: function () {
            return userState.then(function (auth) {
                if (auth.isAnonymous()) {
                    return Access.OK;
                } else {
                    return $q.reject(Access.FORBIDDEN);
                }
            });
        },

        isAuthenticated: function () {
            return userState.then(function (auth) {
                if (auth.$isAuthenticated()) {
                    return Access.OK;
                } else {
                    return $q.reject(Access.UNAUTHORIZED);
                }
            });
        }
    };

    return Access;

}])