CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(500) COMMENT 'User Cover Image' DEFAULT 'https://images.unsplash.com/photo-1599508704512-2f19efd1e35f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8JTNGfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60',
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT NOT NULL DEFAULT 0,
        kept INT NOT NULL DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500) NOT NULL DEFAULT 'No description for this vault',
        img VARCHAR(500) NOT NULL,
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorID VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

SELECT
vaultKeeps.*,
vaults.*,
keeps.*
FROM vaultKeeps
JOIN vaults ON vaultKeeps.vaultId = vaults.id
JOIN keeps ON vaultKeeps.keepId = keeps.id
WHERE vaults.id = 43;