﻿using Microsoft.AspNetCore.Authorization;

namespace PokemonApp.Server.Authorization
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }

        public string Scope { get; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }    
    }
}