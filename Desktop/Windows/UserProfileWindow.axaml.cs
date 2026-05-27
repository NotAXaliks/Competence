using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using API.Controllers;
using API.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Desktop.Services;

namespace Desktop;

public partial class UserProfileWindow : Window
{
    private int _userId;

    public UserProfileWindow(int userId)
    {
        InitializeComponent();
        _userId = userId;
        LoadProfile();
    }

    private async void LoadProfile()
    {
        var profile = await ApiService.Request<UserEmployeeData>(HttpMethod.Get, $"users/{_userId}?mode=employer");
        var rating = await ApiService.Request<UserRating>(HttpMethod.Get, $"users/{_userId}/rating");

        if (profile.Data != null)
        {
            PublicIdText.Text = $"#ITP-{profile.Data.Id}";
            AnonymNameText.Text = $"{profile.Data.Name}. (анонимный профиль)";

            var bytes = Convert.FromBase64String(profile.Data!.Avatar);
            using var stream = new MemoryStream(bytes);
            UserAvatar.Source = new Bitmap(stream);
            
            var skills = profile.Data.Skills
                .OrderByDescending(s => s.Level)
                .Select(s => new {
                    s.Skill.Name,
                    LevelDisplay = $"{s.Level}/10",
                    ConfMarks = new string('✓', int.Min(s.ConfirmationsCount, 4))
                }).ToList();
            SkillsGrid.ItemsSource = skills;

            ExpItems.ItemsSource = profile.Data.Experiences.Select(e => new {
                DisplayText = $"{e.StartDate:yyyy}–{e.EndDate?.ToString("yyyy") ?? "2026"} {e.Company.Name}",
            });
        }

        if (rating.Data != null)
        {
            IndexBar.Value = (double)rating.Data.CompetenceIndex;
            IndexVal.Text = $"{rating.Data.CompetenceIndex}%";
            TrustBar.Value = (double)rating.Data.CommunityTrust;
            TrustVal.Text = $"{rating.Data.CommunityTrust}%";
            StatsSummary.Text = $"Подтверждений: {rating.Data.CompetenceIndex} | Навыков: {profile.Data.Skills.Count()}";
        }
    }

    private void Back_Click(object? s, RoutedEventArgs e) => Close();
}