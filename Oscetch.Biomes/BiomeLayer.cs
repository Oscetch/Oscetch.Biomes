using System.Diagnostics.CodeAnalysis;

namespace Oscetch.Biomes
{
    public readonly struct BiomeLayer(int value)
    {
        public int Value { get; } = value;
        public bool IsFillLayer { get; } = value == FillLayerValue;

        public static implicit operator BiomeLayer(int value) => new (value);

        public const int FillLayerValue = -1;
        public static readonly BiomeLayer FillLayer = new (FillLayerValue);

        public override string ToString() 
        {
            return IsFillLayer ? "Fill" : Value.ToString();
        }
    }
}
