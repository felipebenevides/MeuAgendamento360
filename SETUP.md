# MeuAgendamento360 Setup Guide

This guide will help you set up the MeuAgendamento360 project for local development.

## Prerequisites

- .NET 9.0 SDK
- Node.js 18+ and npm
- PostgreSQL 15+
- Git

## Backend Setup

1. Clone the repository:
   ```
   git clone https://github.com/felipebenevides/MeuAgendamento360.git
   cd MeuAgendamento360
   ```

2. Set up the database:
   ```
   # Create a PostgreSQL database named 'meuagendamento360'
   # Update connection string in appsettings.Development.json
   ```

3. Run migrations:
   ```
   cd src/myschedule360.API
   dotnet ef database update
   ```

4. Start the API:
   ```
   dotnet run
   ```
   The API will be available at `https://localhost:7001`

## Frontend Setup

1. Navigate to the frontend directory:
   ```
   cd frontend
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Create a `.env.local` file with the following content:
   ```
   NEXT_PUBLIC_API_URL=https://localhost:7001
   ```

4. Start the development server:
   ```
   npm run dev
   ```
   The frontend will be available at `http://localhost:3000`

## Running Tests

### Backend Tests
```
dotnet test
```

### Frontend Tests
```
cd frontend
npm test
```

## Docker Setup (Optional)

You can also run the entire application using Docker:

```
docker-compose up -d
```

This will start the API, frontend, and PostgreSQL database in containers.

## Troubleshooting

- If you encounter database connection issues, ensure PostgreSQL is running and the connection string is correct
- For frontend build errors, ensure you're using the correct Node.js version
- Check firewall settings if services can't communicate with each other

## Additional Resources

- [Project Documentation](./project_documentation.md)
- [Development Tasks](./development_tasks.md)
- [Contributing Guidelines](./CONTRIBUTING.md)