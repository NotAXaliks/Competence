using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using API.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Desktop.Services;
using MsBox.Avalonia;
using Tmds.DBus.Protocol;

namespace Desktop;

public partial class ShortlistsPage : UserControl
{
    public ShortlistsPage()
    {
        InitializeComponent();

        UpdateShortLists();
    }

    public async void UpdateShortLists()
    {
        var resp = await ApiService.Request<List<ShortList>>(HttpMethod.Get, "Shortlists");
        if (!resp.Success)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", resp.Error);
            await box.ShowAsync();
            return;
        }

        SelectionsCombo.ItemsSource = resp.Data!;
    }

    private void SelectionsCombo_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var list = (ShortList)SelectionsCombo.SelectedItem!;

        SelectionStats.Text = $"Кандидатов: {list.Users.Count} | Создано: {list.CreatedAt}";

        // var candidates = new ObservableCollection<Candidate>();

        // CandidatesGrid.ItemsSource = candidates;

        // var index = 0;
        // foreach (var candidate in list.Users)
        // {
        //     candidates.Add(new Candidate(++index, $"ITP-{candidate.Id}", $"{candidate.UserRating?.CompetenceIndex ?? 0}%"));
        // }
    }
    
    private void UsersGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        // var newWindow = new UserProfile();
        // newWindow.Show();
    }
}