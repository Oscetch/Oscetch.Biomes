using Oscetch.Biomes;
using System.ComponentModel;

namespace BiomeTester
{
    public class DrawControl : Control
    {
        private Dictionary<BiomeLayer, Dictionary<Position, BiomePlaceableItem>>? _map;

        [Browsable(false)]
        public Dictionary<BiomeLayer, Dictionary<Position, BiomePlaceableItem>>? Map 
        { 
            get => _map;
            set
            {
                _map = value;
                Invalidate();
            }
        }

        public int TileSize { get; set; } = 16;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(Color.Black);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_map == null) 
            {
                e.Graphics.Clear(Color.Black);
                base.OnPaint(e);
                return;
            }
            var keys = _map.Keys.OrderBy(x => x.Value);
            foreach (var key in keys) 
            {
                var items = Map![key];
                foreach (var position in items.Keys) 
                {
                    var x = position.X * TileSize;
                    var y = position.Y * TileSize;
                    e.Graphics.DrawImage(items[position].Image, new Rectangle(x, y, TileSize, TileSize));
                }
            }

            base.OnPaint(e);
        }
    }
}
