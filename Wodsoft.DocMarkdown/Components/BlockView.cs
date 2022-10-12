using Microsoft.AspNetCore.Components;

namespace Wodsoft.DocMarkdown.Components
{
    public class BlockView : ComponentBase
    {
        [Parameter]
        public RenderFragment Content { get; set; }
    }
}
