create table "Roles" (
    "Id" serial primary key,
    "Name" text not null unique
);

create table "Users" (
    "Id" serial primary key,
    "Name" text not null,
    "Email" text not null unique,
    "Password" text not null,
    "Phone" text not null unique,
    "RoleId" int not null references "Roles"("Id"),
    "Avatar" text not null,
    "PinCode" text not null,
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

create table "Organizations" (
    "Id" serial primary key,
    "Name" text not null unique
);

create table "Education" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "InstitutionId" int not null references "Institutions"("Id"),
    "EducationTypeId" int not null references "EducationType"("Id"),
    "Order" int not null default 0,
    "StartDate" timestamp not null,
    "EndDate" timestamp,
    constraint check_exp_dates check ("EndDate" >= "StartDate" or "EndDate" is null)
);

create table "Experience" (
    "Id" serial primary key,
    "UserId" int not null references "Users"("Id"),
    "CompanyId" int not null references "Organizations"("Id"),
    "EmploymentType" text not null,
    "Description" text,
    "StartDate" timestamp not null,
    "EndDate" timestamp,
    constraint check_exp_dates check ("EndDate" >= "StartDate" or "EndDate" is null)
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
    "UpdatedAt" timestamp not null default now(),
    constraint no_self_confirm check ("RequestorId" <> "TargetId")
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
    "HRId" int not null references "Users"("Id"),
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

CREATE OR REPLACE FUNCTION check_experience_overlap() RETURNS TRIGGER AS $$
BEGIN
    IF EXISTS (
        SELECT 1 FROM "Experience" 
        WHERE "UserId" = NEW."UserId"
        AND "Id" <> NEW."Id"
        AND (NEW."StartDate", COALESCE(NEW."EndDate", '9999-12-31')) 
            OVERLAPS ("StartDate", COALESCE("EndDate", '9999-12-31'))
    ) THEN
        RAISE EXCEPTION 'Периоды опыта работы не должны пересекаться';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_exp_overlap BEFORE INSERT OR UPDATE ON "Experience"
FOR EACH ROW EXECUTE FUNCTION check_experience_overlap();

-- CREATE OR REPLACE FUNCTION update_user_rating() RETURNS TRIGGER AS $$
-- BEGIN
--     IF NEW.status = 'accepted' THEN
--         -- Пример упрощенной логики пересчета
--         UPDATE user_skills SET confirmations_count = confirmations_count + 1 WHERE id = NEW.skill_id;
        
--         -- Обновление индекса в таблице ratings (здесь должна быть формула: образование * опыт * подтверждения) [26]
--         UPDATE ratings SET 
--             competence_index = competence_index + 1.5,
--             last_updated = CURRENT_TIMESTAMP
--         WHERE user_id = (SELECT user_id FROM user_skills WHERE id = NEW.skill_id);
        
--         -- Запись в историю [22]
--         INSERT INTO rating_history (user_id, rating_value)
--         SELECT user_id, competence_index FROM ratings WHERE user_id = (SELECT user_id FROM user_skills WHERE id = NEW.skill_id);
--     END IF;
--     RETURN NEW;
-- END;
-- $$ LANGUAGE plpgsql;

-- CREATE TRIGGER trg_after_confirm AFTER UPDATE OF status ON confirmations
-- FOR EACH ROW WHEN (NEW.status = 'accepted') EXECUTE FUNCTION update_user_rating();

CREATE OR REPLACE FUNCTION log_role_change() RETURNS TRIGGER AS $$
BEGIN
    IF OLD."RoleId" <> NEW."RoleId" THEN
        INSERT INTO "RoleHistory" ("UserId", "OldRoleId", "NewRoleId")
        VALUES (OLD."Id", OLD."RoleId", NEW."RoleId");
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_role_history AFTER UPDATE ON "Users"
FOR EACH ROW EXECUTE FUNCTION log_role_change();
