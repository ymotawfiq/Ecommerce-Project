Ecommerce-Project is a comprehensive platform designed to manage ecommerce websites, boasting a plethora of features tailored to streamline your online business. It offers functionalities such as: Product Management: Easily create product categories and seamlessly insert products with detailed information including names, descriptions, and photos. Each product can have multiple items, each with its own name, description, and photo. Category Variations: Categories can have a multitude of variations, and products may have specific variations associated with their category. Promotions: Implement promotions for categories to attract and retain customers. User Registration: Users can register on the website to purchase products, with robust authentication and authorization mechanisms in place, including two-factor authentication for enhanced security. Email Verification: Utilizing SMTP, users receive email verification to confirm their accounts, reset passwords, or update their email addresses. Address Management: Upon registration, users can securely store their address information for hassle-free product delivery. Order and Shipping Management: Effortlessly manage all aspects of orders, including order status, shipping methods, and detailed order information. Payment Methods: Users can save multiple payment methods and select their preferred option at checkout. They can also set a default payment method for convenience. Product Reviews: Empower users to rate products and leave feedback, fostering a vibrant and interactive shopping community. Ecommerce-Project is your onestop solution for building and managing a successful ecommerce venture, providing a seamless and user-friendly experience for both merchants and customers alike


How to run project?
You must install dotnet core 8, sql server and Visual studio code or visual studio 2022
to run project in visual studio code
- first you should add migration to create database in sql server
go to (Ecommerce.Data)
from terminal write (add-migration InitialCreate) if you use visual studio 2022 or (dotnet ef migrations add InitialCreate) if you use visual studio code
then write (update-database) if you use visual studio 2022 or (dotnet ef database update) if you use visual studio code

- second go to (Ecommerce.Api) and use {dotnet run} command from terminal if you use visual studio code or run it from visual studio 2022

