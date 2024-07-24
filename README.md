# Brokerless

This project seeks to remove the intermediary role traditionally linking buyers and sellers of residences, commercial spaces, used gadgets/products, and lands/plots.

## Types of user
- <b>Admin</b> - Will be verifying the properties uploaded by sellers, The product will be published once admins approve the listing.

- <b>User</b> - can buy, sell properties on this platform.

## Entities

### User
- UserId (PK)
- Role (Admin, User)
- CreatedDate
- Email
- FullName
- PhoneNumber
- Subscription

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
- UserId (FK)

### HouseDetails
- MeasurementUnit
- Length
- Width
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
- SubscriptionTemplateId (FK)

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