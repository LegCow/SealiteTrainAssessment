## Design Decisions

 # Analysis
	Originally I thought each link was 2 way but the input file and test examples makes it apparent they're one way only.
	Thought it was going to be very simple until I saw some of the later tests!
	Modelled it in NetworkGraph.png. MS Paint skills!

# Towns and Links
	Using the towns as a linked set of nodes to make route planning a bit simpler and a bit more Object Oriented.
	TrainNetwork contains Towns which contain Links pointing to other towns.
	Using a lot of overrides on Equals and GetHashCode so the tests look cleaner. Could have used new Record types.
	I tend to use Domain Driven Designs when possible - A business first method of design just makes life so much easier.


# Primitive encapsulation for TownCode and LinkDistance
	In this instance it's overkill but the concept is good when combined with proper equality (ValueObject)

# Repository
	Separate repository to reduce dependencies on object rehydration
	Includes Interface in case we're loading from SQL/EF or any other data source.

# Route Planner
	Separated into it's own class so that I could implement additional methods against the IRoutePlanner interface.
	I would like to use a more interesting approach to this, a good implementation to the Travelling Salesman Problem for example. That would be pretty fun.
	For this instance though, brute force it is! there's a parameter to set the maximum number of nodes to traverse so it doesn't explode.
	Cyclic links are required given some of the test.
	
# Other things

	If I was going to commit more time to improve the design and architecture I would separate the code into something like Onion Architecture.
		Domain to contain all the domain objects such as the towns and links.
		Application would contain the Route Plannet modules
		Infrastructure would contain the repositories

	Take a crack at another efficient node traversal algorithm!

	Application Builder
		I didn't use the new console application builder but it would be great for managing dependency injection for the repository and route planner.
		I would also pull in the file path from the config file and set up a logger for the fun of it.

	Hard to implement more SOLID/Clean principles without overengineering to the point of it being rediculous.

# Thanks
	Thanks for this style of assessment, I really do prefer this sort of thing compared to some of the rubbish online assessments, FIZZBUZZ or to create an algorithm for sorting a list in O(log(sin(exp(blah)))

Cheers,
Joel.

	
