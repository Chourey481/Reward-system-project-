CREATE DATABASE IF NOT EXISTS RewardSystemDB;
USE RewardSystemDB;

CREATE TABLE IF NOT EXISTS Members (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  MobileNumber VARCHAR(15) NOT NULL UNIQUE,
  Name VARCHAR(100),
  Otp VARCHAR(10),
  IsVerified BOOLEAN DEFAULT FALSE,
  PasswordHash VARCHAR(256),
  TotalPoints INT DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Coupons (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  MemberId INT,
  Code VARCHAR(100),
  Amount INT,
  RedeemedAt DATETIME,
  FOREIGN KEY (MemberId) REFERENCES Members(Id)
);

-- sample member (mobile: 9999999999, verified, 1000 points)
INSERT INTO Members (MobileNumber, Name, Otp, IsVerified, PasswordHash, TotalPoints)
VALUES ('9999999999', 'Sample User', '1234', TRUE, 'REPLACE_WITH_HASH', 1000);
