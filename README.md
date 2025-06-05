# Rira-Task
# ToDo Application
📘 Todo API Documentation
🧾 Overview
I used Clean Architecture for this project and the explanations are in the format of this architecture.
This API allows clients to manage Todo items through Create, Read, Update, and Delete (CRUD) operations. The API follows REST principles and uses FluentValidation for data validation. Error responses follow a structured format.

🔐 Base URL
arduino
Copy
Edit
https://yourdomain.com/api/todo
🧩 Data Models
🎯 TodoItemDto
json
Copy
Edit
{
  "title": "string",
  "description": "string",
  "dueDate": "2025-06-10T00:00:00Z",
  "isCompleted": false
}
Property	Type	Required	Description
title	string	✅ Yes	Title of the todo item
description	string?	❌ No	Optional description
dueDate	DateTime	✅ Yes	Due date of the todo
isCompleted	bool	✅ Yes	Completion status

🔧 API Endpoints
📌 Create Todo
POST /api/todo

Request Body
json
Copy
Edit
{
  "title": "Buy groceries",
  "description": "Milk, Bread, Cheese",
  "dueDate": "2025-06-10T10:00:00Z",
  "isCompleted": false
}
Responses
201 Created: Returns the ID of the created todo.

400 Bad Request: Validation errors

json
Copy
Edit
{
  "message": "Validation failed",
  "errors": {
    "title": ["Title is required."],
    "dueDate": ["DueDate is required."]
  }
}
📌 Get Todo By ID
GET /api/todo/{id}

Path Parameter
id (Guid): The unique identifier of the todo item.

Responses
200 OK: Returns the TodoItemDto.

404 Not Found: If the item does not exist.

json
Copy
Edit
{
  "message": "Todo with ID {id} not found."
}
📌 Get All Todos
GET /api/todo

Responses
200 OK: Returns a list of todos.

json
Copy
Edit
[
  {
    "title": "Buy groceries",
    "description": "Milk, Bread",
    "dueDate": "2025-06-10T10:00:00Z",
    "isCompleted": false
  }
]
📌 Update Todo
PUT /api/todo/{id}

Path Parameter
id (Guid): The ID of the item to update.

Request Body
Same as create.

Responses
204 No Content: Successfully updated.

400 Bad Request: Validation error.

404 Not Found: If item does not exist.

📌 Delete Todo
DELETE /api/todo/{id}

Path Parameter
id (Guid): The ID of the item to delete.

Responses
204 No Content: Successfully deleted.

404 Not Found: If item does not exist.

json
Copy
Edit
{
  "message": "Todo with ID {id} not found."
}
❗ Error Handling
📤 Validation Error Format
json
Copy
Edit
{
  "message": "Validation failed",
  "errors": {
    "title": ["Title is required."],
    "dueDate": ["DueDate is required."]
  }
}
📤 Not Found Format
json
Copy
Edit
{
  "message": "Todo with ID {id} not found."
}
🛠️ Status Codes Summary
Code	Meaning
200	OK
201	Created
204	No Content
400	Bad Request (Validation)
404	Not Found
500	Internal Server Error
