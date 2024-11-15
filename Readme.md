Demonstration of Searching and Filtering in EntityFramework
====
In this project, I show how it is possible to search for items in a database, using EntityFramework.

If you run this project, a Swagger window should open up with CRUD operations for a database of boxes. If you first Get /api/Boxes, you should get the entire list of boxes.

If you instead go to Get /api/Boxes/Search, you can get a list of boxes with certain properties, replace the "additionalProperty" and "string" with the property and its value .

Valid properties are "name","color", "height","width","depth" and "volume" (the search is case insensitive!)

The value must always be set as a string, even if it is a number. For example setting the property to this:

	{
		"Color": "Red",
		"Volume": "4"
	}

Returns all Red boxes with volume 4 (It is entirely possible it returns `[]` since the boxes are randomly generated at startup), Colour can be any string, but only Red, Green, Blue, White or Black boxes are generated when the program starts (Color is case insensitive)