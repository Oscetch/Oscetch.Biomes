namespace Oscetch.Biomes.Test
{
    [TestClass]
    public sealed class BiomeGeneratorTest
    {
        [TestMethod]
        public void DifferentOrderProducesSameResult()
        {
            var seed = 3;
            var a = new TestBiomePlaceableItem();
            var b = new TestBiomePlaceableItem();
            var biomeLayer = BiomeLayer.FillLayer;
            var aList = new Dictionary<BiomeLayer, List<TestBiomePlaceableItem>> 
            {
                { biomeLayer, [a] },
            };
            var bList = new Dictionary<BiomeLayer, List<TestBiomePlaceableItem>> 
            {
                { biomeLayer, [b] },
            };
            var aConfig = new BiomeConfiguration<TestBiomePlaceableItem>(aList);
            var bConfig = new BiomeConfiguration<TestBiomePlaceableItem>(bList);
            var aFirstConfiguration = new GeneratorConfiguration<TestBiomePlaceableItem>([aConfig, bConfig]) 
            {
                Seed = seed
            };
            var bFirstConfiguration = new GeneratorConfiguration<TestBiomePlaceableItem>([bConfig, aConfig]) 
            {
                Seed = seed
            };

            var aGenerator = new BiomeGenerator<TestBiomePlaceableItem>(aFirstConfiguration);
            var bGenerator = new BiomeGenerator<TestBiomePlaceableItem>(bFirstConfiguration);

            var aResult = aGenerator.RunForSize(100, 100);
            var bResult = bGenerator.RunForSize(100, 100);

            var expectedPositions = 100 * 100;

            Assert.AreEqual(aResult.Count, 1);
            Assert.AreEqual(bResult.Count, 1);

            foreach (var key in aResult.Keys) 
            {
                var aResultPositionList = aResult[key];
                var bResultPositionList = bResult[key];
                Assert.AreEqual(expectedPositions, aResultPositionList.Count);
                Assert.AreEqual(aResultPositionList.Count, bResultPositionList.Count);
                foreach (var (position, aItem) in aResultPositionList) 
                {
                    var bItem = bResultPositionList[position];
                    Assert.AreEqual(aItem.Id, bItem.Id);
                }
            }
        }
    }
}
