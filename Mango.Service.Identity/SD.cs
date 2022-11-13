using Duende.IdentityServer.Models;

namespace Mango.Service.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> apiScopes => new List<ApiScope>
        {
            new ApiScope("Mango", "Mango Server"),
            new ApiScope("read", "Read your data."),
            new ApiScope("write", "write your data."),
            new ApiScope("delete", "Delete your data."),
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client()
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
        };

    }
}
