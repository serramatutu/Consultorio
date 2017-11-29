app.factory("access", ["$q", "userState", function ($q, userState) {
    var _hasRole = function (auth, role) {
        if (auth.hasRole(role))
            return accessFactory.OK;
        if (auth.isAnonymous())
            return $q.reject(accessFactory.UNAUTHORIZED);

        return $q.reject(accessFactory.FORBIDDEN);
    };

    var _hasAnyRole = function (auth) {
        if (auth.hasAnyRole(roles))
            return accessFactory.OK;
        else if (auth.isAnonymous())
            return $q.reject(accessFactory.UNAUTHORIZED);

        return $q.reject(accessFactory.FORBIDDEN);
    };

    var _isAnonymous = function (auth) {
        if (auth.isAnonymous())
            return accessFactory.OK;

        return $q.reject(accessFactory.FORBIDDEN);
    };

    var _isAuthenticated = function (auth) {
        if (auth.isAuthenticated())
            return accessFactory.OK;

        return $q.reject(accessFactory.UNAUTHORIZED);
    }

    var accessFactory = {
        OK: 200,

        // não sabe quem você é. fazer login primeiro
        UNAUTHORIZED: 401,

        // sabemos quem você é e não pode :)
        FORBIDDEN: 403,

         // obtém um objeto estático para ser utilizado várias vezes
        getStatic: function() {
            return userState.getState().then(function (auth) {
                return {
                    hasRole: function (role) {
                        return _hasRole(auth, role);
                    },
                    hasAnyRole: function (roles) {
                        return _hasAnyRole(auth, roles)
                    },
                    isAnonymous: function () {
                        return _isAnonymous(auth);
                    },
                    isAuthenticated: function () {
                        return _isAuthenticated(auth);
                    }
                };
            });
        },

        // se o usuário está em um determinado papel
        hasRole: function (role) {
            userState.getState().then((auth) => { return _hasRole(auth, role) });
        },

        hasAnyRole: function (roles) {
            userState.getState().then((auth) => { return _hasRole(auth, roles) });
        },

        isAnonymous: function () {
            userState.getState().then(_isAnonymous);
        },

        isAuthenticated: function () {
            userState.getState().then(_isAuthenticated);
        }
    };

    return accessFactory;
}])