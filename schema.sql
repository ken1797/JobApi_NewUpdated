-- SQLite schema (optional if you use migrations on startup)
CREATE TABLE IF NOT EXISTS Departments(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT,
    IsDeleted INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Locations(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    City TEXT NOT NULL,
    State TEXT NOT NULL,
    Country TEXT NOT NULL,
    Zip TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT,
    IsDeleted INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Jobs(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code TEXT NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT NOT NULL,
    LocationId INTEGER NOT NULL,
    DepartmentId INTEGER NOT NULL,
    PostedDate TEXT NOT NULL,
    ClosingDate TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT,
    IsDeleted INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY(LocationId) REFERENCES Locations(Id),
    FOREIGN KEY(DepartmentId) REFERENCES Departments(Id)
);
