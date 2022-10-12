using System.Collections.ObjectModel;

namespace Wodsoft.DocMarkdown.Services
{
    public class CatalogManager
    {
        private List<Catalog> _values;

        public CatalogManager()
        {
            _values = new List<Catalog>();
            Values = new ReadOnlyCollection<Catalog>(_values);
        }

        public void Clear()
        {
            _values.Clear();
            OnChanged?.Invoke(this, new EventArgs());
        }

        public void Add(Catalog item)
        {
            _values.Add(item);
            OnChanged?.Invoke(this, new EventArgs());
        }

        public IReadOnlyList<Catalog> Values { get; }

        public event EventHandler OnChanged;
    }

    public class Catalog
    {
        public int Level { get; set; }

        public string Name { get; set; }

        public string Anchor { get; set; }
    }
}
