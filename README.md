# Mutants API
Technical Challenge for Mercado Libre to build an API to identify whether the specified DNA sequence is human or mutant.

## Requirements
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Optional: Visual Studio Code
- [MongoDB](https://www.mongodb.com/) running either [locally](https://docs.mongodb.com/manual/installation/) or in a container. The src code provides a docker-compose.yml for the later.
- Postman or similar tools to test the API. Additionally the API exposes a /swagger endpoint that could be use to test the same.

## Setup and Run
- To run the application locally you will need to have an instance of MongoDB running, or setup one container and update the connectionstring accordingly.
- The API can run in a containerized environment by executing from the root dir of the project: 
`docker-compose -f "docker-compose.yml" up -d --build`

## Usage
API is build following [Ion Hypermedia Type](https://ionspec.org/) specs. You can see all API endpoints by accessing the root path.
i.e.: http://localhost:8080/ will return Status 200 OK with the following body

```
{
    "href": "http://localhost:8080/",
    "info": {
        "href": "http://localhost:8080/info"
    },
    "getStats": {
        "href": "http://localhost:8080/stats"
    },
    "registerDna": {
        "href": "http://localhost:8080/mutant",
        "rel": [
            "create-form"
        ],
        "method": "POST",
        "value": [
            {
                "name": "dna",
                "label": "Dna sequence to process",
                "required": true,
                "type": "array",
                "minLength": 1
            }
        ]
    }
}
```
Where each node corresponds to a valid method that can be accessed through their "href" value. This will allow clients to navigate through the API taking advantage of HTTP protocol.


### /mutant service
The mutant service provides the ability to evaluate a DNA secuence in the following json definition:
```
POST /mutant
{
  "dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
}
```
In case the dna is mutant, the service will return HTTP 200-OK; otherwise, a status result 403-Forbidden will be returned.

### /stats service
The stats service provides the number of evaluated human and mutant DNA sequences, along with the corresponding ration as follows:
```
{
    "href": "http://localhost:8080/stats",
    "count_mutant_dna": 6,
    "count_human_dna": 0,
    "ratio": 1.0
}
```
