using Markdig.Extensions.TaskLists;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class TaskListRenderer : MarkdownRenderer<TaskList>
    {
        protected override object Render(IMarkdownRenderContext context, TaskList markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<TaskListView>(0);
                builder.AddAttribute(1, nameof(TaskListView.IsChecked), markdown.Checked);
                builder.CloseComponent();
            });
        }
    }
}
