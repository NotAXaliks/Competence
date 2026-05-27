using System;
using System.IO;
using System.Net.Http;
using API.Models;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Desktop.Services;

namespace Desktop.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
App.MainWindow = this;
        UpdateUser();
        SelectMainPage();
    }

    public async void UpdateUser()
    {
        var user = await ApiService.Request<User>(HttpMethod.Get, "Users/me");

        UserNameTextBlock.Text = $"{user.Data!.Name} ({user.Data!.Role.Name})";

        var bytes = Convert.FromBase64String(user.Data!.Avatar);
        using var stream = new MemoryStream(bytes);
        UserAvatar.Source = new Bitmap(stream);
    }

    public void SelectMainPage()
    {
        UContent.Content = new MainPage();
    }

    public void SelectSearchPage()
    {
        UContent.Content = new SearchPage();
    }

    public void SelectCompetencePage()
    {
        // UContent.Content = new CompetencePage();
    }

    private void MainPageSelected(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SelectMainPage();
    }

    private void SearchPageSelected(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SelectSearchPage();
    }

    private void CompetencePageSelected(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SelectCompetencePage();
    }
}