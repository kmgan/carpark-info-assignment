# Carpark Info Assignment

## Getting Started

To run this project, follow these steps:

1. **Clone the repository:**  
   ```sh
   git clone https://github.com/kmgan/carpark-info-assignment.git
   ```

2. **Project Structure:**
   - `csv/` - Contains the carpark dataset.
   - `carparks.db` - The SQLite database (contains table structures and one dummy record for users).
   - `ERD/` - Contains the ERD diagram for Task 1.
   - `CarparkInfoAPI/` - Contains the API project for Task 3.
   - `CarparkInfoBatchJob/` - Contains the batch job project for Task 2.

### Restore Dependencies
Before running the applications, restore dependencies by navigating to the respective folders and running:
```sh
dotnet restore
```

## Running the Projects

### Running the Batch Job (Task 2)
To insert carpark data into the database, **run the batch job first**:
1. Open `CarparkInfoBatchJob/` in your IDE.
2. Open `Program.cs`.
3. Click **Run** or press `F5`.

### Running the API (Task 3)
Once the database is populated, you can proceed to run the API:
1. Open `CarparkInfoAPI/` in your IDE.
2. Open `Program.cs`.
3. Click **Run** or press `F5`.

After running the API, you can access its endpoints to interact with the carpark data.

---

