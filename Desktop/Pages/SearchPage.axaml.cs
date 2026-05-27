using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Desktop.Services;

namespace Desktop;

public record SkillItem(string Name);

public partial class SearchPage : UserControl
{
    public ObservableCollection<SkillItem> FilterSkills { get; set; } = new();
    public int PageSize = 25;

    public SearchPage()
    {
        InitializeComponent();

        SkillsItemsControl.ItemsSource = FilterSkills;

        SkillSearchBox.AsyncPopulator = async (searchText, _) =>
        {
            var res = await ApiService.Request<List<string>>(HttpMethod.Get, $"Skills/suggest?q={searchText}");
            return res.Data ?? new List<string>();
        };
    }

    private void AddSkill_Click(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SkillSearchBox.Text))
        {
            FilterSkills.Add(new SkillItem(SkillSearchBox.Text));
            SkillSearchBox.Text = "";
        }
    }

    private void RemoveSkill_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is SkillItem item)
            FilterSkills.Remove(item);
    }

    private async void Search_Click(object? sender, RoutedEventArgs e)
    {
        var skills = string.Join(",", FilterSkills.Select(s => s.Name));
        var url = $"Users/search?skills=1&level=0&exp=0&rating=0&limit=25";

        var res = await ApiService.Request<List<UserSearchResult>>(HttpMethod.Get, url);
        if (res.Data != null)
        {
            for (int i = 0; i < res.Data.Count; i++) res.Data[i].Index = i + 1;
            UsersGrid.ItemsSource = res.Data;
        }
    }

    private void Clear_Click(object? sender, RoutedEventArgs e)
    {
        FilterSkills.Clear();
        LevelNum.Value = 0; ExpNum.Value = 0; RatingNum.Value = 0;
        UsersGrid.ItemsSource = null;
    }

    private void ExportCsv_Click(object? sender, RoutedEventArgs e)
    {
        var items = UsersGrid.ItemsSource as List<UserSearchResult>;
        if (items == null) return;

        var csv = "No;ID;Skills;Rating\n" + string.Join("\n", items.Select(i => $"{i.Index};{i.PublicId};{i.SkillsDisplay};{i.Rating}"));
        File.WriteAllText("search_results.csv", csv);
    }

    private void SetPageSize_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            PageSize = int.Parse(btn.Tag.ToString());
            Search_Click(null, null);
        }
    }

    private void Detail_Click(object? sender, RoutedEventArgs e)
    {
        var user = (sender as Button)?.Tag as UserSearchResult;

        var window = new UserProfileWindow(int.Parse(user.PublicId.Substring(5)));
        window.ShowDialog(App.MainWindow);
    }
}

public class UserSearchResult {
    public int Index { get; set; }
    public string PublicId { get; set; }
    public List<string> Skills { get; set; }
    public int Rating { get; set; }
    public int Trend { get; set; } // 1, 0, -1
    public string SkillsDisplay => string.Join(", ", Skills);
    public string RatingWithTrend => $"{Rating}% {(Trend > 0 ? "📈" : Trend < 0 ? "📉" : "➡️")}";
}