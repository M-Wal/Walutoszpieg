CREATE database Walutoszpieg

use Walutoszpieg

CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE wallets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CurrencyCode NVARCHAR(3) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES users(Id)
);

CREATE TABLE transaction_history (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    from_currency NVARCHAR(10) NOT NULL,
    to_currency NVARCHAR(10) NOT NULL,
    amount DECIMAL(18, 2) NOT NULL,
    rate DECIMAL(18, 6) NOT NULL,
    timestamp DATETIME NOT NULL
);

INSERT INTO users (username, email, password_hash)
VALUES ('Marcin', 'Marcin@op.pl', 'QW!QW!'),
 ('Radek', 'Radeczek@wp.pl', 'ZX#ZX#');