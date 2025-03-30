# SauceDemo Login Test Automation

This repository contains automated test cases for testing the login functionality of the SauceDemo application using Selenium WebDriver, MSTest, and Fluent Assertions.

## Prerequisites

Before running the tests, ensure the following are installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Selenium WebDriver](https://www.selenium.dev/)
- A supported browser (Chrome, Firefox, or Edge)
- Browser-specific WebDriver (e.g., `chromedriver`, `geckodriver`, `msedgedriver`)
- [Fluent Assertions](https://fluentassertions.com/)
- [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)

## Test Overview

The automated tests validate the login functionality of the [SauceDemo](https://www.saucedemo.com/) website. The tests cover the following scenarios:

1. **Login with empty credentials**:
   - Verifies that the "Username is required" error message is displayed when no credentials are provided.

2. **Login with an empty password**:
   - Verifies that the "Password is required" error message is displayed when the password field is left empty.

3. **Login with correct credentials**:
   - Validates that the inventory page header is displayed upon successful login using various user credentials.

## Test Execution

### Run Tests in Different Browsers

Tests are running in different browsers. The supported browsers are:

- `chrome`
- `firefox`
- `edge`

