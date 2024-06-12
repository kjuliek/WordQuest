DROP DATABASE IF EXISTS wordquest;
CREATE DATABASE wordquest;
USE wordquest;

DROP TABLE IF EXISTS AspNetUsers;
CREATE TABLE AspNetUsers (
    Id VARCHAR(255) NOT NULL,
    UserName VARCHAR(256) NOT NULL,
    PasswordHash LONGTEXT NOT NULL,
    Email VARCHAR(256) NOT NULL,
    PhoneNumber LONGTEXT NOT NULL,
    NormalizedUserName VARCHAR(256),
    NormalizedEmail VARCHAR(256),
    EmailConfirmed TINYINT(1) NOT NULL,
    SecurityStamp LONGTEXT,
    ConcurrencyStamp LONGTEXT,
    PhoneNumberConfirmed TINYINT(1) NOT NULL,
    TwoFactorEnabled TINYINT(1) NOT NULL,
    LockoutEnd DATETIME(6),
    LockoutEnabled TINYINT(1) NOT NULL,
    AccessFailedCount INT NOT NULL,
    PRIMARY KEY (Id)
) CHARSET=utf8mb4;

DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
    user_id VARCHAR(255) NOT NULL,
    user_level INTEGER,
    user_xp INTEGER,
    FOREIGN KEY (user_id) REFERENCES AspNetUsers(id)
);

DROP TABLE IF EXISTS Words;
CREATE TABLE Words (
    word_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    fr_word VARCHAR(255) NOT NULL,
    en_word VARCHAR(255) NOT NULL
);

DROP TABLE IF EXISTS `Groups`;
CREATE TABLE `Groups` (
    group_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    group_name VARCHAR(255) NOT NULL,
    admin_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (admin_id) REFERENCES AspNetUsers(id)
);

DROP TABLE IF EXISTS Courses;
CREATE TABLE Courses (
    course_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    creator_id VARCHAR(255) NOT NULL,
    course_name VARCHAR(255) NOT NULL,
    course_description TEXT,
    course_level INTEGER,
    creation_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (creator_id) REFERENCES AspNetUsers(id)
);

DROP TABLE IF EXISTS Group_Users;
CREATE TABLE Group_Users (
    group_id INTEGER,
    user_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (group_id) REFERENCES Groups(group_id),
    FOREIGN KEY (user_id) REFERENCES AspNetUsers(id)
);

DROP TABLE IF EXISTS Group_Courses;
CREATE TABLE Group_Courses (
    group_id INTEGER,
    course_id INTEGER,
    FOREIGN KEY (group_id) REFERENCES Groups(group_id),
    FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);

DROP TABLE IF EXISTS Course_Words;
CREATE TABLE Course_Words (
    course_id INTEGER,
    word_id INTEGER,
    FOREIGN KEY (course_id) REFERENCES Courses(course_id),
    FOREIGN KEY (word_id) REFERENCES Words(word_id)
);

DROP TABLE IF EXISTS Learned_Words;
CREATE TABLE Learned_Words (
    user_id VARCHAR(255) NOT NULL,
    word_id INTEGER,
    learning_stage INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES AspNetUsers(id),
    FOREIGN KEY (word_id) REFERENCES Words(word_id)
);