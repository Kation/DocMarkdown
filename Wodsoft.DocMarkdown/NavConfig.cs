using System.Collections.ObjectModel;

namespace Wodsoft.DocMarkdown
{
    public class NavConfig
    {
        public string Path { get; set; }

        public Dictionary<string, NavConfig> Children { get; set; }
    }

    public class NavModel
    {
        public NavModel(IReadOnlyList<NavItem> items, IReadOnlyList<NavItem> allItems)
        {
            Items = items;
            AllItems = allItems;
        }

        public IReadOnlyList<NavItem> Items { get; }

        public IReadOnlyList<NavItem> AllItems { get; }
    }

    public class NavItem
    {
        public NavItem(string name, string path, NavItem parent)
        {
            Name = name;
            Path = path;
            _children = new List<NavItem>();
            Children = new ReadOnlyCollection<NavItem>(_children);
            if (parent != null)
            {
                parent._children.Add(this);
                Parent = parent;
            }
        }

        public string Name { get; }

        public string Path { get; }

        public bool HasContent => Path != null;

        public NavItem Parent { get; }

        private List<NavItem> _children;
        public IReadOnlyList<NavItem> Children { get; }
    }
}
