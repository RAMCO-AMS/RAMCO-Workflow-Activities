This activity is located here:

![.](/Documentation/Images/price%20list%20activity.png)


You must pass two input parameters:

- Product (Entity Reference): Which product the new price list items will be associated with
- Price (Currency): The price to set for each as a currency data type

![](/Documentation/Images/create%20price%20list%20items%20input%20parameters.png)


This activity will then query all active price lists, then create a price list item for each at the given price


## Note: ##

All created price list items will have the same price. If you wish to set different prices based on price list you must do so manually.
