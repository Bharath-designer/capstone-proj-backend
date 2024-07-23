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

### Property
- PropertyType (Residential, Commercial, Product, Land)
- Category (House, Hostel) - Only for Residential 
- ListingType (Sale, Rent)
- Tags
- LocationLat
- LocationLon
- City
- State
- Description
- PriceNegotiable
- Files
- Price
- PricePerUnit (sq.ft)
- Rent 
- RentDuration (month, year)
- Deposit
- PostedOn
- Status

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


### HostelDetails
- TypesOfRooms (Single, Double, Three, Four)
- GenderPreference ('Male', 'Female')
- Food
- Wifi
- GatedSecurity

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

### LandDetails
- MeasurementUnit
- Length
- Width
- ZoningType 

### ProductDetails
- ProductType (Electronics, Household, Furniture, HomeDecor, Fitness )
- Manufacturer
- WarrantyPeriod
- WarrantyUnit