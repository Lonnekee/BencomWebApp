# BencomWebApp

## Model
- Feed: the view model used in TwitterController. Contains a list of tweets and a user name.
- Tweet: contains certain variables describing the tweet, such as name of the writer, date and time.

## View
- Home: only displays "Welcome".
- Twitter:
	- Index: a page where the feed of a specific user can be requested
	- ShowFeed: a page with the recent tweets of the requested user.

## Controller
- HomeController: shows the home page.
- Twittercontroller:
	- Index(): displays a page where input is requested.
	- Index(model): has received the input and validates it. If it is validated, redirects to ShowFeed.
	If not, the application gives an error message on the index page.
	- ShowFeed(model): requests the tweets of the given user name and gives that model to the view ShowFeed.

## Util
- StaticCache: contains the stored bearer token, which allows access to the API. The bearer token is 
  stored in Startup.cs.
- TwitterUserNameAttribute: the attribute is used to check if the input of the field UserName
  on the Twitter page adheres to certain restrictions. Applied in the Model.Feed class.