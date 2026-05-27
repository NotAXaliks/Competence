using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using API.Controllers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Desktop.Services;


namespace Desktop;

public partial class MainPage : UserControl
{
    public ObservableCollection<dynamic> TopSkills { get; } = new();

    public MainPage()
    {
        InitializeComponent();

        TopSkillsControl.ItemsSource = TopSkills;

        var timer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(5) };
        timer.Tick += (s, e) => LoadData();
        timer.Start();

        LoadData();
    }

    public async void LoadData()
    {
        var resp = await ApiService.Request<DashboardSummary>(HttpMethod.Get, "Dashboard/summary");

        ActiveProfilesTextBlock.Text = resp.Data.ActiveProfiles;
        JobMatchTextBlock.Text = resp.Data.JobMatch;
        AvgRatingTextBlock.Text = resp.Data.AvgRating;

        var resSkills = await ApiService.Request<List<dynamic>>(HttpMethod.Get, "dashboard/skills/top");
        TopSkills.Clear();
        if (resSkills.Data != null)
            foreach (var s in resSkills.Data) TopSkills.Add(s);
    }
}