-- Tabela: users
CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Tabela: currencies
CREATE TABLE currencies (
    id INT PRIMARY KEY IDENTITY(1,1),
    currency_code VARCHAR(10) UNIQUE NOT NULL,
    currency_name VARCHAR(50) NOT NULL
);

-- Tabela: exchange_rates
CREATE TABLE exchange_rates (
    id INT PRIMARY KEY IDENTITY(1,1),
    currency_id INT FOREIGN KEY REFERENCES currencies(id),
    rate DECIMAL(18,8) NOT NULL,
    timestamp DATETIME NOT NULL
);

-- Tabela: user_preferences
CREATE TABLE user_preferences (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT FOREIGN KEY REFERENCES users(id),
    currency_id INT FOREIGN KEY REFERENCES currencies(id),
    alert_threshold DECIMAL(18,8),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Tabela: alerts
CREATE TABLE alerts (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT FOREIGN KEY REFERENCES users(id),
    currency_id INT FOREIGN KEY REFERENCES currencies(id),
    alert_type VARCHAR(20) NOT NULL,
    threshold DECIMAL(18,8) NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    notified_at DATETIME
);

-- Tabela: transaction_history
CREATE TABLE transaction_history (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT FOREIGN KEY REFERENCES users(id),
    currency_from_id INT FOREIGN KEY REFERENCES currencies(id),
    currency_to_id INT FOREIGN KEY REFERENCES currencies(id),
    amount DECIMAL(18,8) NOT NULL,
    rate DECIMAL(18,8) NOT NULL,
    timestamp DATETIME NOT NULL
);

-- Tabela: historical_exchange_rates
CREATE TABLE historical_exchange_rates (
    id INT PRIMARY KEY IDENTITY(1,1),
    currency_id INT FOREIGN KEY REFERENCES currencies(id),
    rate DECIMAL(18,8) NOT NULL,
    timestamp DATETIME NOT NULL
);

-- Tabela: notifications
CREATE TABLE notifications (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT FOREIGN KEY REFERENCES users(id),
    message TEXT NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);