using System;
using practice11;
using Xunit;

namespace TestProject1
{
    public class Tests
    {
        private TestCollections col = new TestCollections();
        
        [Fact]
        public void Test()
        {
            col.SearchEngs();
        }
    }
}