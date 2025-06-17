CREATE DATABASE HospitalAppointmentBooking;
GO
USE HospitalAppointmentBooking;
GO

-- Doctors Table
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL
);
GO

-- Add Doctor
CREATE PROC SP_AddDoctor
@Name NVARCHAR(100),
@Specialization NVARCHAR(100)
AS
BEGIN
    INSERT INTO Doctors (Name, Specialization)
    VALUES (@Name, @Specialization)
END
GO

-- Get All Doctors
CREATE PROC SP_GetAllDoctors
AS
BEGIN
    SELECT * FROM Doctors
END
GO

-- Get Doctor By Id
CREATE PROC SP_GetDoctorById
@DoctorId INT
AS
BEGIN
    SELECT * FROM Doctors WHERE DoctorId = @DoctorId
END
GO

-- Update Doctor
CREATE PROC SP_UpdateDoctor
@DoctorId INT,
@Name NVARCHAR(100),
@Specialization NVARCHAR(100)
AS
BEGIN
    UPDATE Doctors
    SET Name = @Name, Specialization = @Specialization
    WHERE DoctorId = @DoctorId
END
GO

-- Delete Doctor
CREATE PROC SP_DeleteDoctor
@DoctorId INT
AS
BEGIN
    DELETE FROM Doctors WHERE DoctorId = @DoctorId
END
GO


-- Patients Table
CREATE TABLE Patients (
    PatientId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- Add Patient
CREATE PROC SP_AddPatient
@Name NVARCHAR(100),
@Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Patients (Name, Email)
    VALUES (@Name, @Email)
END
GO

-- Get All Patients
CREATE PROC SP_GetAllPatients
AS
BEGIN
    SELECT * FROM Patients
END
GO

-- Get Patient By Id
CREATE PROC SP_GetPatientById
@PatientId INT
AS
BEGIN
    SELECT * FROM Patients WHERE PatientId = @PatientId
END
GO

-- Update Patient
CREATE PROC SP_UpdatePatient
@PatientId INT,
@Name NVARCHAR(100),
@Email NVARCHAR(100)
AS
BEGIN
    UPDATE Patients
    SET Name = @Name, Email = @Email
    WHERE PatientId = @PatientId
END
GO

-- Delete Patient
CREATE PROC SP_DeletePatient
@PatientId INT
AS
BEGIN
    DELETE FROM Patients WHERE PatientId = @PatientId
END
GO


-- Appointments Table
CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),
    DoctorId INT NOT NULL,
    PatientId INT NOT NULL,
    Date DATETIME NOT NULL,
    Remarks NVARCHAR(255),

    -- Foreign Keys
    CONSTRAINT FK_Appointment_Doctor FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId),
    CONSTRAINT FK_Appointment_Patient FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);
GO

-- Add Appointment
CREATE PROC SP_AddAppointment
@DoctorId INT,
@PatientId INT,
@Date DATETIME,
@Remarks NVARCHAR(255)
AS
BEGIN
    INSERT INTO Appointments (DoctorId, PatientId, Date, Remarks)
    VALUES (@DoctorId, @PatientId, @Date, @Remarks)
END
GO

-- Get All Appointments (with join for DTO-style)
CREATE PROC SP_GetAllAppointments
AS
BEGIN
    SELECT 
        a.AppointmentId,
        a.Date,
        a.Remarks,
        d.DoctorId,
        d.Name AS DoctorName,
        d.Specialization,
        p.PatientId,
        p.Name AS PatientName,
        p.Email
    FROM Appointments a
    JOIN Doctors d ON a.DoctorId = d.DoctorId
    JOIN Patients p ON a.PatientId = p.PatientId
END
GO

-- Get Appointment By Id (with join)
CREATE PROC SP_GetAppointmentById
@AppointmentId INT
AS
BEGIN
    SELECT 
        a.AppointmentId,
        a.Date,
        a.Remarks,
        d.DoctorId,
        d.Name AS DoctorName,
        d.Specialization,
        p.PatientId,
        p.Name AS PatientName,
        p.Email
    FROM Appointments a
    JOIN Doctors d ON a.DoctorId = d.DoctorId
    JOIN Patients p ON a.PatientId = p.PatientId
    WHERE a.AppointmentId = @AppointmentId
END
GO

-- Update Appointment
CREATE PROC SP_UpdateAppointment
@AppointmentId INT,
@DoctorId INT,
@PatientId INT,
@Date DATETIME,
@Remarks NVARCHAR(255)
AS
BEGIN
    UPDATE Appointments
    SET DoctorId = @DoctorId,
        PatientId = @PatientId,
        Date = @Date,
        Remarks = @Remarks
    WHERE AppointmentId = @AppointmentId
END
GO

-- Delete Appointment
CREATE PROC SP_DeleteAppointment
@AppointmentId INT
AS
BEGIN
    DELETE FROM Appointments WHERE AppointmentId = @AppointmentId
END
GO

-- Get All Appointments (Raw - for IRepository<Appointment>)
CREATE PROC SP_GetAllAppointmentsRaw
AS
BEGIN
    SELECT AppointmentId, DoctorId, PatientId, Date, Remarks
    FROM Appointments
END
GO

-- Get Appointment By Id (Raw - for IRepository<Appointment>)
CREATE PROC SP_GetAppointmentByIdRaw
@AppointmentId INT
AS
BEGIN
    SELECT AppointmentId, DoctorId, PatientId, Date, Remarks
    FROM Appointments
    WHERE AppointmentId = @AppointmentId
END
GO
