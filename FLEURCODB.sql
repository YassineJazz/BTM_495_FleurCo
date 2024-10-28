DROP DATABASE if exists FleurCoDB;
CREATE DATABASE FleurCoDB;
use FleurCoDB;

CREATE TABLE Users (
user_id int AUTO_INCREMENT PRIMARY KEY,
name varchar(50),
email varchar(50),
main_phone_number varchar(50));

CREATE TABLE WarehouseManagers (
user_id int,
FOREIGN KEY (user_id) REFERENCES Users(user_id));

CREATE TABLE WarehouseWorkers (
user_id int,
FOREIGN KEY (user_id) references Users(user_id));

CREATE TABLE Customers (
user_id int,
fax_number varchar(50),
FOREIGN KEY (user_id) references Users(user_id)
);

CREATE TABLE Addresses (
address_id int AUTO_INCREMENT PRIMARY KEY,
street varchar(25),
municipality varchar(25),
province varchar(25),
country varchar(25),
postal_code varchar(25),
address_type varchar (25),
user_id int,
FOREIGN KEY (user_id) references Users(user_id)
);

CREATE TABLE Orders (
order_id int AUTO_INCREMENT PRIMARY KEY,
order_type varchar(25),
order_status varchar(25),
order_total decimal (7,2),
user_id int,
FOREIGN KEY (user_id) references Users(user_id)
);

CREATE TABLE NewOrders (
order_id int,
user_id int,
neworder_price decimal (7,2),
FOREIGN KEY (order_id) references Orders(order_id),
FOREIGN KEY (user_id) references Users(user_id));

CREATE TABLE BackOrders (
order_id int,
user_id int,
backorder_cost decimal (7,2),
FOREIGN KEY (order_id) references Orders(order_id),
FOREIGN KEY (user_id) references Users(user_id));

CREATE TABLE Products (
product_id int AUTO_INCREMENT PRIMARY KEY,
product_name varchar(50),
product_category varchar(50),
product_type varchar(50),
product_price decimal(6,2),
product_cost decimal(6,2)
);

CREATE TABLE OrderProducts (
order_id int,
product_id int,
product_qty int,
email varchar(50),
main_phone_number varchar(50),
FOREIGN KEY (order_id) references Orders(order_id),
FOREIGN KEY (product_id) references Products(product_id)
);

CREATE TABLE Invoices (
invoice_id int AUTO_INCREMENT PRIMARY KEY,
total_price decimal (7,2),
invoice_date date,
order_id int,
FOREIGN KEY (order_id) references Orders(order_id)
);

CREATE TABLE SalesForecasts (
forecast_id int AUTO_INCREMENT PRIMARY KEY,
start_date date,
end_date date,
forecast_stock_level int,
product_id int,
FOREIGN KEY (product_id) references Products(product_id)
);