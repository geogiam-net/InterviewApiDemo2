# Introduction 

This a small Demo of a web api project to show my skill and knowledge, of course there is still more than one could add in real software, like unit tests, loggers, translators, more services, etc, that is out of the scope and requirements.

# Getting Started
What you need to run the project:
1.	Install .Net 10
2.	Install Visual Studio 2026

# Build and Test
Out of the box, one only needs to compile the Demo.Api and it should work.

To test the web api, one can could use swagger, but I think it is easier to go the HttpTests folder and activate the endpoints with the requests at:

* Authorize.http            (use 1st to generate a token)
* VehicleEmissions.http     (paste the token and get emissions)

Some requests are wrong on purpose to test that errors or validations are generated and formed well.