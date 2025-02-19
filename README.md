# Carpark Info Assignment 🚗

This project processes car park information using a **batch job** and provides a **REST API** for accessing car park data.

## 📂 Repository Structure

After cloning the repository, pay attention to these key folders:

1. **`csv/`** – Contains the car park dataset in CSV format.
2. **`carparks.db`** – The SQLite database storing car park data (only table structures with one dummy user record).
3. **`ERD/`** – The Entity Relationship Diagram (ERD) for Task 1.
4. **`CarparkInfoBatchJob/`** – The batch job for processing CSV data (Task 2).
5. **`CarparkInfoAPI/`** – The REST API for querying car park data (Task 3).

## 🚀 How to Run

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/kmgan/carpark-info-assignment.git
cd carpark-info-assignment
```

### 2️⃣ Run the Batch Job (Task 2)
- Navigate to the **`CarparkInfoBatchJob/`** folder.
- Open `Program.cs` in Visual Studio.
- Press **F5** or click the **Run** button.
- **Note:** The database initially only has table structures with one dummy user record. Run the batch job first to insert car park data.

### 3️⃣ Run the API (Task 3)
- Navigate to the **`CarparkInfoAPI/`** folder.
- Open `Program.cs` in Visual Studio.
- Press **F5** or click the **Run** button.

## 📝 Notes
- Ensure **.NET 6** is installed on your machine.
- The batch job processes the CSV and updates `carparks.db`.
- The API queries car park data from `carparks.db`.

---



