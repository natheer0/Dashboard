# Product Management Dashboard

The Product Management Dashboard is a web application designed to streamline the process of managing products within an organization. Built using ASP.NET Core MVC along with HTML, CSS, JavaScript, and Bootstrap, this dashboard provides a user-friendly interface for administrators to efficiently handle product-related tasks.

The application supports the following functionalities:
- **Product Management:** Users can perform full CRUD (Create, Read, Update, Delete) operations on products.
- **Product Details Management:** Users have the ability to perform full CRUD operations on product details, ensuring all relevant information is up-to-date.
- **Damaged Products Management:** The dashboard allows users to log damaged products, helping keep track of inventory issues.

The intuitive design and responsive layout make it easy to use on various devices, ensuring accessibility and ease of use for administrators managing the product inventory.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup](#setup)
- [Usage](#usage)

## Features

- **Product Management:** Full CRUD operations for products.
- **Product Details Management:** Full CRUD operations for product details.
- **Damaged Products Management:** Add damaged products.

## Technologies Used

- **ASP.NET Core MVC:** Backend framework for building web applications.
- **HTML:** Markup language for creating web pages.
- **CSS:** Stylesheet language for designing web pages.
- **JavaScript:** Programming language for creating dynamic web content.
- **Bootstrap:** Front-end framework for developing responsive and mobile-first web pages.

## Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/natheer0/Dashboard.git
    ```
2. **Navigate to the project directory:**
    ```bash
    cd Dashboard
    ```
3. **Install the required packages:**
    ```bash
    dotnet restore
    ```
4. **Run the application:**
    ```bash
    dotnet run
    ```
5. **Open your browser and navigate to:**
    ```
    http://localhost:5000
    ```

## Usage

1. **Adding a Product:**
    - Navigate to the Products section.
    - Click on the 'Add Product' button.
    - Fill in the product details and save.

2. **Editing a Product:**
    - Navigate to the Products section.
    - Click on the 'Edit' icon next to the product you want to edit.
    - Update the product details and save.

3. **Deleting a Product:**
    - Navigate to the Products section.
    - Click on the 'Delete' icon next to the product you want to delete.

4. **Managing Product Details:**
    - Navigate to the Product Details section.
    - Use the available options to add, edit, or delete product details.

5. **Adding Damaged Products:**
    - Navigate to the Damaged Products section.
    - Click on the 'Add Damaged Product' button.
    - Fill in the details and save.
