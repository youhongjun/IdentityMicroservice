﻿using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;


public class InMemoryConfiguration
{
    //public static IConfiguration Configuration { get; set; }
    /// <summary>
    /// Define which APIs will use this IdentityServer
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new[]
        {
            new ApiResource("myapiservice", "my api Service"),
            new ApiResource("clientservice", "Client Service"),
            new ApiResource("productservice", "Product Service"),
            new ApiResource("agentservice", "Agent Service")
        };
    }

    /// <summary>
    /// Define which Apps will use thie IdentityServer
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Client> GetClients()
    {
        return new[]
        {
            new Client
            {
                ClientId = "myapi.service",
                ClientSecrets = new [] { new Secret("myapisecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = new [] { "myapiservice" }
            },
            new Client
            {
                ClientId = "client.api.service",
                ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = new [] { "clientservice" }
            },
            new Client
            {
                ClientId = "product.api.service",
                ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = new [] { "clientservice", "productservice" }
            },
            new Client
            {
                ClientId = "agent.api.service",
                ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
            }
        };
    }

    /// <summary>
    /// Define which uses will use this IdentityServer
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<TestUser> GetUsers()
    {
        return new[]
        {
            new TestUser
            {
                SubjectId = "10001",
                Username = "edison@gmail.com",
                Password = "edisonpassword"
            },
            new TestUser
            {
                SubjectId = "10002",
                Username = "andy@gmail.com",
                Password = "andypassword"
            },
            new TestUser
            {
                SubjectId = "10003",
                Username = "leo@gmail.com",
                Password = "leopassword"
            }
        };
    }
}