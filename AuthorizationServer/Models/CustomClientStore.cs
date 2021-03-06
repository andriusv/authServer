﻿using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class CustomClientStore : IClientStore
    {
        public static IEnumerable<Client> AllClients { get; } = new[]
        {
            new Client
            {
                ClientId = "myClient",
                ClientName = "My Custom Client",
                AccessTokenLifetime = 60 * 15, // 15 min
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes =
                {
                    "myAPIs",
                    "offline_access"
                }
                //,
                //AllowOfflineAccess = true,
                //AllowAccessTokensViaBrowser = true
            }
        };

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(AllClients.FirstOrDefault(c => c.ClientId == clientId));
        }
    }
}
