```csharp
var pixelSize = new PixelSize((int)control.Bounds.Width, (int)control.Bounds.Height);
using var bitmap = new RenderTargetBitmap(pixelSize);
bitmap.Render(control);

using var stream = new MemoryStream();
bitmap.Save(stream);
return stream.ToArray();

public void GeneratePdfFromImage(byte[] imageData, string filePath)
{
    Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);

            page.Content().Image(imageData); // Inserts the screenshot
        });
    })
    .GeneratePdf(filePath);
}

```


```xaml
<ItemsControl Name="SkillsItemsControl">
            <ItemsControl.ItemsPanel>
              <ItemsPanelTemplate><WrapPanel/></ItemsPanelTemplate>
            </ItemsControl.ItemsPanel>
            <ItemsControl.ItemTemplate>
              <DataTemplate>
                <Button Content="{Binding Name, StringFormat='x {0}'}"
                        Click="RemoveSkill_Click" Margin="2"/>
              </DataTemplate>
            </ItemsControl.ItemTemplate>
          </ItemsControl>

 <AutoCompleteBox Name="SkillSearchBox" Width="250" FilterMode="None" />

 SkillSearchBox.AsyncPopulator = async (searchText, _) =>
        {
            var res = await ApiService.Request<List<string>>(HttpMethod.Get, $"Skills/suggest?q={searchText}");
            return res.Data ?? new List<string>();
        };


  <ComboBox Name="SelectionsCombo" Width="250" PlaceholderText="Выберите подборку" SelectionChanged="SelectionsCombo_SelectionChanged">
          <ComboBox.ItemTemplate>
            <DataTemplate>
              <TextBlock Text="{Binding Name}" />
            </DataTemplate>
          </ComboBox.ItemTemplate>
        </ComboBox>
```