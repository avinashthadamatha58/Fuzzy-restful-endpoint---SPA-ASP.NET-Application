# Fuzzy-restful-endpoint---SPA-ASP.NET-Application
This is an SPA ASP.NET application to consume a fuzzy restful endpoint.
This web application is a lot complex than it looks! It displays data consumed from a fuzzy Restful endpoint.
The end point is called fuzzy because it doesn't give correct data on the first try or second try or third, or you know, I could go on.
The endpoint might give incorrect data or errors.
I used constant polling and async ajax calls to get the correct data.
Some of the data depends on the data obtained from previous endpoint.
All the calls made to several endpoints are async ajax calls, the architecture is built in a way that it doesn't run into a deadlock.
There are many traps set in this API endpoint which could make it very difficult for a user to consume this data.
