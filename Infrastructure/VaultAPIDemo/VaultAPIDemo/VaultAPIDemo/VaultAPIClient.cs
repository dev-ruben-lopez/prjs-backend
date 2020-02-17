using System;
using Vault;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace VaultAPIDemo
{
    public class VaultAPIClient
    {

        private static Vault.VaultClient vClient;

        public void CreateClient()
        {
            // Initialize one of the several auth methods.

            var token = "s.shgi38Lx6XcpEJXCc5yt6hVw";
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(token);
            System.Uri uri = new Uri("http://127.0.0.1:8200");

            vClient = new Vault.VaultClient(uri,token);


        }

        public string Get()
        {

            // Use client to read a key-value secret.
            var kv2Secret = vClient.Secret.Read<SecretData>("secret/name");

            return kv2Secret.Result.ToString();
        }
    }
}
