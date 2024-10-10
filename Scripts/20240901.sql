CREATE TABLE Appointments (
    AppointId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    CustMyKad CHAR(12) NOT NULL,
    LaserName VARCHAR(255) NOT NULL,
	LaserPrice DECIMAL NOT NULL,
    StaffId VARCHAR(255) NOT NULL,
    AppointDate DATE NOT NULL,
    AppointTime VARCHAR(20) NOT NULL,
    Status VARCHAR(20) NOT NULL,
 
    FOREIGN KEY (CustMyKad) REFERENCES Customers(CustMyKad),
    FOREIGN KEY (LaserName) REFERENCES Lasers(LaserName),
    FOREIGN KEY (StaffId) REFERENCES Staffs(StaffId)
);
 
CREATE TABLE Lasers (
    LaserName VARCHAR(255) PRIMARY KEY NOT NULL, 
    LaserDesc VARCHAR(255) NOT NULL,  
    NormalPrice DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(10, 2) NOT NULL,  
    LaserPrice DECIMAL(10, 2) NOT NULL 
);
 
CREATE TABLE Customers (
    CustMyKad CHAR(12) PRIMARY KEY NOT NULL, 
    Email NVARCHAR(255) NOT NULL,  
    CustName NVARCHAR(255) NOT NULL,  
    Gender VARCHAR(6) NOT NULL,   
    PhoneNo VARCHAR(12) NOT NULL,     
    BirthDate DATE NOT NULL,  
    RegisterDate DATETIME2 NOT NULL DEFAULT GETDATE(), 
    Password NVARCHAR(255) NOT NULL 
);
 
CREATE TABLE Staffs (
    StaffId VARCHAR(255) PRIMARY KEY NOT NULL,
    Position VARCHAR(255) NOT NULL,    
    MyKad CHAR(12) UNIQUE NOT NULL,  
    Email VARCHAR(255) NOT NULL,      
    StaffName VARCHAR(255) NOT NULL,  
    Gender VARCHAR(6) NOT NULL,  
    PhoneNo VARCHAR(12) NOT NULL, 
    BirthDate DATE NOT NULL,  
    RegisterDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    HireDate DATE NOT NULL,  
    Password VARCHAR(255) NOT NULL,
	Status VARCHAR(8) NOT NULL
);