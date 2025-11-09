namespace Oscetch.Biomes
{
    public class BiomeGenerator<T>(GeneratorConfiguration<T> configuration) where T: IBiomePlaceableItem
    {
        private readonly GeneratorConfiguration<T> _configuration = configuration;
        public readonly Random _random = new(configuration.Seed);

        /// <summary>
        /// Generates a map on the given positions
        /// </summary>
        /// <param name="positions">The positions where you want to generate the map on</param>
        /// <returns>A finished map, grouped by biome layers</returns>
        public Dictionary<BiomeLayer, Dictionary<Position, T>> Run(IReadOnlyList<Position> positions) => RunWithIterations(positions).Last();
        /// <summary>
        /// Generates a map with the provided width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>A finished map, grouped by biome layers</returns>
        public Dictionary<BiomeLayer, Dictionary<Position, T>> RunForSize(int width, int height) => RunForSizeWithIterations(width, height).Last();

        /// <summary>
        /// Generates a map on the given positions and yields each generation of the simulation
        /// </summary>
        /// <param name="positions">The positions where you want to generate the map on</param>
        /// <returns>Each generation of the map on the provided positions</returns>
        public IEnumerable<Dictionary<BiomeLayer, Dictionary<Position, T>>> RunWithIterations(IReadOnlyList<Position> positions) 
        {
            Dictionary<Position, BiomeConfiguration<T>> layerMap = [];
            var sortedBiomes = _configuration.Biomes
                .OrderBy(x => x.InitialCreationChance)
                .ThenBy(x => x.Id).ToList();
            InitializeMap(layerMap, sortedBiomes, positions, true);

            for (var i = 0; i < _configuration.NumberOfSimulations; i++) 
            {
                var newMap = Simulate(layerMap, sortedBiomes, positions);
                layerMap = newMap;
            }

            var biomeGroups = layerMap.GroupBy(x => x.Value.Id);
            var map = new Dictionary<BiomeLayer, Dictionary<Position, T>>();
            foreach (var biomeGroup in biomeGroups) 
            {
                var biomePositions = biomeGroup.Select(x => x.Key).ToList();
                var biome = biomeGroup.First().Value;
                foreach (var sim in PopulateBiomes(biomePositions, biome)) 
                {
                    foreach (var layer in sim.Keys) 
                    {
                        var simLayer = sim[layer];
                        if (!map.TryGetValue(layer, out var mapLayer)) 
                        {
                            mapLayer = [];
                            map.Add(layer, mapLayer);
                        }
                        foreach (var position in simLayer.Keys) 
                        {
                            mapLayer[position] = simLayer[position];
                        }
                    }
                    yield return map.ToDictionary(x => x.Key, x => new Dictionary<Position, T>(x.Value));
                }
            }
        }
        /// <summary>
        /// Generates a map with the provided width and height, and yields each generation of the simulation
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Each generation of the map on the provided width and height</returns>
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
                var sortedPool = configuration.LayerItems[layerMap.Key]
                    .OrderBy(x => x.InitialCreationChance)
                    .ThenBy(x => x.Id).ToList();

                InitializeMap(layerMap.Value, sortedPool, positions, layerMap.Key.IsFillLayer);
            }

            for (var i = 0; i < configuration.NumberOfSimulations; i++)
            {
                Dictionary<BiomeLayer, Dictionary<Position, T>> newMaps = [];
                foreach (var map in layerMaps) 
                {
                    var sortedPool = configuration.LayerItems[map.Key]
                        .OrderBy(x => x.InitialCreationChance)
                        .ThenBy(x => x.Id).ToList();

                    newMaps[map.Key] = Simulate(map.Value, sortedPool, positions);
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
            if (shouldFill)
            {
                foreach (var position in positions)
                {
                    while (!TryAdd(position, map, pool)) ;
                }
            }
            else 
            {
                foreach (var position in positions)
                {
                    TryAdd(position, map, pool);
                }
            }
        }
        private bool TryAdd<TAddItem>(Position position, Dictionary<Position, TAddItem> map, IReadOnlyList<TAddItem> pool) where TAddItem : IBiomeItem
        {
            foreach (var option in pool)
            {
                if (_random.NextDouble() > option.InitialCreationChance)
                {
                    continue;
                }

                map.Add(position, option);
                return true;
            }
            return false;
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
    }
}
