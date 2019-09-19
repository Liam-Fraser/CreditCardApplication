# CreditCardApplication

## Setup Instructions

### Prerequisites
1) Visual Studio (Tested with 2019)
2) Data Storage & Processing and ASP.Net web development packages

### Running the Software
1) Navigate into the CreditCardApplicationData Project
2) Click 'Start' in Visual Studio. This will initialise the database
3) Open up "InitialiseCreditCards.sql"
4) Execute the query (play icon in the corner of the window). This sets up the two credit cards mentioned in the Excercise
5) Navigate to the CreditCardApplication project
6) Click the the Play icon (labelled IIS Express)
7) A browser should open with the site loaded.

### Troubleshooting

- If the system is failing to connect to the database, locate the server in the SQL Server Object explorer, right click and copy the connection string. Replace the "connection" variable value with the copied connection string.

- If you have trouble executing the "InitialiseCreditCards.sql", check that the database has been created (using the SQL Server Object Explorer in VS2019) and that the tables are empty.