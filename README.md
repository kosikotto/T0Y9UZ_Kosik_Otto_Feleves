🌤️ Cross-Platform Weather App (.NET MAUI)

Egy modern, mobilra optimalizált időjárás-előrejelző alkalmazás, amely a .NET MAUI keretrendszer erejét kihasználva nyújt natív élményt. A fejlesztés során az MVVM (Model-View-ViewModel) tervezési mintát követtem, biztosítva a kód tiszta szétválasztását és tesztelhetőségét.

***

🌟 Főbb funkciók

1. Valós idejű és 5 napos előrejelzés: Pontos adatok a jelenlegi időjárásról és részletes prognózis a következő napokra az OpenWeather adatbázisából.
2. Intelligens helymeghatározás: * Engedélyezett GPS esetén az alkalmazás automatikusan felismeri a tartózkodási helyet.
3. GPS hiányában az elmentett alapértelmezett helyet tölti be.
4. Manuális keresési lehetőség új városok felfedezéséhez.
5. Kedvencek kezelése: Felhasználóbarát felület a gyakran keresett helyszínek mentéséhez és gyors eléréséhez.
6. Offline támogatás & Cache: Belső adatbázis (SQLite) segítségével az alkalmazás eltárolja az utolsó lekérdezett adatokat, így internetkapcsolat nélkül is elérhetőek a korábbi információk.
7. Hálózati tudatosság: Beépített ellenőrző mechanizmus, amely figyeli az internetkapcsolat állapotát.
8. Személyre szabhatóság: Beépített sötét és világos mód (Theme Switcher) a kényelmes használat érdekében.

***

🏗️ Technikai részletek

1. Architektúra: MVVM (tisztán szétválasztott UI és üzleti logika).
2. Adattárolás: Helyi adatbázis az adatok cache-elésére (SQLite / Preferences).
3. API integráció: Külső időjárási szolgáltatás (OpenWeatherAPI) REST API-n keresztül.
4. Hardver elérés: Geolocation szolgáltatások a pontos pozicionáláshoz.

***

🛠️ Használat és Telepítés

1. Klónozd a repository-t.
2. Nyisd meg a solution-t Visual Studio 2022-ben (MAUI workload szükséges).
3. Válaszd ki a célplatformot (Android emulátor vagy fizikai eszköz).
4. Indítsd el a futtatást (F5).
