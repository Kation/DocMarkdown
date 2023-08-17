namespace Wodsoft.DocMarkdown
{
    public class NavItemChangedEventArgs : EventArgs
    {
        public NavItemChangedEventArgs(NavItem item)
        {
            Item = item;
        }

        public NavItem Item { get; }
    }
}
