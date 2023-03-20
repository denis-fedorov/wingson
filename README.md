# Narrative:
As a Front End Developer I need a REST-ful Web API for my ticketing website, so that I can access and
manage information related to the passenger.

# Acceptance criteria:
We need the following resources for our website:
1. Endpoint that returns a Person by Id.
2. Endpoint that returns all passengers on the flight ‘PZ696’.
3. Endpoint that lists all the male passengers.
4. BONUS: Endpoint that creates a booking of an existing flight for a new passenger.
5. BONUS: Endpoint that updates passenger’s address.

# Recommendations:
1. Assume you are implementing a production-ready solution.
2. Focus on code quality and coverage.
3. Put your best on it, but keep it simple.
4. Extending the provided code is optional, but changing it is not recommended.

# Example output
## 1. Returns a Person by Id
```json
{
  "Name": "Kendall Velazquez",
  "DateBirth": "1980-09-24T00:00:00",
  "Gender": 0,
  "Address": "805-1408 Mi Rd.",
  "Email": "egestas.a.dui@aliquet.ca",
  "Id": 91
}
```

## 2. Returns all passengers on the flight ‘PZ696’
```json
[
  {
    "Name": "Claire Stephens",
    "DateBirth": "1948-11-27T00:00:00",
    "Gender": 1,
    "Address": "P.O. Box 344, 5822 Curabitur Rd.",
    "Email": "non.cursus.non@turpisIncondimentum.co.uk",
    "Id": 69
  },
  {
    "Name": "Kendall Velazquez",
    "DateBirth": "1980-09-24T00:00:00",
    "Gender": 0,
    "Address": "805-1408 Mi Rd.",
    "Email": "egestas.a.dui@aliquet.ca",
    "Id": 91
  }
]
```

## 3. Returns all the male passengers
```json
[
  {
    "Name": "Kendall Velazquez",
    "DateBirth": "1980-09-24T00:00:00",
    "Gender": 0,
    "Address": "805-1408 Mi Rd.",
    "Email": "egestas.a.dui@aliquet.ca",
    "Id": 91
  },
  {
    "Name": "Branden Johnston",
    "DateBirth": "1940-01-01T00:00:00",
    "Gender": 0,
    "Address": "P.O. Box 795, 1956 Odio. Rd.",
    "Email": "egestas.lacinia@Proinmi.com",
    "Id": 77
  }
]
```