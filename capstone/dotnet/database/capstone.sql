USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

GO

BEGIN TRANSACTION;

CREATE TABLE accounts
(
	account_id int identity(1,1),
	user_id int not null,
	first_name varchar(100),
	last_name varchar(100),
	address varchar(200),
	zipcode int,
	state char(2),
	phone_number int,
	email varchar(100),
	CONSTRAINT PK_accounts PRIMARY KEY (account_id),
	CONSTRAINT FK_accounts_users FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE category
(
	category_id int identity(1,1),
	name varchar(50) not null,
	category_image varchar(500),
	category_description varchar(2000),
	CONSTRAINT PK_category PRIMARY KEY (category_id),
);

CREATE TABLE area
(
	area_id int identity(1,1),
	name varchar(50) not null,
	CONSTRAINT PK_area PRIMARY KEY (area_id),
);

CREATE TABLE recipe
(
	recipe_id int identity(1,1),
	recipe_name varchar(500) not null,
	drink_alternate varchar(500),
	category_id int,
	area_id int,
	instructions varchar(5000),
	recipe_image varchar(500),
	recipe_tags varchar(1000),
	youtube varchar(1000),
	source varchar(1000),
	image_source varchar(1000),
	date varchar(1000),
	user_id int,
	CONSTRAINT PK_recipe PRIMARY KEY (recipe_id),
	CONSTRAINT FK_recipe_users FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT FK_recipe_category FOREIGN KEY (category_id) REFERENCES category(category_id),
);

CREATE TABLE user_recipes
(
	id int identity(1,1),
	user_id int not null,
	recipe_id int not null,
	CONSTRAINT PK_user_recipes PRIMARY KEY (id),
	CONSTRAINT FK_user_recipes_users FOREIGN KEY (user_Id) REFERENCES users(user_id),
	CONSTRAINT FK_user_recipes_recipe FOREIGN KEY (recipe_id) REFERENCES recipe(recipe_id)
);

CREATE TABLE ingred_type
(
	type_id int identity(1,1),
	name varchar(100) not null,
	isFresh bit,
	CONSTRAINT PK_ingred_type PRIMARY KEY (type_id),
);

INSERT INTO ingred_type (name) VALUES ('Grain');
INSERT INTO ingred_type (name) VALUES ('Bread');
INSERT INTO ingred_type (name) VALUES ('Seafood');
INSERT INTO ingred_type (name) VALUES ('Vegetable');
INSERT INTO ingred_type (name) VALUES ('Drink');
INSERT INTO ingred_type (name) VALUES ('Fruit');
INSERT INTO ingred_type (name) VALUES ('Rice');
INSERT INTO ingred_type (name) VALUES ('Preserve');
INSERT INTO ingred_type (name) VALUES ('Fish');
INSERT INTO ingred_type (name) VALUES ('Spice');
INSERT INTO ingred_type (name) VALUES ('Meat');
INSERT INTO ingred_type (name) VALUES ('Sugar');
INSERT INTO ingred_type (name) VALUES ('Juice');
INSERT INTO ingred_type (name) VALUES ('Liquid');
INSERT INTO ingred_type (name) VALUES ('Fat');
INSERT INTO ingred_type (name) VALUES ('Cheese');
INSERT INTO ingred_type (name) VALUES ('Sauce');
INSERT INTO ingred_type (name) VALUES ('Cereal');
INSERT INTO ingred_type (name) VALUES ('Stock');
INSERT INTO ingred_type (name) VALUES ('Root Vegetable');
INSERT INTO ingred_type (name) VALUES ('Wine');
INSERT INTO ingred_type (name) VALUES ('Liqueur');
INSERT INTO ingred_type (name) VALUES ('Confectionery');
INSERT INTO ingred_type (name) VALUES ('Pastry');
INSERT INTO ingred_type (name) VALUES ('Bean');
INSERT INTO ingred_type (name) VALUES ('Dressing');
INSERT INTO ingred_type (name) VALUES ('Spirit');
INSERT INTO ingred_type (name) VALUES ('Side');
INSERT INTO ingred_type (name) VALUES ('Curd');
INSERT INTO ingred_type (name) VALUES ('Sedge');
INSERT INTO ingred_type (name) VALUES ('Vinegar');
INSERT INTO ingred_type (name) VALUES ('Seasoning');

CREATE TABLE ingredient
(
	ingred_id int identity(1,1),
	name varchar(500) not null,
	description varchar(5000),
	type_id int,
	ingred_image varchar(500),
	CONSTRAINT PK_ingredient PRIMARY KEY (ingred_id),
	CONSTRAINT FK_ingredient_ingred_type FOREIGN KEY (type_id) REFERENCES ingred_type (type_id),
);

INSERT INTO ingredient (name) VALUES ('butter, softened');
INSERT INTO ingredient (name) VALUES ('Green Chili');
INSERT INTO ingredient (name) VALUES ('Blackberrys');
INSERT INTO ingredient (name) VALUES ('Red Chili Powder');
INSERT INTO ingredient (name) VALUES ('All spice');
INSERT INTO ingredient (name) VALUES ('Carrot');
INSERT INTO ingredient (name) VALUES ('Self raising flour');
INSERT INTO ingredient (name) VALUES ('Vermicelli');
INSERT INTO ingredient (name) VALUES ('Harissa');
INSERT INTO ingredient (name) VALUES ('Clove');
INSERT INTO ingredient (name) VALUES ('Potato');

CREATE TABLE recipes_ingredients
(
	ri_id int identity(1,1) not null,
	name VARCHAR(500) not null,
	recipe_id int,
	ingred_id int,
	measure VARCHAR(500),
	CONSTRAINT PK_recipes_ingredients PRIMARY KEY (ri_id),
	CONSTRAINT FK_recipes_ingredients_recipe FOREIGN KEY (recipe_id) REFERENCES recipe(recipe_id),
	CONSTRAINT FK_recipes_ingredients_ingredient FOREIGN KEY (ingred_id) REFERENCES ingredient(ingred_id),
);

CREATE TABLE planner
(
	planner_id int identity(1,1),
	name varchar(500),
	user_id int,
	isSharable bit null,
	CONSTRAINT PK_planner PRIMARY KEY(planner_id),
	CONSTRAINT FK_planner_users FOREIGN KEY(user_id) REFERENCES users(user_id),
);

CREATE TABLE account_recipes
(
	recipe_id int not null,
	account_id int not null,
	CONSTRAINT PK_account_recipes PRIMARY KEY (recipe_id, account_id),
	CONSTRAINT FK_account_recipes_recipe FOREIGN KEY (recipe_id) REFERENCES recipe(recipe_id),
	CONSTRAINT FK_account_recipes_accounts FOREIGN KEY (account_id) REFERENCES accounts(account_id),
)

CREATE TABLE grocery_list
(
	grocery_id int not null,
	ingred_id int not null,
	account_id int not null,
	CONSTRAINT PK_grocery_list PRIMARY KEY (grocery_id),
	CONSTRAINT FK_grocery_list_ingredient FOREIGN KEY (ingred_id) REFERENCES ingredient(ingred_id),
	CONSTRAINT FK_grocery_list_accounts FOREIGN KEY (account_id) REFERENCES accounts(account_id),
)

CREATE TABLE recipes_planner
(
	rp_id int identity(1,1),
	planner_id int not null,
	recipe_id int not null,
	day varchar(20),
	week int,
	CONSTRAINT PK_recipes_planner PRIMARY KEY (rp_id),
	CONSTRAINT FK_recipes_planner_recipe FOREIGN KEY (recipe_id) REFERENCES recipe(recipe_id),
	CONSTRAINT FK_recipes_planner_planner FOREIGN KEY (planner_id) REFERENCES planner(planner_id),
	CONSTRAINT CHK_day CHECK (day IN ('sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday')),
)

