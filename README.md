# Brokerless

This project seeks to remove the intermediary role traditionally linking buyers and sellers of residences, commercial spaces, used gadgets/products, and lands/plots.

## Types of Users
- **Admins**: Verify and approve property listings before they go live on the site.
- **Users**: Can post, browse, and manage listings. They can also communicate with others and manage their subscriptions.


### Key Features
- **Property Listings**: Post and browse listings for residential and commercial properties, land, and used products.
- **Detailed Information**: Each listing includes important details like location, price, and property features.
- **Subscription Plans**: Choose a plan to get additional features and benefits, such as more listings or enhanced visibility.
- **Chat and Transactions**: Communicate directly with others and manage your transactions on the platform.

### Getting Started
- **Sign Up**: Create an account as an Admin or User.
- **Post or Browse**: List your properties or check out existing ones.
- **Choose a Subscription**: Select a plan that fits your needs.
- **Interact**: Use the chat feature to connect with others and manage your transactions.


## Entities

### User
- UserId (PK)
- Role (Admin, User)
- CreatedDate
- Email
- FullName
- PhoneNumber
- PhoneNumberVerified
- Subscription
- AvailableSellerDetails[]

### Property
- PropertyType (Residential, Commercial, Product, Land)
- Category (House, Hostel) - Only for Residential 
- ListingType (Sale, Rent)
- LocationLat
- LocationLon
- City
- State
- Description
- PriceNegotiable
- Price
- PricePerUnit (sq.ft)
- Rent 
- RentDuration (month, year)
- Deposit
- PostedOn
- Status
- Tags[]
- PropertyFiles[]
- SellerId (FK)
- BuyerId (FK)

### HouseDetails
- MeasurementUnit
- Length
- Width
- Height
- FloorCount
- RoomCount
- HallAndKitchen
- Restrooms
- WaterSupply
- Electricity
- GatedSecurity
- CarParking
- FurnishingDetails
- PropertyId (FK)

### HostelDetails
- TypesOfRooms (Single, Double, Three, Four)
- GenderPreference ('Male', 'Female')
- Food
- Wifi
- GatedSecurity
- PropertyId (FK)

### CommercialDetails 
- CommercialType
- FloorLevel
- MeasurementUnit
- Length
- Width
- WaterSupply
- Electricity
- Restrooms
- GatedSecurity
- CarParking
- PropertyId (FK)

### LandDetails
- MeasurementUnit
- Length
- Width
- ZoningType (Residential, Commercial, Industrial, Agricultural)
- PropertyId (FK)

### ProductDetails
- ProductType (Electronics, Household, Furniture, HomeDecor, Fitness )
- Manufacturer
- WarrantyPeriod
- WarrantyUnit
- PropertyId (FK)


### Tag
- Tag (PK)
- PropertyId (FK)

### PropertyFiles
- FileId (PK)
- FileUrl
- Type
- Size
- PropertyId

### Subscription
- SubscriptionId
- SubscribedOn
- ExpiresOn
- AvailableListingCount
- AvailableSellerViewCount
- Validity
- SubscriptionTemplateId (FK)
- UserId (FK)

### SubscriptionTemplate
- SubscriptionTemplateId 
- SubscriptionType
- Description
- MaxListingCount
- MaxSellerViewCount
- Validity


### Chat
- ChatId
- SenderId
- ReceiverId
- Message
- CreatedOn
- MessageType (text, media)
- ChatReactions[]

### ChatReaction
- ChatReactionId
- ReactedBy
- Reaction

### Transaction
- TransactionId (PK)
- Amount 
- TransactionStatus (Pending,Failed, Completed)
- SubsriptionTemplateId (FK)
- UserId (FK)
