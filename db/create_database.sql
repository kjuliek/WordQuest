DROP DATABASE IF EXISTS wordquest;
CREATE DATABASE wordquest;
USE wordquest;

DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
    user_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    user_name VARCHAR(255) NOT NULL,
    user_password VARCHAR(255) NOT NULL,
    user_email VARCHAR(255) NOT NULL,
    user_level INTEGER,
    user_xp INTEGER
);

DROP TABLE IF EXISTS Words;
CREATE TABLE Words (
    word_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    fr_word VARCHAR(255) NOT NULL,
    en_word VARCHAR(255) NOT NULL
);

DROP TABLE IF EXISTS Groups;
CREATE TABLE Groups (
    group_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    group_name VARCHAR(255) NOT NULL,
    admin_id INTEGER,
    FOREIGN KEY (admin_id) REFERENCES Users(user_id)
);

DROP TABLE IF EXISTS Courses;
CREATE TABLE Courses (
    course_id INTEGER PRIMARY KEY AUTO_INCREMENT,
    creater_id INTEGER,
    course_name VARCHAR(255) NOT NULL,
    course_description TEXT,
    course_level INTEGER,
    creation_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (creater_id) REFERENCES Users(user_id)
);

DROP TABLE IF EXISTS Group_Users;
CREATE TABLE Group_Users (
    group_id INTEGER,
    user_id INTEGER,
    FOREIGN KEY (group_id) REFERENCES Groups(group_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
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
    user_id INTEGER,
    word_id INTEGER,
    learning_stage INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (word_id) REFERENCES Words(word_id)
);