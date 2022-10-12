using Markdig.Extensions.Tables;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class TableRenderer : MarkdownRenderer<Table>
    {
        protected override object Render(IMarkdownRenderContext context, Table markdown)
        {
            bool hasColumnWidth = false;
            foreach (var tableColumnDefinition in markdown.ColumnDefinitions)
            {
                if (tableColumnDefinition.Width != 0.0f && tableColumnDefinition.Width != 1.0f)
                {
                    hasColumnWidth = true;
                    break;
                }
            }
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<TableView>(0);
                if (hasColumnWidth)
                {
                    List<string> widths = new List<string>();
                    foreach (var tableColumnDefinition in markdown.ColumnDefinitions)
                    {
                        var width = Math.Round(tableColumnDefinition.Width * 100) / 100;
                        var widthValue = string.Format(CultureInfo.InvariantCulture, "{0:0.##}", width);
                        widths.Add(widthValue);
                    }
                    builder.AddAttribute(1, nameof(TableView.Widths), widths.ToArray());
                }
                builder.AddAttribute(hasColumnWidth ? 2 : 1, nameof(BlockView.Content), (RenderFragment)(rowBuilder =>
                {
                    int rowIndex = 0;
                    bool firstHeader = true;
                    bool firstBody = true;
                    foreach (TableRow row in markdown)
                    {
                        if (row.IsHeader && firstHeader)
                        {
                            rowBuilder.OpenElement(rowIndex++, "thead");
                            firstHeader = false;
                        }
                        if (!row.IsHeader && firstBody)
                        {
                            if (!firstHeader)
                                rowBuilder.CloseElement();
                            rowBuilder.OpenElement(rowIndex++, "tbody");
                            firstBody = false;
                        }
                        rowBuilder.OpenComponent<TableRowView>(rowIndex++);
                        rowBuilder.AddAttribute(rowIndex++, nameof(BlockView.Content), (RenderFragment)(cellBuilder =>
                        {
                            int cellIndex = 0;
                            for (int i = 0; i < row.Count; i++)
                            {
                                var cell = (TableCell)row[i];
                                var columnIndex = cell.ColumnIndex < 0 || cell.ColumnIndex >= markdown.ColumnDefinitions.Count ? i : cell.ColumnIndex;
                                columnIndex = columnIndex >= markdown.ColumnDefinitions.Count ? markdown.ColumnDefinitions.Count - 1 : columnIndex;
                                var cellDefinition = markdown.ColumnDefinitions[columnIndex];
                                cellBuilder.OpenComponent<TableCellView>(cellIndex++);
                                cellBuilder.AddAttribute(cellIndex++, nameof(TableCellView.IsHeader), row.IsHeader);
                                cellBuilder.AddAttribute(cellIndex++, nameof(TableCellView.ColumnSpan), cell.ColumnSpan);
                                cellBuilder.AddAttribute(cellIndex++, nameof(TableCellView.RowSpan), cell.RowSpan);
                                cellBuilder.AddAttribute(cellIndex++, nameof(TableCellView.Align), cellDefinition.Alignment ?? TableColumnAlign.Left);
                                cellBuilder.AddAttribute(cellIndex++, nameof(BlockView.Content), context.RenderBlock(cell));
                                cellBuilder.CloseComponent();
                            }
                        }));
                        rowBuilder.CloseComponent();
                    }
                    if (!firstBody || !firstHeader)
                        rowBuilder.CloseElement();
                }));
                builder.CloseComponent();
            });
        }
    }
}
