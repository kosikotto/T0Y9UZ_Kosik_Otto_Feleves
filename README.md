# 🌤️ Cross-Platform Weather App (.NET MAUI)

A modern, mobile-optimized weather forecasting application that leverages the power of the .NET MAUI framework to provide a native experience. During development, I followed the MVVM (Model-View-ViewModel) design pattern, ensuring a clean separation of code and high testability.

---

## 🌟 Key Features

*   **Real-time and 5-day forecast:** Accurate current weather data and detailed forecasts for the upcoming days from the OpenWeather database.
*   **Intelligent location detection:** 
    *   If GPS is enabled, the app automatically detects your current location.
    *   Without GPS, it loads the saved default location.
*   **Manual search:** Ability to discover and search for new cities.
*   **Favorites management:** A user-friendly interface for saving and quickly accessing frequently searched locations.
*   **Offline support & Cache:** Using an internal database (SQLite), the application stores the most recently fetched data, making previous information available even without an internet connection.
*   **Network awareness:** Built-in checking mechanism that monitors the internet connection status.
*   **Customization:** Built-in Dark and Light mode (Theme Switcher) for a comfortable user experience.

---

## 🏗️ Technical Details

*   **Architecture:** MVVM (cleanly separated UI and business logic).
*   **Data Storage:** Local database for data caching (SQLite / Preferences).
*   **API Integration:** External weather service (OpenWeatherAPI) via REST API.
*   **Hardware Access:** Geolocation services for accurate positioning.

---

## 🛠️ Usage and Installation

1. Clone the repository.
2. Open the solution in Visual Studio 2022 (.NET MAUI workload required).
3. Select the target platform (Android emulator or physical device).
4. Run the application (press `F5`).
