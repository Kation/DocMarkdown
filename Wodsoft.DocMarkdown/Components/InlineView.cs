using Microsoft.AspNetCore.Components;

namespace Wodsoft.DocMarkdown.Components
{
    public class InlineView : ComponentBase
    {
        [Parameter]
        public string Content { get; set; }
    }
}
