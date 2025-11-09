using System.Diagnostics.CodeAnalysis;

namespace Oscetch.Biomes
{
    /// <summary>
    /// This represent a value where different placeable items will compete for space inside a biome
    /// </summary>
    /// <param name="value"></param>
    public readonly struct BiomeLayer(int value)
    {
        public int Value { get; } = value;
        public bool IsFillLayer { get; } = value == FillLayerValue;

        public static implicit operator BiomeLayer(int value) => new (value);

        public const int FillLayerValue = -1;
        /// <summary>
        /// This layer will tell the generator to fill this biome completely
        /// </summary>
        public static readonly BiomeLayer FillLayer = new (FillLayerValue);

        public override string ToString() 
        {
            return IsFillLayer ? "Fill" : Value.ToString();
        }
    }
}
