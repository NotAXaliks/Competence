create table "Roles" (
    "Id" serial primary key,
    "Name" text not null unique
);

create table "Users" (
    "Id" serial primary key,
    "FirstName" text not null,
    "MiddleName" text not null,
    "LastName" text not null,
    "Email" text not null unique,
    "Password" text not null,
    "Phone" text not null unique,
    "RoleId" int not null references "Roles"("Id"),
    "Avatar" text,
    "CreatedAt" timestamp not null default now()
);

create table "Institutions" (
    "Id" serial primary key,
    "Name" text not null
);

create table "EducationType" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Education" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "InstitutionId" int not null references "Institutions"("Id"),
    "EducationTypeId" int not null references "EducationType"("Id"),
    "Order" int not null default 0,
    "StartDate" timestamp not null,
    "EndDate" timestamp
);

create table "Organizations" (
    "Id" serial primary key,
    "Name" text not null unique
);

create table "Experience" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "CompanyId" int not null references "Organizations"("Id"),
    "EmploymentType" text not null,
    "Description" text,
    "StartDate" timestamp not null,
    "EndDate" timestamp
);

create table "Skills" (
    "Id" serial primary key,
    "Name" text not null,
    "Category" text not null
);

create table "UserSkills" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "SkillId" int not null references "Skills"("Id"),
    "Level" int not null check ("Level" between 0 and 10),
    "ConfirmationsCount" int not null default 0,
    unique ("UserId", "SkillId")
);

create table "ConfirmationStatuses" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Confirmations" (
    "Id" serial primary key,
    "RequestorId" int not null references "Users"("Id"),
    "TargetId" int not null references "Users"("Id"),
    "SkillId" int not null references "UserSkills"("Id"),
    "StatusId" int not null references "ConfirmationStatuses"("Id"),
    "CreatedAt" timestamp not null default now(),
    "UpdatedAt" timestamp not null default now()
);

create table "UserRatings" (
    "UserId" int not null references "Users"("Id") primary key,
    "CompetenceIndex" numeric(5,2) not null default 0,
    "CommunityTrust" numeric(5,2) not null default 0,
    "LastUpdated" timestamp not null default now()
);

create table "RatingHistory" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "Value" numeric(5,2) not null,
    "Date" timestamp not null default now()
);

create table "ShortLists" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "Name" text not null,
    "CreatedAt" timestamp not null default now()
);

create table "ShortListCandidates" (
    "ShortListId" int not null references "ShortLists"("Id"),
    "UserId" int not null references "Users"("Id"),
    primary key ("ShortListId", "UserId")
);

create table "ApiLogs" (
    "Id" serial primary key,
    "UserId" int references "Users"("Id"),
    "Method" text not null,
    "Endpoint" text not null,
    "StatusCode" int not null,
    "DurationMs" int not null,
    "Date" timestamp not null default now()
);

create table "RoleHistory" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "OldRoleId" int not null references "Roles"("Id"),
    "NewRoleId" int not null references "Roles"("Id"),
    "Date" timestamp not null default now()
);
