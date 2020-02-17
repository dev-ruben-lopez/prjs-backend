using NUnit.Framework;
using VaultAPIDemo;

namespace Tests
{
    public class Tests
    {

        private VaultAPIClient vaultAPIClient = new VaultAPIClient();

        [SetUp]
        public void Setup()
        {
            vaultAPIClient.CreateClient();
        }

        [Test]
        public void Test1()
        {
            
            var res = vaultAPIClient.Get();

            Assert.IsNotNull(res);
        }
    }
}