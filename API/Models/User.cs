using System;
using System.Collections.Generic;

namespace API.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int RoleId { get; set; }

    public string Avatar { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApiLog> ApiLogs { get; set; } = new List<ApiLog>();

    public virtual ICollection<Confirmation> ConfirmationRequestors { get; set; } = new List<Confirmation>();

    public virtual ICollection<Confirmation> ConfirmationTargets { get; set; } = new List<Confirmation>();

    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();

    public virtual ICollection<RatingHistory> RatingHistories { get; set; } = new List<RatingHistory>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<RoleHistory> RoleHistories { get; set; } = new List<RoleHistory>();

    public virtual ICollection<ShortList> ShortLists { get; set; } = new List<ShortList>();

    public virtual UserRating? UserRating { get; set; }

    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

    public virtual ICollection<ShortList> ShortListsNavigation { get; set; } = new List<ShortList>();
}
