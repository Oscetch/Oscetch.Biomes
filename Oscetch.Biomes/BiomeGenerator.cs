namespace Oscetch.Biomes
{
    public class BiomeGenerator<T>(GeneratorConfiguration<T> configuration) where T: IBiomePlaceableItem
    {
        private readonly GeneratorConfiguration<T> _configuration = configuration;

        public Dictionary<BiomeLayer, Dictionary<Position, T>> Run(IReadOnlyList<Position> positions) => RunWithIterations(positions).Last();
        public Dictionary<BiomeLayer, Dictionary<Position, T>> RunForSize(int width, int height) => RunForSizeWithIterations(width, height).Last();

        public IEnumerable<Dictionary<BiomeLayer, Dictionary<Position, T>>> RunWithIterations(IReadOnlyList<Position> positions) 
        {
            Dictionary<Position, BiomeConfiguration<T>> layerMap = [];
            InitializeMap(layerMap, _configuration.Biomes, positions, true);

            var groups = layerMap.Values.GroupBy(x => x.Id);

            for (var i = 0; i < _configuration.NumberOfSimulations; i++) 
            {
                var newMap = Simulate(layerMap, _configuration.Biomes, positions);
                groups = newMap.Values.GroupBy(x => x.Id);
                layerMap = newMap;
            }

            var biomeGroups = layerMap.GroupBy(x => x.Value.Id);
            foreach (var biomeGroup in biomeGroups) 
            {
                var biomePositions = biomeGroup.Select(x => x.Key).ToList();
                var biome = biomeGroup.First().Value;
                foreach (var sim in PopulateBiomes(biomePositions, biome)) 
                {
                    yield return sim;
                }
            }
        }

        public IEnumerable<Dictionary<BiomeLayer, Dictionary<Position, T>>> RunForSizeWithIterations(int width, int height) 
        {
            var positions = new List<Position>();
            for (var x = 0; x < width; x++) 
            {
                for (var y = 0; y < height; y++) 
                {
                    positions.Add(new Position(x, y));
                }
            }
            return RunWithIterations(positions);
        }

        private IEnumerable<Dictionary<BiomeLayer, Dictionary<Position, T>>> PopulateBiomes(IReadOnlyList<Position> positions, BiomeConfiguration<T> configuration) 
        {
            Dictionary<BiomeLayer, Dictionary<Position, T>> layerMaps = configuration.LayerItems.Where(x => x.Value.Count != 0)
                .ToDictionary(x => x.Key, x => new Dictionary<Position, T>());

            foreach (var layerMap in layerMaps) 
            {
                InitializeMap(layerMap.Value, configuration.LayerItems[layerMap.Key], positions, layerMap.Key.IsFillLayer);
            }

            for (var i = 0; i < configuration.NumberOfSimulations; i++)
            {
                Dictionary<BiomeLayer, Dictionary<Position, T>> newMaps = [];
                foreach (var map in layerMaps) 
                {
                    newMaps[map.Key] = Simulate(map.Value, configuration.LayerItems[map.Key], positions);
                }
                layerMaps = newMaps;
                yield return newMaps;
            }
        }

        private static Dictionary<Position, TSimItem> Simulate<TSimItem>(Dictionary<Position, TSimItem> oldMap, IReadOnlyList<TSimItem> pool, IReadOnlyList<Position> positions) where TSimItem : IBiomeItem
        {
            Dictionary<Position, TSimItem> newMap = new(oldMap);

            foreach (var position in positions) 
            {
                var neighbors = GetNeighbors(position, oldMap).ToList();
                if (!oldMap.TryGetValue(position, out var itemAtPosition)) 
                {
                    foreach (var option in pool) 
                    {
                        var weight = neighbors.Count(x => x.Id == option.Id);
                        if (weight > option.BirthLimit) 
                        {
                            newMap[position] = option;
                        }
                    }
                    continue;
                }
                foreach (var option in pool) 
                {
                    var weight = neighbors.Count(x => x.Id == option.Id);
                    if (weight > option.BirthLimit && weight > itemAtPosition.DeathLimit) 
                    {
                        newMap[position] = option;
                    }
                }
            }
            return newMap;
        }

        private void InitializeMap<TInitItem>(Dictionary<Position, TInitItem> map, IReadOnlyList<TInitItem> pool, IReadOnlyList<Position> positions, bool shouldFill) where TInitItem : IBiomeItem
        {
            var random = new Random(_configuration.Seed);
            var orderedPool = pool.OrderBy(x => x.InitialCreationChance).ToList();

            if (shouldFill)
            {
                foreach (var position in positions)
                {
                    while (!TryAdd(position, map, orderedPool, random)) ;
                }
            }
            else 
            {
                foreach (var position in positions)
                {
                    TryAdd(position, map, orderedPool, random);
                }
            }
        }

        private static IEnumerable<TNeighborItem> GetNeighbors<TNeighborItem>(Position position, Dictionary<Position, TNeighborItem> map) where TNeighborItem : IBiomeItem 
        {
            foreach (var neighborsPosition in GetNeighborPositions(position)) 
            {
                if (map.TryGetValue(neighborsPosition, out var neighbor)) 
                {
                    yield return neighbor;
                }
            }
        }

        private static IEnumerable<Position> GetNeighborPositions(Position position) 
        {
            for (var xOffset = -1; xOffset < 2; xOffset++) 
            {
                for (var yOffset = -1; yOffset < 2; yOffset++) 
                {
                    var neighborX = position.X + xOffset;
                    var neighborY = position.Y + yOffset;
                    if (neighborX == position.X && neighborY == position.Y) continue;
                    yield return new Position(neighborX, neighborY);
                }
            }
        }

        private static bool TryAdd<TAddItem>(Position position, Dictionary<Position, TAddItem> map, IReadOnlyList<TAddItem> pool, Random random) where TAddItem : IBiomeItem
        {
            foreach (var option in pool)
            {
                if (random.NextDouble() > option.InitialCreationChance)
                {
                    continue;
                }

                map.Add(position, option);
                return true;
            }
            return false;
        }
    }
}
